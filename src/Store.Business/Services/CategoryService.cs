using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository, INotificator notificator)
            : base(notificator)
        {
            _repository = repository;
        }

        public async Task Add(Category category)
        {
            if (!Validate(new CategoryValidation(), category))
                return;

            await _repository.Add(category);
        }

        public async Task Update(Category category)
        {
            if (!Validate(new CategoryValidation(), category))
                return;

            await _repository.Update(category);
        }

        public async Task Delete(Guid id)
        {
            if (_repository.Get(id).Result.Products.Any())
            {
                Notify("Não é possível excluir uma categoria utilizada em produtos cadastrados");
                return;
            }

            await _repository.Delete(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
