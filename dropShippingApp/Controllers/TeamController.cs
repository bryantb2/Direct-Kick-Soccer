﻿using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    }
}
