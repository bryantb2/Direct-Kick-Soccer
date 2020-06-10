using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamRepo
    {
        List<Team> GetTeams { get; }
        Task AddTeam(Team team);
        Task<Team> RemoveTeam(int teamId);
        Task UpdateTeam(Team team);
        Team FindTeamById(int teamId);
        Team FindTeamByProductId(int productId);
        Task MarkInactiveById(int teamId);
    }
}
