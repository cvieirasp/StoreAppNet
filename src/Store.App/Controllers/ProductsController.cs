using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.App.ViewModels;
using Store.Business.Interfaces;
using Store.Business.Models;

namespace Store.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _repository;
        private readonly ISupplierRepository _supRepository;
        private readonly ICategoryRepository _catRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, ICategoryRepository catRepository, ISupplierRepository supRepository, IMapper mapper)
        {
            _repository = repository;
            _catRepository = catRepository;
            _supRepository = supRepository;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _repository.GetListWithSuppliers()));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetWithSupplier(id);
            if (productViewModel == null) return NotFound();
            return View(productViewModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var productViewModel = await SetSuppliersAndCategories(new ProductViewModel());
            return View(productViewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await SetSuppliersAndCategories(productViewModel);
            if (!ModelState.IsValid) return RedirectToAction("Index");

            string prefixFile = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_";
            string fileName = prefixFile + productViewModel.ImageUpload.FileName;

            if (!await UploadFile(productViewModel.ImageUpload, fileName))
                return View(productViewModel);

            productViewModel.Image = fileName;

            var product = _mapper.Map<Product>(productViewModel);
            await _repository.Add(product);

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetWithSupplier(id);
            if (productViewModel == null) return NotFound();
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(productViewModel);
            var product = _mapper.Map<Product>(productViewModel);
            await _repository.Update(product);
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetWithSupplier(id);
            if (productViewModel == null) return NotFound();
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productViewModel = await GetWithSupplier(id);
            if (productViewModel == null) return NotFound();
            await _repository.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetWithSupplier(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _repository.GetWithSupplier(id));
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supRepository.GetAll());
            return product;
        }

        private async Task<ProductViewModel> SetSuppliersAndCategories(ProductViewModel productViewModel)
        {
            productViewModel.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supRepository.GetAll());
            productViewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _catRepository.GetAll());
            return productViewModel;
        }

        private async Task<bool> UploadFile(IFormFile file, string fileName)
        {
            if (file?.Length <= 0) return false;
            var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            
            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Create))
                await file.CopyToAsync(stream);

            return System.IO.File.Exists(path);
        }
    }
}
