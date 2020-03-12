using dropShippingApp.Data.Repositories;
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

        public async Task<IActionResult> PopularItems()
        {

        }

        public async Task<IActionResult> BrowseTeams()
        {
            // TODO
            // returns team results page 
            return View();
        }

        public async Task<IActionResult> SearchTeams(string searchTerm)
        {
            // TODO
            // returns team results page (will have view model with search term)
            return View();
        }
    }
}
