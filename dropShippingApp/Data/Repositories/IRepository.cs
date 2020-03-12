using System.Threading.Tasks;
using System.Linq;
using dropShippingApp.Models;
namespace dropShippingApp.Data

{
    public interface IRepository
    {
        public Task<int> AddRosterProdAsync(RosterProduct rp);
        public Task<RosterProduct> RemoveRosterProdAsync(int? id);

        public Task<IQueryable<RosterProduct>> GetAllRosterProdAsync();

        public bool RosterProdExists(int id);

    }
}
