using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface ITagRepo
    {
        Task AddTag(ProductTag tag);
        Task UpdateTag(ProductTag updatedTag);
        Task<List<ProductTag>> GetAllTags();
        Task<ProductTag> GetTagById(int id);
        Task<ProductTag> DeleteTag(int id);
    }
}
