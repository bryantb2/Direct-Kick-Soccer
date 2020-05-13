using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dropShippingApp.Controllers
{
    public class TeamController : Controller
    {
        ITeamRepo teamRepo;
        ILocationRepo locRepo;
        ITeamCreationReqRepo requestRepo;
        IUserRepo userRepo;
        public TeamController(ITeamRepo t, ILocationRepo l,ITeamCreationReqRepo r,IUserRepo u)
        {
            userRepo = u;
            requestRepo = r;
            locRepo = l;
            teamRepo = t;
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

        public async Task<ViewResult> ViewTeam(int teamId)
        {
            // TODO
            // returns specific team page
            var team = await teamRepo.FindTeamById(teamId);
            return View(team);
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

        [HttpGet]
        public IActionResult TeamReq()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeamReq(string name, string description, string email, string corporatePageURL,string streetAddress,
                                      string country, string providence, string zipCode)
        {
            MyWordFilter filter = new MyWordFilter();
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
                    TeamCreationRequest request = new TeamCreationRequest
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
                    await requestRepo.AddReq(request);
                    AppUser user = await userRepo.GetUserDataAsync(HttpContext.User);
                    user.AddCreationRequest(request);
                    return View("ReqConfirm");
                    
                }
            }
                
                

            return View("TeamReq");
        }

    }
}
