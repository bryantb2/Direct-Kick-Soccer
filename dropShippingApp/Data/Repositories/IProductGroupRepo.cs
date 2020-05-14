using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IProductGroupRepo
    {
        List<ProductGroup> Groups { get; }
        ProductGroup GetGroupById(int groupId);
        ProductGroup GetGroupByProductId(int productId);
        Task UpdateProductGroup(ProductGroup group);
        Task AddProductGroup(ProductGroup group);
        Task<ProductGroup> RemoveProductGroup(int groupId);
    }
}
