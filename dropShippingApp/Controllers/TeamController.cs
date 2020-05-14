using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using dropShippingApp.HelperUtilities;
using Microsoft.AspNetCore.Identity;


namespace dropShippingApp.Controllers
{
    public class TeamController : Controller
    {
        private UserManager<AppUser> userManager;
        private ICustomProductRepo customProductRepo;
        private IProductGroupRepo productGroupRepo;
        private ITeamRepo teamRepo;
        private ITeamSortRepo teamSortRepo;
        private ITeamCategoryRepo categoryRepo;
        private IOrderRepo orderRepo;
        private IUserRepo userRepo;
        private ILocationRepo locationRepo;
        private ITeamCreationReqRepo teamRequestRepo;

        public TeamController(
            ITeamRepo teamRepo, 
            ITeamSortRepo sortRepo,
            ITeamCategoryRepo categoryRepo,
            IOrderRepo orderRepo,
            IUserRepo userRepo,
            ILocationRepo locationRepo,
            ICustomProductRepo customProductRepo,
            IProductGroupRepo productGroupRepo,
            UserManager<AppUser> userManager,
            ITeamCreationReqRepo teamRequestRepo)
        {
            this.teamRepo = teamRepo;
            this.teamSortRepo = sortRepo;
            this.categoryRepo = categoryRepo;
            this.orderRepo = orderRepo;
            this.userRepo = userRepo;
            this.locationRepo = locationRepo;
            this.customProductRepo = customProductRepo;
            this.productGroupRepo = productGroupRepo;
            this.userManager = userManager;
            this.teamRequestRepo = teamRequestRepo;
        }

        public async Task<IActionResult> Index()
        {
            var teamList = teamRepo.GetTeams;
            return View(teamList);
        }

