using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.App.Extensions;
using Store.App.ViewModels;
using Store.Business.Interfaces;
using Store.Business.Models;

namespace Store.App.Controllers
{
    [Authorize]
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _repository;
        private readonly ISupplierService _service;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository repository, ISupplierService service, IMapper mapper, INotificator notificator)
            :base(notificator)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        // GET: Suppliers
        [AllowAnonymous]
        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _repository.GetAll()));
        }

        // GET: Suppliers/Details/5
        [AllowAnonymous]
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddress(id);
            if (supplierViewModel == null) return NotFound();
            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        [ClaimsAuthorize("Supplier", "Add")]
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [ClaimsAuthorize("Supplier", "Add")]
        [Route("novo-fornecedor")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);
            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _service.Add(supplier);

            if (!ValidOperation())
                return View(supplierViewModel);

            return RedirectToAction("Index");
        }

        // GET: Suppliers/Edit/5
        [ClaimsAuthorize("Supplier", "Edit")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddressAndProducts(id);
            if (supplierViewModel == null) return NotFound();
            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        [ClaimsAuthorize("Supplier", "Edit")]
        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(supplierViewModel);
            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _service.Update(supplier);

            if (!ValidOperation())
                return View(await GetSupplierWithAddressAndProducts(id));

            return RedirectToAction("Index");
        }

        // GET: Suppliers/Delete/5
        [ClaimsAuthorize("Supplier", "Delete")]
        [Route("remover-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddress(id);
            if (supplierViewModel == null) return NotFound();
            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [ClaimsAuthorize("Supplier", "Delete")]
        [Route("remover-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierWithAddress(id);
            if (supplierViewModel == null) return NotFound();
            await _service.Delete(id);

            if (!ValidOperation())
                return View(supplierViewModel);

            TempData["Success"] = "Fornecedor excluido com sucesso!";

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Supplier", "Edit")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await GetSupplierWithAddress(id);
            if (supplier == null)
                return NotFound();
            return PartialView("_UpdateAddress", new SupplierViewModel { Address = supplier.Address });
        }

        [AllowAnonymous]
        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierWithAddress(id);
            if (supplier == null)
                return NotFound();
            return PartialView("_AddressDetails", supplier);
        }

        [ClaimsAuthorize("Supplier", "Edit")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");
            if (!ModelState.IsValid)
                return PartialView("_UpdateAddress", supplierViewModel);
            await _service.UpdateAddress(_mapper.Map<Address>(supplierViewModel.Address));

            if (!ValidOperation())
                return PartialView("_UpdateAddress", supplierViewModel);

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