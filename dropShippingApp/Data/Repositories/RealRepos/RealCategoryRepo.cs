using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealCategoryRepo : ICategoryRepo
    {
        private ApplicationDbContext context;

        public RealCategoryRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<ProductCategory> GetCategories 
        {
            get 
            {
                return this.context.Categories.ToList();
            }
        }

        public async Task<ProductCategory> GetCategoryById(int categoryId)
        {
            return this.context.Categories.ToList()
                .Find(category => category.ProductCategoryID == categoryId);
        }

        public async Task AddCategory(ProductCategory category)
        {
            this.context.Categories.Add(category);
            await this.context.SaveChangesAsync();
        }

        public async Task<ProductCategory> RemoveCategory(int categoryId)
        {
            var foundCategory = this.context.Categories.ToList()
                .Find(category => category.ProductCategoryID == categoryId);
            this.context.Categories.Remove(foundCategory);
            await this.context.SaveChangesAsync();
            return foundCategory;
        }

        public async Task UpdateCategory(ProductCategory category)
        {
            this.context.Categories.Update(category);
            await this.context.SaveChangesAsync();
        }
    }
}
