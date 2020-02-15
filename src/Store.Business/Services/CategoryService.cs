using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Store.Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public async Task Add(Category category)
        {
            if (!Validate(new CategoryValidation(), category))
                return;


        }

        public async Task Update(Category category)
        {
            if (!Validate(new CategoryValidation(), category))
                return;


        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
