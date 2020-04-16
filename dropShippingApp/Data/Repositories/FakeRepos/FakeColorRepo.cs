using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeColorRepo : IProductColorRepo
    {
        private List<ProductColor> colors = new List<ProductColor>();
        public async Task AddColor(ProductColor c)
        {
            colors.Add(c);
        }

        public async Task<ProductColor> CreateColor(string colorName, bool isActive)
        {
            ProductColor c = new ProductColor
            {
                ColorName=colorName,
                IsColorActive=isActive
            };
            return await Task.FromResult<ProductColor>(c);
        }

        public async Task<ProductColor> RemoveColor(int colorId)
        {
            try
            {
                ProductColor c = (from color in colors
                                  where color.ProductColorID == colorId
                                  select color).First();
                colors.Remove(c);
                return await Task.FromResult<ProductColor>(c);
            }
            catch
            {
                return await Task.FromResult<ProductColor>(null);
            }
        }

        public async Task UpdateColor(ProductColor color)
        {
            try
            {
                ProductColor c = (from col in colors
                                  where color.ProductColorID == color.ProductColorID
                                  select color).First();
                colors.Remove(c);
                
            }
            catch
            {
                throw new ArgumentException("Color Not Found");
            }
        }
    }
}
