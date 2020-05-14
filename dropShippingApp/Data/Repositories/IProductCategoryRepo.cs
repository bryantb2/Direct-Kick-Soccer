using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IProductCategoryRepo
    {
        List<ProductCategory> GetCategories { get; }
        ProductCategory GetCategoryById(int categoryId);
        Task AddCategory(ProductCategory category);
        Task<ProductCategory> RemoveCategory(int categoryId);
        Task UpdateCategory(ProductCategory category);
    }
}
