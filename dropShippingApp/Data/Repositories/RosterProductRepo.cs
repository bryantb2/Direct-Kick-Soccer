using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public class RosterProductRepo : IRosterProductRepo
    {
        private List<RosterProduct> rosterProducts = new List<RosterProduct>();
        public List<RosterProduct> RosterProducts { get { return rosterProducts; } }

        // methods
        public async Task AddRosterProduct(RosterProduct newProduct)
        {
            RosterProducts.Add(newProduct);
        }

        public async Task<RosterProduct> GetRosterProductById(int rosterProductId)
        {
            RosterProduct foundProduct = RosterProducts.Find(product => product.RosterProductID == rosterProductId);
            if (foundProduct != null)
            {
                return await Task.FromResult<RosterProduct>(foundProduct);
            }
            // Return the Roster product as null if not found
            return await Task.FromResult<RosterProduct>(null);
        }

        public async Task UpdateRosterProduct(RosterProduct updatedProduct)
        {
            RosterProduct oldProduct = RosterProducts.Find(cp => cp.RosterProductID == updatedProduct.RosterProductID);
            RosterProducts.Remove(oldProduct);
            RosterProducts.Add(updatedProduct);
        }

        public async Task RemoveRosterProduct(RosterProduct product)
        {
            if (product != null)
            {
                RosterProducts.Remove(product);
            }
        }
    }
}
