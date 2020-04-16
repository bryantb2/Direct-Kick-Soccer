using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealTagRepo : ITagRepo
    {

        private ApplicationDbContext context;
        public RealTagRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<Tag> GetTags
        {
            get
            {
                return this.context.Tags.ToList();
            }
        }

        public async Task AddTag(Tag tag)
        {
            this.context.Tags.Add(tag);
            await this.context.SaveChangesAsync();
        }

        public async Task<Tag> RemoveTag(int tagId)
        {
            var foundTag = this.context.Tags.ToList()
                .Find(tag => tag.TagID == tagId);
            this.context.Tags.Remove(foundTag);
            await this.context.SaveChangesAsync();
            return foundTag;
        }
        
        public async Task UpdateTag(Tag tag)
        {
            this.context.Tags.Update(tag);
            await this.context.SaveChangesAsync();

        private  ApplicationDbContext context;

        public IQueryable<ProductTag> Tags
        {
            get { return context.ProductTags; }
        }
        public RealTagRepo(ApplicationDbContext applicationDbcontext)
        {
            context = applicationDbcontext;
        }
        public async Task AddTag(ProductTag tag)
        {
            await context.ProductTags.AddAsync(tag);
            await context.SaveChangesAsync();
        }

        public async Task<ProductTag> DeleteTag(int id)
        {
            try
            {
                ProductTag t = (from tag in Tags
                                where tag.ProductTagID == id
                                select tag).FirstOrDefault();


                context.ProductTags.Remove(t);
                await context.SaveChangesAsync();
                return await Task.FromResult<ProductTag>(t);
            }
            catch
            {
                return await Task.FromResult<ProductTag>(null);
            }

        }

        public async Task<List<ProductTag>> GetAllTags()
        {
            List<ProductTag> tags = (from tag in Tags
                                     select tag).ToList();
            return await Task.FromResult<List<ProductTag>>(tags);
        }

        public async Task<ProductTag> GetTagById(int id)
        {
            try
            {
                ProductTag t = (from tag in Tags
                                where tag.ProductTagID == id
                                select tag).FirstOrDefault();
                return await Task.FromResult<ProductTag>(t);
            }
            catch
            {
                return await Task.FromResult<ProductTag>(null);
            }

            
        }

        public async Task UpdateTag(ProductTag updatedTag)
        {
            context.Update(updatedTag);
            await context.SaveChangesAsync();

        }
    }
}
