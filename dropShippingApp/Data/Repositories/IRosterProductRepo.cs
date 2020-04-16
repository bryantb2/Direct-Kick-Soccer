using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface IRosterProductRepo
    {
       
        List<RosterProduct> GetRosterProducts {get;}
        // CRUD operations for RosterProducts
        Task AddRosterProduct(RosterProduct newProduct);
        Task<RosterProduct> GetRosterProductById(int rosterProductId);
        Task UpdateRosterProduct(RosterProduct updatedProduct);
        Task<RosterProduct> RemoveRosterProduct(int productID);

    }
}
