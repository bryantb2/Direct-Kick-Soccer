﻿using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
	public class CustomProductRepo : ICustomProductRepo
	{
        private ApplicationDbContext context;
        public List<CustomProduct> CustomProducts { get { return context.CustomProducts.ToList(); } }

        public CustomProductRepo(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }

        // methods
        public async Task AddCustomProduct(CustomProduct newProduct)
        {
            context.CustomProducts.Add(newProduct);
            await context.SaveChangesAsync();
        }

        public List<CustomProduct> GetAllCustomProducts => CustomProducts;

        public async Task<CustomProduct> GetCustomProductById(int customProductId)
        {
            CustomProduct foundProduct = context.CustomProducts.FirstOrDefault(product => product.CustomProductID == customProductId);
            if (foundProduct != null)
            {
                return await Task.FromResult<CustomProduct>(foundProduct);
            }
            // Return the custom product as null if not found
            return await Task.FromResult<CustomProduct>(null);
        }

        public async Task UpdateCustomProduct(CustomProduct updatedProduct)
        {
            this.context.CustomProducts.Update(updatedProduct);
            await context.SaveChangesAsync();
        }

        public async Task RemoveCustomProduct(CustomProduct product)
        {
            // Make sure the product that is passed in is not null
            if (product != null)
            {
                context.CustomProducts.Remove(product);
            }
            await context.SaveChangesAsync();
        }
    }
}
