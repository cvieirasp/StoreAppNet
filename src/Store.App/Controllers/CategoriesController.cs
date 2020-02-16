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
    public class CategoriesController : BaseController
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, ICategoryService service, IMapper mapper, INotificator notificator)
            :base (notificator)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }


        // GET: Categories
        [Route("lista-de-categorias")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _repository.GetAll()));
        }

        // GET: Categories/Details/5
        [Route("dados-da-categoria/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            return View(categoryViewModel);
        }

        // GET: Categories/Create
        [ClaimsAuthorize("Category", "Add")]
        [Route("nova-categoria")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [ClaimsAuthorize("Category", "Add")]
        [Route("nova-categoria")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);
            var category = _mapper.Map<Category>(categoryViewModel);
            await _service.Add(category);

            if (!ValidOperation())
                return View(categoryViewModel);

            return RedirectToAction("Index");
        }

        // GET: Categories/Edit/5
        [ClaimsAuthorize("Category", "Edit")]
        [Route("editar-categoria/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        [ClaimsAuthorize("Category", "Edit")]
        [Route("editar-categoria/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(categoryViewModel);
            var category = _mapper.Map<Category>(categoryViewModel);
            await _service.Update(category);

            if (!ValidOperation())
                return View(categoryViewModel);

            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        [ClaimsAuthorize("Category", "Delete")]
        [Route("remover-categoria/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            return View(categoryViewModel);
        }

        // POST: Categories/Delete/5
        [ClaimsAuthorize("Category", "Delete")]
        [Route("remover-categoria/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            await _service.Delete(id);

            if (!ValidOperation())
                return View(categoryViewModel);

            TempData["Success"] = "Categoria excluida com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
