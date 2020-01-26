using Microsoft.EntityFrameworkCore;
using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Data.Context;
using System;
using System.Threading.Tasks;

namespace Store.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(StoreDbContext context) : base(context) { }

        public async Task<Supplier> GetWithAddress(Guid id)
        {
            return await Context.Suppliers.AsNoTracking()
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetWithAddressAndProducts(Guid id)
        {
            return await Context.Suppliers.AsNoTracking()
                .Include(s => s.Address)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
