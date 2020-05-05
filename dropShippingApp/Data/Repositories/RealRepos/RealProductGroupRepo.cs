using dropShippingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public class RealProductGroupRepo : IProductGroupRepo
    {
        private ApplicationDbContext context;
        public RealProductGroupRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<ProductGroup> Groups 
        {
            get
            {
                return this.context.ProductGroups
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.ProductTags)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.PricingHistory)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(rosterProduct => rosterProduct.BaseColor)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(rosterProduct => rosterProduct.BasePrice)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(rosterProduct => rosterProduct.BaseSize)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(rosterProduct => rosterProduct.Category)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(rosterProduct => rosterProduct.PricingHistory)
                    .ToList();
            }
        }

        public ProductGroup GetGroupById(int groupId)
        {
            return this.context.ProductGroups.ToList().Find(group => group.ProductGroupID == groupId);
        }

        public async Task UpdateProductGroup(ProductGroup group)
        {
            this.context.ProductGroups.Update(group);
            await this.context.SaveChangesAsync();
        }

        public async Task AddProductGroup(ProductGroup group)
        {
            this.context.ProductGroups.Add(group);
            await this.context.SaveChangesAsync();
        }

        public async Task<ProductGroup> RemoveProductGroup(int groupId)
        {
            var foundGroup = this.context.ProductGroups.ToList()
                .Find(group => group.ProductGroupID == groupId);
            this.context.ProductGroups.Remove(foundGroup);
            await this.context.SaveChangesAsync();
            return foundGroup;
        }
    }
}
