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
        public async Task<IActionResult> ViewTeam(int teamId)
        {
            // TODO
            var team = await teamRepo.FindTeamById(teamId);
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
            // search for products
            var foundProducts = SearchByString(searchString);

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

        // private actions
        private BrowseViewModel CreateBrowseObject(List<CustomProduct> queriedProducts, int currentPageNumber, Category categoryObj = null, string searchTerm = null)
        {
            // setup starting and ending product indexes
            var itemsPerPage = 30;
            var startProduct = currentPageNumber * itemsPerPage;
            var endProduct = startProduct + 30;

            // setup paging view model
            var pagingInfo = new BrowseViewModel()
            {
                Products = SplitList(queriedProducts, startProduct, endProduct),
                CurrentPage = currentPageNumber,
                SearchString = searchTerm == null ? null : searchTerm,
                CurrentCategory = categoryObj == null ? null : categoryObj,
                // next page exists if the number of products left in the query is greater than the total number of dispalyed products
                NextPageExists = queriedProducts.Count > endProduct ? true : false,
                PreviousPageExists = startProduct - itemsPerPage > 0 ? true : false
            };

            return pagingInfo;
        }

        private List<Team> SplitList(List<Team> filterableList, int start, int end)
        {
            // remember: index is one behind the actual product number in the list
            if (filterableList.Count == 0)
                return filterableList;

            // check if end parameter is higher than remain filterable list count (prevent out of range error
            var checkedEnd = (filterableList.Count - end) < 0 ? filterableList.Count : end;
            var splitList = new List<Team>();
            for (var i = start; i <= checkedEnd - 1; i++)
            {
                splitList.Add(filterableList[i]);
            }
            return splitList;
        }

        private List<CustomProduct> FilterProductsByCategory(int categoryId)
        {
            var filteredProducts = customProductRepo.CustomProducts.Where(product => product.BaseProduct.Category.ProductCategoryID == categoryId);
            return filteredProducts.ToList();
        }

        private List<CustomProduct> SearchByString(string searchString)
        {
            if (searchString.Length >= 2)
            {
                // clean search term
                var cleanedSearchTerm = searchString.Trim().Split(' ');
                // checks product tags, title, color, size, SKU, model number
                var customProducts = customProductRepo.CustomProducts;
                var foundProducts = new List<CustomProduct>();
                foreach (var product in customProducts)
                {
                    if (DoesQueryContainString(cleanedSearchTerm, product.ProductTitle))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.ProductTags))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.ProductTags))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.ModelNumber.ToString()))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.SKU.ToString()))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.BaseColor.ColorName))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.BaseSize.SizeName))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.ProductTags))
                        foundProducts.Add(product);
                    else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.Category.Name))
                        foundProducts.Add(product);
                }
                return foundProducts;
            }
            return new List<CustomProduct>();
        }

        private bool DoesQueryContainString(string[] query, string stringToCheck)
        {
            var stringAsTolken = stringToCheck.Split(' ');
            foreach (var searchTerm in query)
            {
                foreach (var checkAgainstTerm in stringAsTolken)
                {
                    if (searchTerm.ToUpper() == checkAgainstTerm.ToUpper() || checkAgainstTerm.ToUpper().Contains(searchTerm.ToUpper()))
                        return true;
                }
            }
            return false;
        }

        private bool DoesQueryContainString(string[] query, List<Tag> tagsToCheck)
        {
            if (tagsToCheck == null)
                return false;
            else
            {
                foreach (var term in query)
                {
                    foreach (var tag in tagsToCheck)
                    {
                        if (term.ToUpper() == tag.TagLine.ToUpper())
                            return true;
                    }
                }
                return false;
            }
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

        // only admins will have access to this
        public async Task<ViewResult> RemoveTeam(int teamId)
        {
            // TODO
            // returns redirect to browse teams
            await teamRepo.RemoveTeam(teamId);
            return View();
        }

        public async Task<ViewResult> BrowseTeams()
        {
            // TODO
            // returns team results page 
            var teamList = teamRepo.GetTeams;
            return View(teamList);
        }

        public async Task<ViewResult> SearchTeams(string searchTerm)
        {
            // TODO
            // returns team results page (will have view model with search term)
            var teamList = teamRepo.GetTeams;
            List<Team> searchResults = SearchListForMatches(teamList, searchTerm);
            return View(searchResults);
        }

        // private stat and searching methods
        private List<Team> SearchListForMatches(List<Team> teams, string searchTerm)
        {
            List<Team> searchResults = new List<Team>();
            foreach (Team t in teams)
            {
                // search on team name
                if (t.Name.ToUpper().Contains(searchTerm.ToUpper()))
                    searchResults.Add(t);
                foreach (Tag tag in t.TeamTags)
                {
                    // search on product tags
                    if (tag.TagLine.ToUpper().Contains(searchTerm.ToUpper()))
                        searchResults.Add(t);
                }
            }
            return searchResults;
        }

        // TODO 
        // DO NOT WRITE CODE FOR THESE UNTIL BOTH THE PRODUCT AND TEAM REPOS ARE FINISHED...

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
