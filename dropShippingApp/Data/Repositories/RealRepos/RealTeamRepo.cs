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

        public List<Team> GetTeams
        {
            get
            {
                return this.context.Teams
                    // basic team property objects
                    .Include(team => team.BannerImageData)
                    .Include(team => team.Providence)
                    .Include(team => team.Category)
                    .Include(team => team.TeamTags)
                    .Include(team => team.ProductGroups)
                    .Include(team => team.Country)
                    .Include(team => team.BannerImageData)
                    // get product group data
                    .Include(team => team.ProductGroups)
                        .ThenInclude(group => group.ProductTags)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.PricingHistory)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.ProductPhotoData)
                    // get base product data
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(baseProduct => baseProduct.Category)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                        .ThenInclude(baseProduct => baseProduct.ProductTags)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseColor)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseSize)
                    .Include(team => team.ProductGroups)
                        .ThenInclude(products => products.ChildProducts)
                        .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.PricingHistory)
                    .ToList();
            }
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

     

        public Team FindTeamById(int teamId)
        {
            return this.context.Teams
                // basic team property objects
                .Include(team => team.Providence)
                .Include(team => team.Category)
                .Include(team => team.TeamTags)
                .Include(team => team.ProductGroups)
                .Include(team => team.Country)
                .Include(team => team.BannerImageData)
                // get product group data
                .Include(team => team.ProductGroups)
                    .ThenInclude(group => group.ProductTags)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.PricingHistory)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.ProductPhotoData)
                // get base product data
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .ThenInclude(baseProduct => baseProduct.Category)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .ToList()
                .Find(team => team.TeamID == teamId);
        }

        public Team FindTeamByProductId(int productId)
        {
            return this.context.Teams
                // basic team property objects
                .Include(team => team.Providence)
                .Include(team => team.Category)
                .Include(team => team.TeamTags)
                .Include(team => team.ProductGroups)
                .Include(team => team.Country)
                .Include(team => team.BannerImageData)
                // get product group data
                .Include(team => team.ProductGroups)
                    .ThenInclude(group => group.ProductTags)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.PricingHistory)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                // get base product data
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .ThenInclude(baseProduct => baseProduct.Category)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(rosterProduct => rosterProduct.RosterGroup)
                    .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(team => team.ProductGroups)
                    .ThenInclude(products => products.ChildProducts)
                    .ThenInclude(product => product.BaseProduct)
                    .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .ToList()
                .Find(team => team.ProductGroups
                    .Find(group => group.ChildProducts.Exists(product => product.CustomProductID == productId))
                != null);
        }

        public async Task MarkInactiveById(int teamId)
        {
            var foundTeam = this.context.Teams.ToList()
                .Find(team => team.TeamID == teamId);
            foundTeam.IsTeamInactive = true;
            this.context.Teams.Update(foundTeam);
            await this.context.SaveChangesAsync();
        }
    }
}
