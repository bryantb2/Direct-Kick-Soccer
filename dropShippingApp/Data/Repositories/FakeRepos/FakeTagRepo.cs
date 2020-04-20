using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class FakeTagRepo //: ITagRepo
    {
        private List<Tag> tags = new List<Tag>();

        public async Task AddTag(Tag tag)
        {
            tags.Add(tag);
        }

        public async Task<Tag> CreateTag(string text)
        {

            Tag t = new Tag
            {
                TagLine = text
            };
            tags.Add(t);
            return await Task.FromResult<Tag>(t);
        }

        public async Task<Tag> DeleteTag(int id)
        {

            try
            {
                Tag t = (from tag in tags
                                where tag.TagID == id
                                select tag).First();
                tags.Remove(t);
                return await Task.FromResult<Tag>(t);
            }
            catch
            {
                return await Task.FromResult<Tag>(null);
            }
         
        }

        public async Task UpdateTag(Tag updatedTag)
        {
            Tag oldTag = (from tag in tags
                            where tag.TagID == updatedTag.TagID
                            select tag).First();
            tags.Remove(oldTag);
            tags.Add(updatedTag);
        }
    }
}
