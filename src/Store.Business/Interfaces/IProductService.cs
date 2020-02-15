using Store.Business.Models;
using System;
using System.Threading.Tasks;

namespace Store.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
