using Store.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetListBySupplier(Guid supplierId);
        Task<IEnumerable<Product>> GetListWithSuppliers();
        Task<Product> GetWithSupplier(Guid id);
    }
}
