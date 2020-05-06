﻿using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealProductCategoryRepo : IProductCategoryRepo
    {
        private ApplicationDbContext context;

        public RealProductCategoryRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<Category> GetCategories 
        {
            get 
            {
                return this.context.ProductCategories.ToList();
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            return this.context.ProductCategories.ToList()
                .Find(category => category.CategoryID == categoryId);
        }

        public async Task AddCategory(Category category)
        {
            this.context.ProductCategories.Add(category);
            await this.context.SaveChangesAsync();
        }

        public async Task<Category> RemoveCategory(int categoryId)
        {
            var foundCategory = this.context.ProductCategories.ToList()
                .Find(category => category.CategoryID == categoryId);
            this.context.ProductCategories.Remove(foundCategory);
            await this.context.SaveChangesAsync();
            return foundCategory;
        }

        public async Task UpdateCategory(Category category)
        {
            this.context.ProductCategories.Update(category);
            await this.context.SaveChangesAsync();
        }
    }
}