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

        }

        public async Task<IActionResult> SearchTeams(string searchTerm)
        {
            // TODO
            // returns team results page
            return View();
        }

        public async Task<IActionResult> ManageTeamProducts()
        {
            // TODO
            // returns team product management page
            return View();
        }

        public async Task<IActionResult> AddTeamProduct()
        {
            // TODO
            // redirects to team product management page
            return View();
        }

        public async Task<IActionResult> UpdateTeamProduct()
        {
            // TODO
            // redirects to team product management page
            return View();
        }

        public async Task<IActionResult> RemoveTeamProduct()
        {
            // TODO
            // redirects to team product management page
            return View();
        }

        public async Task<IActionResult> UpdateTeamSettings()
        {
            // TODO: will take in settings view model
            // redirect to home management page
            return View();
        }

        public async Task<IActionResult> UploadNewBanner()
        {
            // TODO: will take in formdata with an image
            // shove image into AWS file system
            // return home management page
            return View();
        }
    }
}
