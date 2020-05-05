using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var teamSort = this.context.TeamSorts.ToList().Find(team => team.TeamSortID == sortId);
            return teamSort;
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
            var teamSort = this.context.TeamSorts.ToList().Find(team => team.TeamSortID == sortId);
            this.context.TeamSorts.Remove(teamSort);
            await this.context.SaveChangesAsync();
            return teamSort;
        }
    }
}
