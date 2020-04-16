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
        }

        public async Task<Tag> GetTagById(int tagId)
        {
            var foundTag = this.context.Tags.ToList()
                .Find(tag => tag.TagID == tagId);
            return foundTag;
        }

        public async Task<Tag> GetTagByName(string name)
        {
            var foundTag = this.context.Tags.ToList()
                .Find(tag => tag.TagLine.ToUpper().Trim() == name.ToUpper().Trim());
            return foundTag;
        }
    }
}
