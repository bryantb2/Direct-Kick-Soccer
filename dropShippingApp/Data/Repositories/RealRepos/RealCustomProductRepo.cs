using dropShippingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealCustomProductRepo : ICustomProductRepo
    {
        private ApplicationDbContext context;
        public List<CustomProduct> CustomProducts { get { return context.CustomProducts.Include(p=>p.BaseProduct)
                                                                                        .Include(p=>p.ProductTags)
                                                                                        .Include(p=>p.PricingHistory)
                                                                                        .ToList(); 
            } }

 
        public RealCustomProductRepo(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }

        // methods
        public async Task AddCustomProduct(CustomProduct newProduct)
        {
            context.CustomProducts.Add(newProduct);
            await context.SaveChangesAsync();
        }

        public List<CustomProduct> GetAllCustomProducts()
        {
            return this.context.CustomProducts
                .Include(product => product.ProductTags)
                .Include(product => product.PricingHistory)
                .Include(product => product.BaseProduct)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.Category)
                .ToList();
        }

        public async Task<CustomProduct> GetCustomProductById(int customProductId)
        {
            return this.context.CustomProducts
                .Include(product => product.ProductTags)
                .Include(product => product.PricingHistory)
                .Include(product => product.BaseProduct)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.Category)
                .ToList()
                .Find(product => product.CustomProductID == customProductId);
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
                context.CustomProducts.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
