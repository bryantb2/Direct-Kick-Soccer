using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ICategoryRepo
    {
        List<ProductCategory> GetCategories { get; }
        Task<ProductCategory> GetCategoryById(int categoryId);
        Task AddCategory(ProductCategory category);
        Task<ProductCategory> RemoveCategory(int categoryId);
        Task UpdateCategory(ProductCategory category);
    }
}
