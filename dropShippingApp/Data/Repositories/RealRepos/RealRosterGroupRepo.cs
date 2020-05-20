using dropShippingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealRosterGroupRepo : IRosterGroupRepo
    {
        private ApplicationDbContext context;
        public RealRosterGroupRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<RosterGroup> ProductGroups
        { 
            get
            {
                return this.context.RosterGroups
                    .Include(group => group.Category)
                    .Include(group => group.ProductTags)
                    .ToList();
            }
        }

        public RosterGroup GetGroupById(int groupId)
        {
            return this.context.RosterGroups
                .Include(group => group.Category)
                .Include(group => group.ProductTags)
                .ToList()
                .Find(group => group.RosterGroupID == groupId);
        }

        public async Task AddGroup(RosterGroup group)
        {
            this.context.RosterGroups.Add(group);
            await this.context.SaveChangesAsync();
        }

        public async Task<RosterGroup> RemoveGroup(int groupId)
        {
            var foundGroup = this.context.RosterGroups.ToList()
                .Find(group => group.RosterGroupID == groupId);
            this.context.RosterGroups.Remove(foundGroup);
            await this.context.SaveChangesAsync();
            return foundGroup;
        }

        public async Task UpdateGroup(RosterGroup group)
        {
            this.context.RosterGroups.Update(group);
            await this.context.SaveChangesAsync();
        }
    }
}
