using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IRosterGroupRepo
    {
        List<RosterGroup> ProductGroups { get; }
        RosterGroup GetGroupById(int groupId);
        Task UpdateGroup(RosterGroup group);
        Task AddGroup(RosterGroup group);
        Task<RosterGroup> RemoveGroup(int groupId);

    }
}
