using Store.Business.Models;
using System;
using System.Threading.Tasks;

namespace Store.Business.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(Guid id);
    }
}
