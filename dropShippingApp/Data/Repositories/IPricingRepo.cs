using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IPricingRepo
    {
        Task AddHistory(PricingHistory history);
        Task<PricingHistory> RemoveHistory(int historyId);
        Task UpdateHistory(PricingHistory history);
        Task<PricingHistory> GetHistoryById(int historyId);
        List<PricingHistory> GetAllHistory { get; }
    }
}
