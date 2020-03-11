using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public class FakeRProductRepo : IRProductRepo
    {
        public FakeRProductRepo()
        {
        }

        private List<PricingHistory> ph = new List<PricingHistory>();
        public List<PricingHistory> Ph { get { return ph; } }

        public  Task<int> AddPriceHistAsync(PricingHistory prh)
        {
            int success = 0;
            if (prh != null)
            {
                Ph.Add(prh);
                success = 1;
            }

            return Task.FromResult<int>(success);
        }

        public Task<PricingHistory> RemovePriceHistAsync(int? id)
        {
           
            PricingHistory oldprice = new PricingHistory();
            if (id.HasValue)
            {
                oldprice = Ph.Find(l => l.PricingHistoryID == id) ;
                Ph.RemoveAll(x => x.PricingHistoryID == id);
               
            } 
           
            
               
            

            return Task.FromResult<PricingHistory>(oldprice);

        }

        public Task<IQueryable<PricingHistory>> GetAllPriceHistAsync()
        {
            return Task.FromResult<IQueryable<PricingHistory>>(ph.AsQueryable<PricingHistory>());
        }
        public bool PriceHistExists(int id)
        {
            return Ph.Exists(l => l.PricingHistoryID == id);
        }

    }
}
