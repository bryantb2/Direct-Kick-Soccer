using dropShippingApp.Models;
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
                return this.context.ProductGroups.ToList();
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
