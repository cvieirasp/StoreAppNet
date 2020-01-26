using Store.Business.Models;
using System;
using System.Threading.Tasks;

namespace Store.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetWithAddress(Guid id);
        Task<Supplier> GetWithAddressAndProducts(Guid id);
    }
}
