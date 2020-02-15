using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Store.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository, INotificator notificator)
            : base(notificator)
        {
            _repository = repository;
        }

        public async Task Add(Product product)
        {
            if (!Validate(new ProductValidation(), product))
                return;

            await _repository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!Validate(new ProductValidation(), product))
                return;

            await _repository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
