using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface ISizeRepo
    {
        Task AddSize(ProductSize s);
        Task<ProductSize> RemoveSize(int sizeId);
        Task UpdateSize(ProductSize size);
        Task<List<ProductSize>> GetAll();
        Task<ProductSize> GetById(int id);
    }
}
