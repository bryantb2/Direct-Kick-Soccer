using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeTeamRepo : ITeamRepo
    {
        // private fields
        private List<Team> teams = new List<Team>();

        public List<Team> GetTeams 
        { 
            get 
            { 
                return this.teams; 
            } 
        }

        public async Task AddTeam(Team team)
        {
            teams.Add(team);
        }

        public async Task<Team> RemoveTeam(int teamId)
        {
            var foundTeam = teams.Find(team => team.TeamID == teamId);
            if(foundTeam != null)
            {
                teams.Remove(foundTeam);
                return await Task.FromResult<Team>(foundTeam);
            }
            else
            {
                return await Task.FromResult<Team>(null);
            }
        }

        public async Task UpdateTeam(Team team)
        {
            // remove team and then add it back
            var foundTeam = teams.Find(tm => tm.TeamID == team.TeamID);
            teams.Remove(foundTeam);
            teams.Add(team);
        }

        public async Task<Team> FindTeamById(int teamId)
        {
            var foundTeam = teams.Find(team => team.TeamID == teamId);
            if(foundTeam != null)
            {
                return await Task.FromResult<Team>(foundTeam);
            }
            else
            {
                return await Task.FromResult<Team>(null);
            }
        }

        public async Task<Team> FindTeamByProductId(int productId)
        {
            // will find custom product in team
            //var groupList = new List<ProductGroup>();

            Team foundTeam = null;
            IEnumerable<CustomProduct> cp;
          
            foreach (Team t in teams)
            {
                cp = t.ProductGroups.SelectMany(x => x.ChildProducts);
                


                foreach(CustomProduct p in cp)
                {
                  
                  
                    if (p.CustomProductID == productId)
                    {
                        foundTeam = t;
                        break;
                    }
                }
                if (foundTeam != null)
                    break;

              
            }
           
            return await Task.FromResult<Team>(foundTeam);
        }

        public async Task MarkInactiveById(int teamId)
        {
            var foundTeam = teams.Find(team => team.TeamID == teamId);
            if (foundTeam != null)
            {
                foundTeam.IsTeamInactive = true;
            }
        }
    }
}
