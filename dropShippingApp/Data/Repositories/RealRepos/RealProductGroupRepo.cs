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
                    .Include(groups => groups.ProductTags)
                    .Include(groups => groups.ChildProducts)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.PricingHistory)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.ProductPhotoData)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.BaseColor)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.BaseSize)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.Category)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.ProductTags)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.Category)
                    .ToList();
            }
        }

        public ProductGroup GetGroupById(int groupId)
        {
            return this.context.ProductGroups
                    .Include(groups => groups.ProductTags)
                    .Include(groups => groups.ChildProducts)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.PricingHistory)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.ProductPhotoData)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.BaseColor)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.BaseSize)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.Category)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.ProductTags)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.Category)
                    .ToList()
                    .Find(group => group.ProductGroupID == groupId);
        }

        public ProductGroup GetGroupByProductId(int productId)
        {
            var foundCustomProduct = this.context.CustomProducts.ToList()
                .Find(product => product.CustomProductID == productId);
            // search each group and return if it contains the product id
            var foundGroup = this.context.ProductGroups
                    .Include(groups => groups.ProductTags)
                    .Include(groups => groups.ChildProducts)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.PricingHistory)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.ProductPhotoData)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.BaseColor)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.BaseSize)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.Category)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.ProductTags)
                    .Include(groups => groups.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(group => group.Category)
                    .ToList()
                    .Find(group => group.ChildProducts.Contains(foundCustomProduct) == true);
            return foundGroup;
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
