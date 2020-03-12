using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamRepo
    {
        Task AddTeam(Team team);
        Task<Team> RemoveTeam(int teamId);
        Task UpdateTeam(Team team);
        Task<Team> FindTeamById(int teamId);
        Task<Team> FindTeamByProductId(int productId);
        Task MarkInactiveById(int teamId);
        Task<List<Team>> GetTeams { get; }
    }
}
