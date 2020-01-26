using Microsoft.EntityFrameworkCore;
using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context) { }

        public async Task<Product> GetWithSupplier(Guid id)
        {
            return await Context.Products.AsNoTracking()
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetListBySupplier(Guid supplierId)
        {
            return await Context.Products.AsNoTracking()
                .Where(p => p.SupplierId == supplierId)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetListWithSuppliers()
        {
            return await Context.Products.AsNoTracking()
                .Include(p => p.Supplier)
                .OrderBy(p => p.Name).ToListAsync();
        }
    }
}
