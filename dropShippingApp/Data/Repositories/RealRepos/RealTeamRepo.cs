using dropShippingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealTeamRepo : ITeamRepo
    {
        private ApplicationDbContext context;

        public RealTeamRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public async Task AddTeam(Team team)
        {
            this.context.Teams.Add(team);
            await this.context.SaveChangesAsync();
        }

        public async Task<Team> RemoveTeam(int teamId)
        {
            var foundTeam = this.context.Teams.ToList()
                .Find(team => team.TeamID == teamId);
            this.context.Teams.Remove(foundTeam);
            await this.context.SaveChangesAsync();
            return foundTeam;
        }

        public async Task UpdateTeam(Team team)
        {
            this.context.Teams.Update(team);
            await this.context.SaveChangesAsync();
        }

        public async Task<Team> FindTeamById(int teamId)
        {
            return this.context.Teams
                .Include(team => team.TeamTags)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.ProductTags)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.PricingHistory)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.BaseProduct)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(team => team.TeamProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.ProductTags)
                .ToList().Find(team => team.TeamID == teamId);
        }
    }
}
