using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, ISupplierRepository supRepository, IMapper mapper)
        {
            _repository = repository;
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
            var productViewModel = await SetSuppliers(new ProductViewModel());
            return View(productViewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await SetSuppliers(productViewModel);
            if (!ModelState.IsValid) return RedirectToAction("Index");
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

        private async Task<ProductViewModel> SetSuppliers(ProductViewModel productViewModel)
        {
            productViewModel.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supRepository.GetAll());
            return productViewModel;
        }
    }
}
