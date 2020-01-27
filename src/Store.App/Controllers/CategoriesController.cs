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
    public class CategoriesController : BaseController
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _repository.GetAll()));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            return View(categoryViewModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);
            var category = _mapper.Map<Category>(categoryViewModel);
            await _repository.Add(category);
            return RedirectToAction("Index");
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(categoryViewModel);
            var category = _mapper.Map<Category>(categoryViewModel);
            await _repository.Update(category);
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            return View(categoryViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _repository.Get(id));
            if (categoryViewModel == null) return NotFound();
            await _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
