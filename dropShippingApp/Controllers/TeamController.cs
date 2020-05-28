using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
        private ITagRepo tagRepo;
        private IPricingRepo pricingRepo;
        private IRosterGroupRepo rosterGroupRepo;

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
            ITeamCreationReqRepo teamRequestRepo,
            ITagRepo tagRepo,
            IPricingRepo pricingRepo,
            IRosterGroupRepo rosterGroupRepo)
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
            this.tagRepo = tagRepo;
            this.pricingRepo = pricingRepo;
            this.rosterGroupRepo = rosterGroupRepo;
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

        public async Task<IActionResult> ViewTeam(int teamId)
        {
            Team foundTeam = await teamRepo.FindTeamById(teamId);
            return View(foundTeam);
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

        public async Task<IActionResult> TeamSettings()
        {
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null && user.ManagedTeam != null)
            {
                // verify users role, after roles are set up
                // get the users team
                Team usersTeam = user.ManagedTeam;
                TeamSettingsViewModel teamSettings = new TeamSettingsViewModel();
                teamSettings.TeamID = usersTeam.TeamID;
                teamSettings.Name = usersTeam.Name;
                teamSettings.Country = usersTeam.Country;
                teamSettings.Providence = usersTeam.Providence;
                teamSettings.StreetAddress = usersTeam.StreetAddress;
                teamSettings.ZipCode = usersTeam.ZipCode;
                teamSettings.CorporatePageURL = usersTeam.CorporatePageURL;
                teamSettings.BusinessEmail = usersTeam.BusinessEmail;
                teamSettings.PhoneNumber = usersTeam.PhoneNumber;
                teamSettings.Description = usersTeam.Description;

                return View("TeamSettings", teamSettings);
            }

            // If user is null, redirect to a Team/Index
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamSettings(int id, TeamSettingsViewModel teamSettings)
        {
            if (ModelState.IsValid)
            {
                Team foundTeam = await teamRepo.FindTeamById(id);
                foundTeam.Name = teamSettings.Name;
                foundTeam.Country = teamSettings.Country;
                foundTeam.Providence = teamSettings.Providence;
                foundTeam.StreetAddress = teamSettings.StreetAddress;
                foundTeam.ZipCode = teamSettings.ZipCode;
                foundTeam.CorporatePageURL = teamSettings.CorporatePageURL;
                foundTeam.BusinessEmail = teamSettings.BusinessEmail;
                foundTeam.PhoneNumber = teamSettings.PhoneNumber;
                foundTeam.Description = teamSettings.Description;
                
                await teamRepo.UpdateTeam(foundTeam);
            }
            return View(teamSettings);
        }

        public async Task<IActionResult> TeamManagement()
        {
            // display the main management page for teams
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null && user.ManagedTeam != null)
            {
                // verify users role, after roles are set up
                // get the users team
                var teamMangementVM = new TeamManagementVM()
                {
                    LifeTimeSales = 0,
                    MonthlySales = 0,
                    WeeklySales = 0,
                    Team = user.ManagedTeam,
                    OfferedProductGroups = null
                };
                ViewBag.DefaultFooter = false;
                return View("TeamManagement", teamMangementVM);
            }
            return View("Index");
        }

        public async Task<IActionResult> AddGroup()
        {
            var tagList = tagRepo.GetTags;
            var rosterGroupList = rosterGroupRepo.ProductGroups;
            var createGroupVM = new CreateGroupVM()
            {
                DatabaseTags = tagList,
                RosterGroups = rosterGroupList
            };
            return View("AddGroup", createGroupVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(CreateGroupVM createdGroupModel)
        {
            if(ModelState.IsValid)
            {
                // valid user
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                if (user != null)
                {
                    // get base roster group
                    var rosterGroup = rosterGroupRepo.GetGroupById(createdGroupModel.SelectedRosterGroupID);
                    // create product group
                    var newGroup = new ProductGroup()
                    {
                        Title = createdGroupModel.Title,
                        Description = createdGroupModel.Description,
                        GeneralThumbnail = createdGroupModel.GeneralThumbnail,
                        PrintDesignPNG = createdGroupModel.PrintDesignPNG,
                        BaseGroupModelNumber = rosterGroup.ModelNumber
                    };


                    // check new tags
                    var tagList = tagRepo.GetTags;
                    if (createdGroupModel.NewTagName != null)
                    {
                        // add existing tag from DB
                        var existingTag = tagList.Find(tag => tag.TagLine.ToUpper() == createdGroupModel.NewTagName.ToUpper());
                        if (existingTag == null)
                        {
                            // create tag and add to DB if it does not exist
                            var newTag = new Tag()
                            {
                                TagLine = createdGroupModel.NewTagName
                            };
                            await tagRepo.AddTag(newTag);
                            existingTag = newTag;
                        }
                        newGroup.ProductTags.Add(existingTag);
                    }
                    else if (createdGroupModel.ExistingTagID != null)
                    {
                        // get existing tag from DB, add to group
                        var existingTag = tagList.Find(tag => tag.TagID == createdGroupModel.ExistingTagID);
                        newGroup.ProductTags.Add(existingTag);
                    }

                    // save new group to DB
                    await productGroupRepo.AddProductGroup(newGroup);

                    // update user team data
                    user.ManagedTeam.ProductGroups.Add(newGroup);
                    await userManager.UpdateAsync(user);

                    return RedirectToAction("TeamManagement");
                }
            }
            ModelState.AddModelError(nameof(CreateGroupVM.Title), "Please make sure to fill out all required fields");
            return View("AddGroup", createdGroupModel);
        }

        public async Task<IActionResult> RemoveGroup(int SelectedGroupID)
        {
            // get user and select the group attached to its team
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null)
            {
                // check if group exists, for secure removal
                var doesGroupExist = user.ManagedTeam.ProductGroups
                    .Exists(group => group.ProductGroupID == SelectedGroupID);
                if(doesGroupExist)
                    await productGroupRepo.RemoveProductGroup(SelectedGroupID);
            }
            return RedirectToAction("TeamManagement");
        }

        public async Task<IActionResult> UpdateGroup(int SelectedGroupID)
        {
            // get user and select the group attached to its team
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if(user != null)
            {
                // get the group and return view
                var foundGroup = user.ManagedTeam.ProductGroups
                    .Find(group => group.ProductGroupID == SelectedGroupID);

                if(foundGroup == null)
                    return RedirectToAction("TeamManagement");

                // create updated group VM from found group
                var updateGroupVM = new UpdateGroupVM()
                { 
                    GroupID = foundGroup.ProductGroupID,
                    Title = foundGroup.Title,
                    Description = foundGroup.Description,
                    ThumbnailURL = foundGroup.GeneralThumbnail,
                    DatabaseTags = tagRepo.GetTags,
                    ExistingGroupTags = foundGroup.ProductTags
                };

                return View("ModifyGroup", updateGroupVM);
            }
            return RedirectToAction("TeamManagement");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGroup(UpdateGroupVM updatedGroup)
        {
            if(ModelState.IsValid)
            {
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                if (user != null)
                {
                    // get the group
                    var foundGroup = user.ManagedTeam.ProductGroups
                        .Find(group => group.ProductGroupID == updatedGroup.GroupID);
                    // apply updated properties
                    foundGroup.Description = updatedGroup.Description;
                    foundGroup.Title = updatedGroup.Title;
                    foundGroup.GeneralThumbnail = updatedGroup.ThumbnailURL;

                    // check new tags
                    var tagList = tagRepo.GetTags;
                    if (updatedGroup.NewTagName != null)
                    {
                        // add existing tag from DB
                        var existingTag = tagList.Find(tag => tag.TagLine.ToUpper() == updatedGroup.NewTagName.ToUpper());
                        if(existingTag == null)
                        {
                            // create tag and add to DB if it does not exist
                            var newTag = new Tag()
                            {
                                TagLine = updatedGroup.NewTagName
                            };
                            await tagRepo.AddTag(newTag);
                            existingTag = newTag;
                        }
                        var groupTags = foundGroup.ProductTags;
                        groupTags.Add(existingTag);
                        foundGroup.ProductTags = groupTags;
                    }
                    else if(updatedGroup.ExistingTagID != null )
                    {
                        // get existing tag from DB, add to group
                        var existingTag = tagList.Find(tag => tag.TagID == updatedGroup.ExistingTagID);
                        var groupTags = foundGroup.ProductTags;
                        groupTags.Add(existingTag);
                        foundGroup.ProductTags = groupTags;
                    }
                    // check remove tag
                    if(updatedGroup.RemovedTagID != null)
                    {
                        // find and remove tag in list
                        var groupTags = foundGroup.ProductTags;
                        var removedTagIndex = groupTags.FindIndex(tag => tag.TagID == updatedGroup.RemovedTagID);
                        groupTags.RemoveAt(removedTagIndex);
                        foundGroup.ProductTags = groupTags;
                    }

                    // update group in DB
                    await productGroupRepo.UpdateProductGroup(foundGroup);
                    return RedirectToAction("TeamManagement");
                }
            }
            ModelState.AddModelError(nameof(UpdateGroupVM.Title), "Please make sure to fill out all required fields");
            return View("ModifyGroup", updatedGroup);
        }

        public async Task<IActionResult> UpdateTeamProduct(CustomProductSelectionCardVM selectedProductModel)
        {
            // redirects to team product management page
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null)
            {
                // get group and product data
                var selectedGroup = user.ManagedTeam.ProductGroups
                    .Find(group => group.ProductGroupID == selectedProductModel.SelectedGroupID);
                var selectedProduct = selectedGroup.ChildProducts
                    .Find(product => product.CustomProductID == selectedProductModel.SelectedProductID);
                if (selectedGroup != null && selectedProduct != null)
                {
                    var modifyProductVM = new UpdateProductVM()
                    {
                        ProductId = selectedProductModel.SelectedProductID,
                        GroupId = selectedProductModel.SelectedGroupID,
                        ProductName = selectedGroup.Title,
                        ProductImageURL = selectedProduct.ProductPNG,
                        CurrentPrice = selectedProduct.CurrentPrice
                    };
                    return View("ModifyProduct", modifyProductVM);
                }
            }
            return RedirectToAction("TeamManagement");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeamProduct(UpdateProductVM updatedProduct)
        {
            if (ModelState.IsValid)
            {
                // redirects to team product management page
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                if (user != null)
                {
                    // get product
                    var foundProduct = user.ManagedTeam.ProductGroups
                        .Find(group => group.ProductGroupID == updatedProduct.GroupId).ChildProducts
                        .Find(product => product.CustomProductID == updatedProduct.ProductId);
                    // set product properties
                    foundProduct.ProductPNG = updatedProduct.ProductImageURL;
                    if(updatedProduct.CurrentPrice != null)
                    {
                        var newPricingHistory = new PricingHistory()
                        {
                            DateChanged = DateTime.Now,
                            NewPrice = (decimal)updatedProduct.CurrentPrice
                        };
                        // save pricing history to DB
                        await pricingRepo.AddHistory(newPricingHistory);
                        foundProduct.AddPricingHistory(newPricingHistory);
                    }

                    // save product changes in DB
                    await customProductRepo.UpdateCustomProduct(foundProduct);
                    return RedirectToAction("TeamManagement");
                }
            }
            ModelState.AddModelError(nameof(UpdateProductVM.ProductName), "Please make sure to fill out all fields");
            return View("ModifyGroup", updatedProduct);
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

        public async Task<IActionResult> RemoveTeamProduct(UpdateProductVM updatedProduct)
        {
            if (ModelState.IsValid)
            {
                // redirects to team product management page
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                if(user != null)
                {
                    // get product to be removed
                    var foundProduct = user.ManagedTeam.ProductGroups
                        .Find(group => group.ProductGroupID == updatedProduct.GroupId).ChildProducts
                        .Find(product => product.CustomProductID == updatedProduct.ProductId);

                    // remove from group
                    var foundGroup = user.ManagedTeam.ProductGroups
                        .Find(group => group.ProductGroupID == updatedProduct.GroupId);
                    foundGroup.ChildProducts.Remove(foundProduct);
                    await productGroupRepo.UpdateProductGroup(foundGroup);

                    // remove product from DB
                    await customProductRepo.RemoveCustomProduct(foundProduct.CustomProductID);
                    return RedirectToAction("TeamManagement");
                }
            }
            ModelState.AddModelError(nameof(UpdateProductVM.ProductId), "Please make sure to fill out all fields");
            return View("ModifyGroup", updatedProduct);
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
        public async Task<IActionResult> TeamReq(TeamCreationRequest request, string country, string province)
        {
            MyWordFilter filter = new MyWordFilter();
            if (ModelState.IsValid)
            {
                if (filter.BadWords(request.TeamDescription) == false && filter.BadWords(request.TeamName)==false
                   && filter.BadWords(request.BusinessEmail) ==false && filter.BadWords(request.CorporatePageURL) ==false
                    && filter.BadWords(request.StreetAddress) ==false) 
                {
                    List<Country> countries = locationRepo.Countries;
                    Country myCountry = countries.First(c => c.CountryName.ToLower() == country.ToLower());

                    List<Province> provinces = locationRepo.Provinces;
                    Province myProv = provinces.First(p => p.ProvienceAbbreviation.ToLower() == province.ToLower());
                    TeamCreationRequest req = new TeamCreationRequest
                    {
                        TeamName = request.TeamName,
                        TeamDescription = request.TeamDescription,
                        BusinessEmail = request.BusinessEmail,
                        PhoneNumber = request.PhoneNumber,
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



        // HEADLESS API METHODS
        public async Task<IActionResult> GetProductsByGroupId(int id)
        {
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if(user != null)
            {
                // get product from DB
                // return product list
                var productGroup = user.ManagedTeam.ProductGroups.Find(group => group.ProductGroupID == id);
                return Ok(productGroup.ChildProducts);
            }
            return BadRequest();
        }
    }
}
