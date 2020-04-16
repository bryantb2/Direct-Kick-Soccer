using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
     public interface IProductColorRepo
    {
        Task AddColor(ProductColor c);
        Task<ProductColor> RemoveColor(int colorId);
        Task UpdateColor(ProductColor color);
        Task<List<ProductColor>> GetAllColors();
        Task<ProductColor> GetById(int id);
    }
}
