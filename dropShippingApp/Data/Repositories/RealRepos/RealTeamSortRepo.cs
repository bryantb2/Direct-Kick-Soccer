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

        public List<TeamSort> Sorts
        {
            get
            {
                return this.context.TeamSorts.ToList();
            }
        }

        public TeamSort GetSortById(int sortId)
        {
            var Sort = this.context.TeamSorts.ToList().Find(team => team.SortID == sortId);
            return Sort;
        }

        public async Task AddSort(TeamSort sort)
        {
            this.context.TeamSorts.Add(sort);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateSort(TeamSort team)
        {
            this.context.TeamSorts.Update(team);
            await this.context.SaveChangesAsync();
        }

        public async Task<TeamSort> RemoveSort(int sortId)
        {
            var Sort = this.context.TeamSorts.ToList().Find(team => team.SortID == sortId);
            this.context.TeamSorts.Remove(Sort);
            await this.context.SaveChangesAsync();
            return Sort;
        }
    }
}
