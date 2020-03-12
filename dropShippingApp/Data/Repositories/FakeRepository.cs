using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public class FakeRepository : IRepository
    {
        public FakeRepository()
        {
        }

        private List<RosterProduct> rp = new List<RosterProduct>();
        public List<RosterProduct> Rp { get { return rp; } }

        public  Task<int> AddRosterProdAsync(RosterProduct prh)
        {
            int success = 0;
            if (prh != null)
            {
                Rp.Add(prh);
                success = 1;
            }

            return Task.FromResult<int>(success);
        }

        public Task<RosterProduct> RemoveRosterProdAsync(int? id)
        {

            RosterProduct oldproduct = new RosterProduct();
            if (id.HasValue)
            {
                oldproduct = Rp.Find(l => l.RosterProductID == id) ;
                Rp.RemoveAll(x => x.RosterProductID == id);
               
            }


            return Task.FromResult<RosterProduct>(oldproduct);





        }

        public Task<IQueryable<RosterProduct>> GetAllRosterProdAsync()
        {
            return Task.FromResult<IQueryable<RosterProduct>>(rp.AsQueryable<RosterProduct>());
        }
        public bool RosterProdExists(int id)
        {
            return Rp.Exists(l => l.RosterProductID == id);
        }

    }
}