        public async Task<IActionResult> Browse()
        {
            return View("Search", null);
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
            var foundTeams = SearchHelper.SearchByString<Team>(teamRepo.GetTeams, searchString);

            // create browse view model
            var browseVM = SearchHelper.CreateBrowseObject<Team>(
                currentPage == -1 ? 0 : currentPage,
                searchTerm: searchString,
                queriedTeams: foundTeams);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> DisplayByCategory(int categoryId, int currentPage = -1)
        {
            // get products by category
            var categoryTeams = SearchHelper.FilterByCategory<Team>(teamRepo.GetTeams, categoryId);

            // get current category
            var category = categoryRepo.GetCategoryById(categoryId);

            // create browse view model
            var browseVM = SearchHelper.CreateBrowseObject<Team>(
                currentPage == -1 ? 0 : currentPage,
                teamCategory: category,
                queriedTeams: categoryTeams);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> SortTeams(int sortId, int categoryId = -1, string searchTerm = null, int currentPage = -1)
        {
            // IMPORTANT: at no point will the user be allowed to search AND browse by category AT THE SAME TIME

            // get products by appropriate query
            var filteredTeams = categoryId != -1 ? 
                SearchHelper.FilterByCategory<Team>(teamRepo.GetTeams, categoryId) 
                : SearchHelper.SearchByString<Team>(teamRepo.GetTeams, searchTerm);

            // get sort and check sort type
            var foundSort = teamSortRepo.GetSortById(sortId);
            if (foundSort.SortName.ToUpper() == "OLDEST")
            {
                filteredTeams.Sort((team1, team2) => team1.DateJoined.CompareTo(team2.DateJoined));
            }
            else if (foundSort.SortName.ToUpper() == "NEWEST")
            {
                filteredTeams.Sort((team1, team2) => team2.DateJoined.CompareTo(team1.DateJoined));
            }
            else if (foundSort.SortName.ToUpper() == "MOST POPULAR")
            {
                SearchHelper.SortByMostPopular<Team>(ref filteredTeams, orderRepo.GetOrders);
            }

            // create browse view model
            BrowseViewModel browseVM = null;
            if (categoryId != -1)
            {
                // means user is browsing by category
                var foundCategory = categoryRepo.GetCategoryById(categoryId);
                browseVM = SearchHelper.CreateBrowseObject<Team>(
                    currentPage == -1 ? 0 : currentPage,
                    teamCategory: foundCategory,
                    queriedTeams: filteredTeams);
            }
            else
            {
                // user is browsing products THEY searched
                browseVM = SearchHelper.CreateBrowseObject<Team>(
                    currentPage == -1 ? 0 : currentPage,
                    searchTerm: searchTerm,
                    queriedTeams: filteredTeams);
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

        public async Task<IActionResult> TeamSettings()
        {
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null && user.ManagedTeam != null)
            {
                // verify users role, after roles are set up
                // get the users team
                return View("TeamSettings", user.ManagedTeam);
            }
            
            // If user is null, redirect to a Team/Index
            return View("Index");
        }

        public async Task<IActionResult> TeamManager()
        {
            // display the main management page for teams
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null && user.ManagedTeam != null)
            {
                // verify users role, after roles are set up
                // get the users team
                return View("TeamManager", user.ManagedTeam);
            }
            return View("Index");
        }

        public async Task<IActionResult> ManageTeamProducts()
        {
            // TODO
            // returns team product management page
           
            return View();
        }

        public async Task<IActionResult> AddTeamProduct(int groupId, CustomProduct customProduct)
        {
            if(ModelState.IsValid)
            {
                // redirects to team product management page
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // get the group
                var group = user.ManagedTeam.ProductGroups.Find(group => group.ProductGroupID == groupId);
                // add product to DB
                // add product to group
                // update group in DB
                // update user in DB
                await customProductRepo.AddCustomProduct(customProduct);
                group.ChildProducts.Add(customProduct);
                await productGroupRepo.UpdateProductGroup(group);
                await userManager.UpdateAsync(user);

                // return view
                throw new NotImplementedException();
            }
            // add return statement here too
            throw new NotImplementedException();
        }

        public async Task<IActionResult> UpdateTeamProduct(int groupId, CustomProduct customProduct)
        {
            if (ModelState.IsValid)
            {
                // redirects to team product management page
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // update in database
                await customProductRepo.UpdateCustomProduct(customProduct);

                // return view
                throw new NotImplementedException();
            }
            // add return statement here too
            throw new NotImplementedException();
        }

        public async Task<IActionResult> RemoveTeamProduct(int groupId, int productId)
        {
            if (ModelState.IsValid)
            {
                // redirects to team product management page
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // find product
                var product = user.ManagedTeam.ProductGroups.Find(group => group.ProductGroupID == groupId).ChildProducts
                    .Find(product => product.CustomProductID == productId);

                if (product != null)
                    await customProductRepo.RemoveCustomProduct(productId);

                // return view
                throw new NotImplementedException();
            }
            // add return statement here too
            throw new NotImplementedException();
        }

        public async Task<IActionResult> MarkProductInActive(int groupId, int productId)
        {
            if (ModelState.IsValid)
            {
                // redirects to team product management page
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // find product
                var product = user.ManagedTeam.ProductGroups.Find(group => group.ProductGroupID == groupId).ChildProducts
                    .Find(product => product.CustomProductID == productId);

                if (product != null)
                {
                    // mark inactive
                    // change in DB
                    product.IsProductActive = false;
                    await customProductRepo.UpdateCustomProduct(product);
                }

                // return view
                throw new NotImplementedException();
            }
            // add return statement here too
            throw new NotImplementedException();
        }

        public async Task<IActionResult> UploadNewBanner()
        {
            // TODO: will take in formdata with an image
            // shove image into AWS file system
            // return home management page
            return View();
        }


        public async Task<IActionResult> TeamReq()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeamReq(TeamCreationRequest request)
        {
            MyWordFilter filter = new MyWordFilter();// documentation https://github.com/smurfpandey/WordFilter
            if (ModelState.IsValid)
            {
                if (filter.BadWords(request.TeamDescription) == false && filter.BadWords(request.TeamName)==false
                   && filter.BadWords(request.BusinessEmail) ==false && filter.BadWords(request.CorporatePageURL) ==false
                    && filter.BadWords(request.StreetAddress) ==false) 
                {
                    List<Country> countries = locationRepo.GetAllCountries;
                    Country myCountry = countries.First(c => c.CountryName.ToLower() == request.Country.CountryName.ToLower());

                    List<Province> provinces = locationRepo.GetAllProvinces;
                    Province myProv = provinces.First(p => p.ProvienceAbbreviation.ToLower() == request.Providence.ProvinceName.ToLower());
                    TeamCreationRequest req = new TeamCreationRequest
                    {
                        TeamName = request.TeamName,
                        TeamDescription = request.TeamDescription,
                        BusinessEmail = request.BusinessEmail,
                        CorporatePageURL = request.CorporatePageURL,
                        StreetAddress = request.StreetAddress,
                        Country = myCountry,
                        Providence = myProv,
                        ZipCode = request.ZipCode
                    };
                    await teamRequestRepo.AddReq(req);

                    return View("ReqConfirm");
                }
            }
            return View("TeamReq");
        }

    }
}
