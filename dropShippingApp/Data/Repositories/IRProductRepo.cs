using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IRProductRepo
    {
        public bool RosterProdExists(int id);
        public Task<int> AddRosterProdAsync(RosterProduct rp);
        public Task<RosterProduct> RemoveRosterProdAsync(int? id);
        public Task<IQueryable<RosterProduct>> GetAllRosterProdAsync();
    }
}
