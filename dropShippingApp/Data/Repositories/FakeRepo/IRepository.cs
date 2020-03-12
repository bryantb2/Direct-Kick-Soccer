using System.Threading.Tasks;
using System.Linq;
using dropShippingApp.Models;
namespace dropShippingApp.Data

{
    public interface IRepository
    {
        public Task<int> AddPriceHistAsync(PricingHistory ph);
        public Task<PricingHistory> RemovePriceHistAsync(int? id);

        public Task<IQueryable<PricingHistory>> GetAllPriceHistAsync();

        public bool PriceHistExists(int id);

    }
}
