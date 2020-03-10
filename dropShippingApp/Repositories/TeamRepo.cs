using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Repositories
{
    public class TeamRepo : ITeamRepo
    {
        private List<Team> teams = new List<Team>();
        public async Task<Team> AddTeamAsync(Team team)
        {
            return team;
        }

        public async Task<int> RemoveTeamAsync(int teamId)
        {
            var foundTeam = teams.Find(team => team.TeamID == teamId);
            teams.Remove(foundTeam);
            return foundTeam;
        }

        public async Task<Team> UpdateTeamAsync(Team team)
        {

        }
    }
}
