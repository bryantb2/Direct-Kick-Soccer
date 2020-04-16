using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealColorRepo : IProductColorRepo
    {
        private ApplicationDbContext context;

        public IQueryable<ProductColor> Colors
        {
            get { return context.ProductColors; }
        }
        public RealColorRepo(ApplicationDbContext applicationDbcontext)
        {
            context = applicationDbcontext;
        }
        public async Task AddColor(ProductColor c)
        {
            await context.ProductColors.AddAsync(c);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductColor>> GetAllColors()
        {
            List<ProductColor> colors = (from c in Colors
                                         select c).ToList();
            return await Task.FromResult<List<ProductColor>>(colors);
        }

        public async Task<ProductColor> GetById(int id)
        {
            try
            {
                ProductColor color = (from c in Colors
                                      where c.ProductColorID == id
                                      select c).First();
                return await Task.FromResult<ProductColor>(color);
            }
            catch
            {
                return await Task.FromResult<ProductColor>(null);
            }
        }

        public async Task<ProductColor> RemoveColor(int colorId)
        {
            ProductColor color = (from c in Colors
                                  where c.ProductColorID == colorId
                                  select c).First();
            context.Remove(color);
            await context.SaveChangesAsync();
            return await Task.FromResult<ProductColor>(color);
        }

        public async Task UpdateColor(ProductColor color)
        {
            context.Update(color);
            await context.SaveChangesAsync();
        }
    }
}
