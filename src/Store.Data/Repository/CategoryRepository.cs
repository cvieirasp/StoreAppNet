using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Data.Context;

namespace Store.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context) { }
    }
}
