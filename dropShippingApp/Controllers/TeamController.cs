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


namespace dropShippingApp.Controllers
{
    public class TeamController : Controller
    {
        private ITeamRepo teamRepo;
        private ITeamSortRepo teamSortRepo;
        private ITeamCategoryRepo categoryRepo;
        private IOrderRepo orderRepo;
        public TeamController(
            ITeamRepo teamRepo, 
            ITeamSortRepo sortRepo,
            ITeamCategoryRepo categoryRepo,
            IOrderRepo orderRepo)
        {
            this.teamRepo = teamRepo;
            this.teamSortRepo = sortRepo;
            this.categoryRepo = categoryRepo;
            this.orderRepo = orderRepo;
        }

        public async Task<IActionResult> Index()
        ITeamRepo teamRepo;
        ILocationRepo locRepo;
        ITeamCreationReqRepo reqRepo;
        IUserRepo userRepo;
        public TeamController(ITeamRepo t, ILocationRepo l, ITeamCreationReqRepo r, IUserRepo u)
        {
            reqRepo = r;
            locRepo = l;
            teamRepo = t;
            userRepo = u;
        }

        public async Task<IActionResult> Index()
        {
            var teamList = teamRepo.GetTeams;
            return View(teamList);
        }

        /*
       public async Task<IActionResult> Index()
       {
            Team mTeam = await teamRepo.FindTeamById(1);
            CustomProduct cProduct = mTeam.TeamProducts.Find(item => item.CustomProductID == 3);
            cProduct.ProductTitle = "Fire Roasted Socks";
            await UpdateTeamProduct(mTeam, cProduct);
            //cProduct = mTeam.TeamProducts.Find(item => item.CustomProductID == 3);
            mTeam = await teamRepo.FindTeamById(1);
            return View(mTeam);
        }

    */
        public async Task<ViewResult> BuildTeam(Team team)
        {
            return View("Search", null);
        }

        public async Task<IActionResult> ViewTeam(int teamId)
        {
            // get 
            /*var team = await teamRepo.FindTeamById(teamId);
            // TODO
            return View(team);*/
            throw new NotImplementedException();
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
            AppUser user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null)
            {
                // verify users role, after roles are set up
                // get the users team
                Team team = user.ManagedTeam;
                return View("TeamSettings", team);
            }
            
            // If user is null, redirect to a Team/Index
            return View("Index");
        }

        public async Task<IActionResult> TeamManager()
        {
            AppUser user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null)
            {
                // verify users role, after roles are set up
                // get the users team
                if (user.ManagedTeam.TeamID != null)
                {
                    var teamID = user.ManagedTeam.TeamID;

                    Team team = await teamRepo.FindTeamById(teamID);
                    foreach (var product in team.TeamProducts)
                    {
                        if (product.ProductTitle == null)
                        {
                            product.ProductTitle = "This product Title is invalid";
                        }
                        else if (product.PricingHistory.Count == 0)
                        {
                            PricingHistory pricingHistoryUpdate = new PricingHistory
                            {
                                DateChanged = new DateTime(2020, 4, 1),
                                NewPrice = 666
                            };
                            product.AddPricingHistory(pricingHistoryUpdate);
                        }
                        else if (product.BaseProduct.SKU == null)
                        {
                            product.BaseProduct.SKU = 666;
                        }
                    }
                    return View("TeamManager", team);
                }
            }
            return View("Index");
        }

        // TODO 
        // DO NOT WRITE CODE FOR THESE UNTIL BOTH THE PRODUCT AND TEAM REPOS ARE FINISHED...

        public async Task<IActionResult> ManageTeamProducts()
        {
            // TODO
            // returns team product management page
           
            return View();
        }

        public async Task<IActionResult> AddTeamProduct(Team team, CustomProduct customProduct)
        {
            // TODO
            // redirects to team product management page
            Team mTeam = await teamRepo.FindTeamById(team.TeamID);
            mTeam.AddProduct(customProduct);
            await teamRepo.UpdateTeam(mTeam);
            return RedirectToAction("/TeamManagement/Index");
        }

        public async Task<IActionResult> UpdateTeamProduct(Team team, CustomProduct updatedCustomProduct)
        {
            // TODO
            // redirects to team product management page
            Team mTeam = await teamRepo.FindTeamById(team.TeamID);
            CustomProduct cProduct = mTeam.TeamProducts.Find(item => item.CustomProductID == updatedCustomProduct.CustomProductID);
            mTeam.RemoveProduct(cProduct);
            mTeam.AddProduct(updatedCustomProduct);
            await teamRepo.UpdateTeam(mTeam);
            return RedirectToAction("/TeamManagement/Index");
        }

        public async Task<IActionResult> RemoveTeamProduct(Team team, CustomProduct customProduct)
        {
            // TODO
            // redirects to team product management page
            Team mTeam = await teamRepo.FindTeamById(team.TeamID);
            mTeam.RemoveProduct(customProduct);
            await teamRepo.UpdateTeam(mTeam);
            return RedirectToAction("/TeamManagement/Index");
        }

        public async Task<IActionResult> MarkProductInActive(Team team, CustomProduct customProduct)
        {
            Team mTeam = await teamRepo.FindTeamById(team.TeamID);
            CustomProduct cProduct = mTeam.TeamProducts.Find(item => item.CustomProductID == customProduct.CustomProductID);
            mTeam.RemoveProduct(cProduct);
            cProduct.IsProductActive = false;
            mTeam.AddProduct(cProduct);
            await teamRepo.UpdateTeam(mTeam);
            return RedirectToAction("/TeamManagement/Index");
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

        [HttpGet]
        public IActionResult TeamReq()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeamReq(string name, string description, string email, string corporatePageURL,string streetAddress,
                                      string country, string providence, string zipCode)
        {
            MyWordFilter filter = new MyWordFilter();// documentation https://github.com/smurfpandey/WordFilter
            if (ModelState.GetValidationState(nameof(name))==ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(description)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(email)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(corporatePageURL)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(streetAddress)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(country)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(providence)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(zipCode)) == ModelValidationState.Valid)
            {
                if (filter.BadWords(name) == false && filter.BadWords(description)==false
                   && filter.BadWords(email)==false && filter.BadWords(corporatePageURL)==false
                    && filter.BadWords(streetAddress)==false) 
                {
                    List<Country> countries = locRepo.GetAllCountries;
                    Country myCountry = countries.First(c => c.CountryName.ToLower() == country.ToLower());

                    List<Province> provinces = locRepo.GetAllProvinces;
                    Province myProv = provinces.First(p => p.ProvienceAbbreviation.ToLower() == providence.ToLower());
                    TeamCreationRequest req = new TeamCreationRequest
                    {
                        TeamName = name,
                        TeamDescription = description,
                        BusinessEmail = email,
                        CorporatePageURL = corporatePageURL,
                        StreetAddress = streetAddress,
                        Country = myCountry,
                        Providence = myProv,
                        ZipCode = zipCode

                    };
                    await reqRepo.AddReq(req);

                    return View("ReqConfirm");
                    
                }
            }
                
                

            return View("TeamReq");
        }

    }
}
