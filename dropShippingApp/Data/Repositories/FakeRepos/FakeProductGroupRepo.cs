using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeProductGroupRepo : IProductGroupRepo
    {
        private List<ProductGroup> productGroups = new List<ProductGroup>();
    

        public List<ProductGroup> Groups
        {
            get { return this.productGroups; }
        }

        public async Task AddProductGroup(ProductGroup group)
        {
            productGroups.Add(group);
        }

        public ProductGroup GetGroupById(int groupId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductGroup> GetGroupByIdAsync(int groupId)
        {
            ProductGroup pg = productGroups.Find(p => p.ProductGroupID == groupId);
            if(pg!=null)
            {
                productGroups.Remove(pg);
                return await Task.FromResult<ProductGroup>(pg);
            }
            else
            {
                return await Task.FromResult<ProductGroup>(null);
            }
        }

        public ProductGroup GetGroupByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductGroup> GetGroupByProductIdAsync(int productId)
        {
            //will this work? query the list in the product groups
            //ProductGroup pg = productGroups.Find(p => p.ChildProducts
            //.Where(c => c.CustomProductID == productId)
            //.Select(c => c.CustomProductID)
            //.Contains(p.ProductGroupID));

            //if(pg!=null)
            //{
            //    return await Task.FromResult<ProductGroup>(pg);
            //}
            //else
            //{
            //    return await Task.FromResult<ProductGroup>(null);
            //}
            throw new NotImplementedException();
        }

        public async Task<ProductGroup> RemoveProductGroup(int groupId)
        {
            ProductGroup result = productGroups.Find(p => p.ProductGroupID == groupId);
            if (result != null)
            {
                productGroups.Remove(result);
                return await Task.FromResult<ProductGroup>(result);
            }
            else
                return await Task.FromResult<ProductGroup>(null);
        }

        public async Task UpdateProductGroup(ProductGroup group)
        {
            ProductGroup result = productGroups.Find(p => p.ProductGroupID == group.ProductGroupID);
            productGroups.Remove(result);
            productGroups.Add(group);
        }

    
    }
}
