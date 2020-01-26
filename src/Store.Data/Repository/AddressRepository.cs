using Microsoft.EntityFrameworkCore;
using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Data.Context;
using System;
using System.Threading.Tasks;

namespace Store.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(StoreDbContext context) : base(context) { }

        public async Task<Address> GetBySupplier(Guid supplierId)
        {
            return await Context.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(a => a.SupplierID == supplierId);
        }
    }
}
