using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Store.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        public async Task Add(Product product)
        {
            if (!Validate(new ProductValidation(), product))
                return;


        }

        public async Task Update(Product product)
        {
            if (!Validate(new ProductValidation(), product))
                return;


        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
