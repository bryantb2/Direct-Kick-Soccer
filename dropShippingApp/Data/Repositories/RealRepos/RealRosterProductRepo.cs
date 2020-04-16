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
                return this.context.RosterProducts.ToList();
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
                .Include(p => p.ModelNumber)
                .Include(p => p.BasePrice)
                .Include(p => p.AddOnPrice)
                .Include(p => p.IsProductActive)
                .Include(p => p.PricingHistory)
                .ThenInclude(r => r.DateChanged)
                .Include(p => p.PricingHistory)
                .ThenInclude(r => r.NewPrice)
                .ToList().Find(id => id.RosterProductID == rosterProductId);
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
