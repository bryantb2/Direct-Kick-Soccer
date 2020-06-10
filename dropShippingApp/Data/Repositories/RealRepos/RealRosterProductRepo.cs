using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;


namespace dropShippingApp.Data.Repositories
{
    public class RealRosterProductRepo : IRosterProductRepo
    {
        private ApplicationDbContext context;

        public RealRosterProductRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<RosterProduct> GetRosterProducts
        {
            get
            {
                return this.context.RosterProducts
                    .Include(product => product.BaseColor)
                    .Include(product => product.BaseSize)
                    .Include(product => product.PricingHistory)
                    .Include(product => product.RosterGroup)
                    .ToList();
            }
        }

        // methods
        public async Task AddRosterProduct(RosterProduct newProduct)
        {
            this.context.RosterProducts.Add(newProduct);
            await this.context.SaveChangesAsync();
        }

        public async Task<RosterProduct> GetRosterProductById(int rosterProductId)
        {
            return this.context.RosterProducts
                .Include(product => product.BaseColor)
                .Include(product => product.BaseSize)
                .Include(product => product.PricingHistory)
                .Include(product => product.RosterGroup)
                .ToList()
                .Find(id => id.RosterProductID == rosterProductId);
        }

        public async Task UpdateRosterProduct(RosterProduct updatedProduct)
        {
            this.context.RosterProducts.Update(updatedProduct);
            await this.context.SaveChangesAsync();
            
        }

        public async Task<RosterProduct> RemoveRosterProduct(int productID)
        {

            var foundRosterProduct = this.context.RosterProducts.ToList()
                .Find(rosterProduct => rosterProduct.RosterProductID == productID);
            this.context.RosterProducts.Remove(foundRosterProduct);
            await this.context.SaveChangesAsync();
            return foundRosterProduct;
        }
    }
}
