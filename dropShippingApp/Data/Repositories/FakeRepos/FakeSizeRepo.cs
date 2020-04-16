using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeSizeRepo : ISizeRepo
    {
        private List<ProductSize> sizes = new List<ProductSize>();
        public async Task AddSize(ProductSize s)
        {
            sizes.Add(s);
        }

        public async Task<ProductSize> CreateSize(string sizeName, bool isActive)
        {
            ProductSize p = new ProductSize
            {
                SizeName = sizeName,
                IsSizeActive = isActive
            };
            return await Task.FromResult<ProductSize>(p);
        }

        public async Task<ProductSize> RemoveSize(int sizeId)
        {
            try
            {
                ProductSize s = (from size in sizes
                                 where size.ProductSizeID == sizeId
                                 select size).First();

                return await Task.FromResult<ProductSize>(s);
            }
            catch
            {
                return await Task.FromResult<ProductSize>(null);
            }
        }

        public async Task UpdateSize(ProductSize newSize)
        {
            try
            {
                ProductSize s = (from size in sizes
                                 where size.ProductSizeID == newSize.ProductSizeID
                                 select size).First();

                sizes.Remove(s);
                sizes.Add(newSize);
            }
            catch
            {
                throw new SystemException("Size Not Found");
            }
        }
    }
}
