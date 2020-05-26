using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    /*public class FakeCategoryRepo : ICategoryRepo
    {
        private List<ProductCategory> categories = new List<ProductCategory>();

        public List<ProductCategory> GetCategories
        {
            get
            {
                return categories;
            }        
        }


        public async Task AddCategory(ProductCategory category)
        {
            categories.Add(category);
        }

        public async Task<ProductCategory> GetCategoryById(int categoryId)
        {
            var category = (from c in categories
                            where c.ProductCategoryID == categoryId
                            select c).First();
            return await Task.FromResult<ProductCategory>(category);
        }

        public async Task<ProductCategory> RemoveCategory(int categoryId)
        {
            var category = (from c in categories
                            where c.ProductCategoryID == categoryId
                            select c).First();

            if (category != null)
            {
                categories.Remove(category);
                return await Task.FromResult<ProductCategory>(category);
            }
            else
                return await Task.FromResult<ProductCategory>(null);
        }

        public async Task UpdateCategory(ProductCategory category)
        {
            try
            {
                ProductCategory foundC = (from c in categories
                                          where c.ProductCategoryID == category.ProductCategoryID
                                          select c).FirstOrDefault();

                categories.Remove(foundC);
                categories.Add(category);
            }
            catch
            {
                throw new ArgumentNullException("No Category Found");
            }

        }
    }*/
}
