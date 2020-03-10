using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Repositories
{
    public interface ITeamRepo
    {
        Task<Team> AddTeamAsync(Team team);

        Task<int> RemoveTeamAsync(int teamId);

        Task<Team> UpdateTeamAsync(Team team);
    }
}
