using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Controllers
{
    public class TeamController : Controller
    {
        ITeamRepo teamRepo;
        public TeamController(ITeamRepo t)
        {
            teamRepo = t;
        }

        public async Task<IActionResult> ViewTeam(int teamId)
        {
            // TODO
            // returns specific team page
            return View();
        }

        public async Task<IActionResult> BrowseTeams()
        {
            // TODO
            // returns team results page 
            var teamList = await teamRepo.GetTeams;
            return View(teamList);
        }

        public async Task<IActionResult> SearchTeams(string searchTerm)
        {
            // TODO
            // returns team results page (will have view model with search term)
            var teamList = await teamRepo.GetTeams;
            List<Team> searchResults = SearchListForMatches(teamList, searchTerm);
            return View(searchResults);
        }

        // private stat and searching methods
        private List<Team> SearchListForMatches(List<Team> teams, string searchTerm)
        {
            List<Team> searchResults = new List<Team>();
            foreach(Team t in teams)
            {
                // search on team name
                if (t.TeamName.ToUpper().Contains(searchTerm.ToUpper()))
                    searchResults.Add(t);
                foreach(TeamTag tag in t.TeamTags)
                {
                    // search on product tags
                    if (tag.TagLine.ToUpper().Contains(searchTerm.ToUpper()))
                        searchResults.Add(t);
                }
            }
            return searchResults;
        }
    }
}
