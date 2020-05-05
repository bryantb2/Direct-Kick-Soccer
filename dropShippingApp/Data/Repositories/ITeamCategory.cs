using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamCategory
    {
        List<Category> GetCategories { get; }
        Category GetCategoryById(int categoryId);
        Task UpdateCategory(Category category);
        Task AddCategory(Category category);
        Task<Category> RemoveCategory(int categoryId);
    }
}
