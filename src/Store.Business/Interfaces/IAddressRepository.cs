using Store.Business.Models;
using System;
using System.Threading.Tasks;

namespace Store.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetBySupplier(Guid supplierId);
    }
}
