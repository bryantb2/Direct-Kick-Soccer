using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Controllers
{
    public class TeamController : Controller
    {
        private ITeamRepo teamRepo;
        private ITeamSortRepo teamSortRepo;
        public TeamController(ITeamRepo teamRepo, ITeamSortRepo sortRepo)
        {
            this.teamRepo = teamRepo;
            this.teamSortRepo = sortRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View("Search", null);
        }

        public async Task<IActionResult> ViewTeam(int teamId)
        {
            var team = await teamRepo.FindTeamById(teamId);

            // TODO

            return View(team);
        }

        public async Task<IActionResult> BackToFirstPage(int categoryId = -1, string searchTerm = null)
        {
            if (searchTerm != null)
                return RedirectToAction("Search", new
                {
                    searchString = searchTerm,
                    currentPage = 0
                });
            else
                return RedirectToAction("DisplayByCategory", new
                {
                    categoryId = categoryId,
                    currentPage = 0
                });
        }

        public async Task<IActionResult> NextPage(int currentPage, int categoryId = -1, string searchTerm = null)
        {
            if (searchTerm != null)
                return RedirectToAction("Search", new
                {
                    searchString = searchTerm,
                    currentPage = currentPage + 1
                });
            else
                return RedirectToAction("DisplayByCategory", new
                {
                    categoryId = categoryId,
                    currentPage = currentPage + 1
                });
        }

        public async Task<IActionResult> PreviousPage(int currentPage, int categoryId = -1, string searchTerm = null)
        {
            if (searchTerm != null)
                return RedirectToAction("Search", new
                {
                    searchString = searchTerm,
                    currentPage = currentPage - 1
                });
            else
                return RedirectToAction("DisplayByCategory", new
                {
                    categoryId = categoryId,
                    currentPage = currentPage - 1
                });
        }

        public async Task<IActionResult> Search(string searchString, int currentPage = -1)
        {
            // search for teams
            var foundTeams = SearchByString(searchString);

            // create browse view model
            var browseVM = CreateBrowseObject(
                foundProducts,
                currentPage == -1 ? 0 : currentPage,
                searchTerm: searchString);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> DisplayByCategory(int categoryId, int currentPage = -1)
        {
            // get products by category
            var categoryProducts = FilterProductsByCategory(categoryId);

            // get current category
            var category = await categoryRepo.GetCategoryById(categoryId);

            // create browse view model
            var browseVM = CreateBrowseObject(
                categoryProducts,
                currentPage == -1 ? 0 : currentPage,
                categoryObj: category);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> SortProducts(int sortId, int categoryId = -1, string searchTerm = null, int currentPage = -1)
        {
            // IMPORTANT: at no point will the user be allowed to search AND browse by category AT THE SAME TIME

            // get products by appropriate query
            var filteredProducts = categoryId != -1 ? FilterProductsByCategory(categoryId) : SearchByString(searchTerm);

            // get sort and check sort type
            var foundSort = sortRepo.GetSortById(sortId);
            if (foundSort.SortName.ToUpper() == "LOWEST PRICE")
            {
                filteredProducts.Sort((product1, product2) => product1.CurrentPrice.CompareTo(product2.CurrentPrice));
            }
            else if (foundSort.SortName.ToUpper() == "HIGHEST PRICE")
            {
                filteredProducts.Sort((product1, product2) => product2.CurrentPrice.CompareTo(product1.CurrentPrice));
            }

            // create browse view model
            BrowseViewModel browseVM = null;
            if (categoryId != -1)
            {
                // means user is browsing by category
                var foundCategory = await categoryRepo.GetCategoryById(categoryId);
                browseVM = CreateBrowseObject(
                    filteredProducts,
                    currentPage == -1 ? 0 : currentPage,
                    categoryObj: foundCategory);
            }
            else
            {
                // user is browsing products THEY searched
                browseVM = CreateBrowseObject(
                    filteredProducts,
                    currentPage == -1 ? 0 : currentPage,
                    searchTerm: searchTerm);
            }

            // return list
            return View("Search", browseVM);
        }

        public async Task<ViewResult> BuildTeam(Team team)
        {
            // TODO
            // returns redirect to view team
            await teamRepo.AddTeam(team);
            return View(team);
        }

        public async Task<ViewResult> MarkTeamInactive(int teamId)
        {
            // TODO
            // returns redirect to browse teams
            await teamRepo.MarkInactiveById(teamId);
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

        public async Task<IActionResult> UpdateTeamSettings(Team updatedTeam)
        {
            // CHECK TO MAKE SURE SENDER HAS THE TEAM IN THEIR APPROVED HISTORY 
            // otherwise they could change the team id and f*** up another person's team
            // TODO: will take in settings view model
            // redirect to home management page
            await teamRepo.UpdateTeam(updatedTeam);
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
