using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class FakeTagRepo : ITagRepo
    {
        private List<ProductTag> tags = new List<ProductTag>();

        public async Task AddTag(ProductTag tag)
        {
            tags.Add(tag);
        }

        public async Task<ProductTag> CreateTag(string text)
        {

            ProductTag t = new ProductTag
            {
                TagLine = text
            };
            tags.Add(t);
            return await Task.FromResult<ProductTag>(t);
        }

        public async Task<ProductTag> DeleteTag(int id)
        {

            try
            {
                ProductTag t = (from tag in tags
                                where tag.ProductTagID == id
                                select tag).First();
                tags.Remove(t);
                return await Task.FromResult<ProductTag>(t);
            }
            catch
            {
                return await Task.FromResult<ProductTag>(null);
            }
         
        }

        public async Task UpdateTag(ProductTag updatedTag)
        {
            ProductTag  oldTag = (from tag in tags
                            where tag.ProductTagID == updatedTag.ProductTagID
                            select tag).First();
            tags.Remove(oldTag);
            tags.Add(updatedTag);
        }
    }
}
