using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ICategoryRepo
    {
        List<Category> GetCategories { get; }
        Category GetCategoryById(int categoryId);
        Task AddCategory(Category category);
        Task<Category> RemoveCategory(int categoryId);
        Task UpdateCategory(Category category);
    }
}
