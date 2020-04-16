using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealSizeRepo : ISizeRepo
    {
        private ApplicationDbContext context;

        public IQueryable<ProductSize> Sizes
        {
            get { return context.ProductSizes; }
        }
        public RealSizeRepo(ApplicationDbContext applicationDbcontext)
        {
            context = applicationDbcontext;
        }
        public async Task AddSize(ProductSize s)
        {
            await context.ProductSizes.AddAsync(s);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductSize>> GetAll()
        {
             List <ProductSize> sizes = (from s in Sizes
                                         select s).ToList();
            return await Task.FromResult<List<ProductSize>>(sizes);
        }

        public async Task<ProductSize> GetById(int id)
        {
            try
            {
                ProductSize s = (from size in Sizes
                                 where size.ProductSizeID == id
                                select size).FirstOrDefault();
                return await Task.FromResult<ProductSize>(s);
            }
            catch
            {
                return await Task.FromResult<ProductSize>(null);
            }
        }

        public async Task<ProductSize> RemoveSize(int sizeId)
        {
            try
            {
                ProductSize size = (from s in Sizes
                                    where s.ProductSizeID == sizeId
                                 select s).First();
                context.Remove(size);
                await context.SaveChangesAsync();
                return await Task.FromResult<ProductSize>(size);
            }
            catch
            {
                return await Task.FromResult<ProductSize>(null);
            }
        }

        public async Task UpdateSize(ProductSize size)
        {
            context.Update(size);
            await context.SaveChangesAsync();
        }
    }
}
