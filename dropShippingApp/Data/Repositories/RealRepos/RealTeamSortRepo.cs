using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Data.Repositories;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealTeamSortRepo : ITeamSortRepo
    {
        private ApplicationDbContext context;
        public RealTeamSortRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<Sort> Sorts
        {
            get
            {
                return this.context.TeamSorts.ToList();
            }
        }

        public Sort GetSortById(int sortId)
        {
            var Sort = this.context.TeamSorts.ToList().Find(team => team.SortID == sortId);
            return Sort;
        }

        public async Task AddSort(Sort sort)
        {
            this.context.TeamSorts.Add(sort);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateSort(Sort team)
        {
            this.context.TeamSorts.Update(team);
            await this.context.SaveChangesAsync();
        }

        public async Task<Sort> RemoveSort(int sortId)
        {
            var Sort = this.context.TeamSorts.ToList().Find(team => team.SortID == sortId);
            this.context.TeamSorts.Remove(Sort);
            await this.context.SaveChangesAsync();
            return Sort;
        }
    }
}
