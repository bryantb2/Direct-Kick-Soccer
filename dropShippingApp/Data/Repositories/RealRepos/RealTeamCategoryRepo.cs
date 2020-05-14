using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealTeamCategoryRepo : ITeamCategoryRepo
    {
        private ApplicationDbContext context;
        public RealTeamCategoryRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<TeamCategory> GetCategories
        {
            get
            {
                return this.context.TeamCategories.ToList();
            }
        }

        public TeamCategory GetCategoryById(int categoryId)
        {
            return this.context.TeamCategories.ToList()
                .Find(category => category.TeamCategoryID == categoryId);
        }

        public async Task AddCategory(TeamCategory category)
        {
            this.context.Add(category);
            await this.context.SaveChangesAsync();
        }

        public async Task<TeamCategory> RemoveCategory(int categoryId)
        {
            var foundCategory = this.context.TeamCategories.ToList()
                .Find(category => category.TeamCategoryID == categoryId);
            this.context.TeamCategories.Remove(foundCategory);
            await this.context.SaveChangesAsync();
            return foundCategory;
        }

        public async Task UpdateCategory(TeamCategory category)
        {
            this.context.TeamCategories.Update(category);
            await this.context.SaveChangesAsync();
        }
    }
}
