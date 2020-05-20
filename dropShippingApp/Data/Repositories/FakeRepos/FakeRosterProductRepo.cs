using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeRosterProductRepo : IRosterProductRepo
    {
        private List<RosterProduct> products=new List<RosterProduct>();
   
        public List<RosterProduct> GetRosterProducts
        {
            get { return this.products; }
        }

        public async Task AddRosterProduct(RosterProduct newProduct)
        {
            products.Add(newProduct);
        }

        public async Task<RosterProduct> GetRosterProductById(int rosterProductId)
        {
            RosterProduct prod = (from p in products
                                  where p.RosterProductID == rosterProductId
                                  select p).FirstOrDefault();
            if(prod!=null)
            {
                products.Remove(prod);
                return await Task.FromResult<RosterProduct>(prod);
            }
            else
            {
                return await Task.FromResult<RosterProduct>(null);
            }
        }

        public async Task<RosterProduct> RemoveRosterProduct(int productID)
        {
            RosterProduct prod = (from p in products
                                 where p.RosterProductID == productID
                                  select p).FirstOrDefault();
            if (prod != null)
            {
                products.Remove(prod);
                return await Task.FromResult<RosterProduct>(prod);
            }
            else
                return await Task.FromResult<RosterProduct>(null);
        }

        public async Task UpdateRosterProduct(RosterProduct updatedProduct)
        {
            RosterProduct prod = products.Find(p => p.RosterProductID == updatedProduct.RosterProductID);
            products.Remove(prod);
            products.Add(updatedProduct);
        }
    }
}
