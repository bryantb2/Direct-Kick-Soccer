using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealPricingRepo : IPricingRepo
    {
        private ApplicationDbContext context;

        public RealPricingRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<PricingHistory> GetAllHistory
        {
            get
            {
                return this.context.PricingHistories.ToList();
            }
        }

        public async Task<PricingHistory> GetHistoryById(int historyId)
        {
            var foundHistory = this.context.PricingHistories.ToList()
                .Find(pHistory => pHistory.PricingHistoryID == historyId);
            return foundHistory;
        }

        public async Task AddHistory(PricingHistory history)
        {
            this.context.PricingHistories.Add(history);
            await this.context.SaveChangesAsync();
        }

        public async Task<PricingHistory> RemoveHistory(int historyId)
        {
            var foundHistory = this.context.PricingHistories.ToList()
                .Find(pHistory => pHistory.PricingHistoryID == historyId);
            this.context.PricingHistories.Remove(foundHistory);
            await this.context.SaveChangesAsync();
            return foundHistory;
        }

        public async Task UpdateHistory(PricingHistory history)
        {
            this.context.PricingHistories.Update(history);
            await this.context.SaveChangesAsync();
        }
    }
}
