using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealSortRepo : ISortRepo
    {
        private ApplicationDbContext context;
        public RealSortRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<Sort> ProductSorts
        {
            get
            {
                return this.context.ProductSorts.ToList();
            }
        }

        public Sort GetSortById(int sortId)
        {
            var Sort = this.context.ProductSorts.ToList().Find(team => team.SortID == sortId);
            return Sort;
        }

        public async Task AddSort(Sort sort)
        {
            this.context.ProductSorts.Add(sort);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateSort(Sort team)
        {
            this.context.ProductSorts.Update(team);
            await this.context.SaveChangesAsync();
        }

        public async Task<Sort> RemoveSort(int sortId)
        {
            var Sort = this.context.ProductSorts.ToList().Find(team => team.SortID == sortId);
            this.context.ProductSorts.Remove(Sort);
            await this.context.SaveChangesAsync();
            return Sort;
        }
    }
}
