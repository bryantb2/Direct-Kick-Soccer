using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealProductSortRepo : IProductSortRepo
    {
        private ApplicationDbContext context;
        public RealProductSortRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<ProductSort> Sorts 
        {
            get
            {
                return this.context.ProductSorts.ToList();
            }
        }

        public ProductSort GetSortById(int sortId)
        {
            return this.context.ProductSorts.ToList()
                .Find(sort => sort.ProductSortID == sortId);
        }

        public async Task AddSort(ProductSort sort)
        {
            this.context.ProductSorts.Add(sort);
            await this.context.SaveChangesAsync();
        }

        public async Task<ProductSort> RemoveSortById(int sortId)
        {
            var foundSort = this.context.ProductSorts.ToList().Find(sort => sort.ProductSortID == sortId);
            this.context.ProductSorts.Remove(foundSort);
            await this.context.SaveChangesAsync();
            return foundSort;
        }
    }
}
