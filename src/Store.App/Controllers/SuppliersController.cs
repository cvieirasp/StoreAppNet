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
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _repository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository repository, IAddressRepository addressRepository, IMapper mapper)
        {
            _repository = repository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        // GET: Suppliers
        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _repository.GetAll()));
        }

        // GET: Suppliers/Details/5
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddress(id);
            if (supplierViewModel == null) return NotFound();
            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [Route("novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);
            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _repository.Add(supplier);
            return RedirectToAction("Index");
        }

        // GET: Suppliers/Edit/5
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddressAndProducts(id);
            if (supplierViewModel == null) return NotFound();
            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(supplierViewModel);
            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _repository.Update(supplier);
            return RedirectToAction("Index");
        }

        // GET: Suppliers/Delete/5
        [Route("remover-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddress(id);
            if (supplierViewModel == null) return NotFound();
            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [Route("remover-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddress(id);
            if (supplierViewModel == null) return NotFound();
            await _repository.Delete(id);
            return RedirectToAction("Index");
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await GetSupplierWithAddress(id);
            if (supplier == null)
                return NotFound();
            return PartialView("_UpdateAddress", new SupplierViewModel { Address = supplier.Address });
        }

        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierWithAddress(id);
            if (supplier == null)
                return NotFound();
            return PartialView("_AddressDetails", supplier);
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");
            if (!ModelState.IsValid)
                return PartialView("_UpdateAddress", supplierViewModel);
            await _addressRepository.Update(_mapper.Map<Address>(supplierViewModel.Address));
            var url = Url.Action("GetAddress", "Suppliers", new { id = supplierViewModel.Address.SupplierId });
            return Json(new { success = true, url });
        }

        private async Task<SupplierViewModel> GetSupplierWithAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _repository.GetWithAddress(id));
        }

        private async Task<SupplierViewModel> GetSupplierWithAddressAndProducts(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _repository.GetWithAddressAndProducts(id));
        }
    }
}