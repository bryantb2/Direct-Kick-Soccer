using System;
using System.Collections.Generic;
using System.Text;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using dropShippingApp.Controllers;
using Xunit;
using System.Threading.Tasks;

namespace ProductTests
{
    public class TeamRepoTests : IDisposable
    {
        // private fields
        private ITeamRepo teamRepo;
        private TeamController teamController;
        private TeamManagementController teamManagementController;

        // setup
        public TeamRepoTests()
        {
            teamRepo = new FakeTeamRepo();
            teamController = new TeamController(teamRepo);
            teamManagementController = new TeamManagementController(teamRepo);
        }

        // cleanup and dispose
        public void Dispose() 
        {
            teamRepo = null;
            teamController = null;
        }

        [Fact]
        public async Task TestAddTeam()
        {
            // arrange 
            var testTeam = new Team()
            {
                TeamID = 32,
                TeamName = "test"
            };
            await teamRepo.AddTeam(testTeam);

            // act
            List<Team> returnedTeams = (List<Team>)teamController.BrowseTeams().Result.ViewData.Model;

            // asert
            Assert.Equal(testTeam, returnedTeams.Find(team => team == testTeam));
        }

        [Fact]
        public async Task TestRemoveTeam()
        {
            // arrange 
            var testTeam = new Team()
            {
                TeamID = 32,
                TeamName = "test"
            };
            await teamRepo.AddTeam(testTeam);

            // act
            await teamController.RemoveTeam(testTeam.TeamID);

            // assert
            Assert.DoesNotContain(testTeam, teamRepo.GetTeams);
        }

        [Fact]
        public async Task TestUpdateTeam()
        {
            // arrange 
            var testTeam = new Team()
            {
                TeamID = 32,
                TeamName = "test"
            };
            var updatedTeam = new Team()
            {
                TeamID = 32,
                TeamName = "notTest"
            };
            await teamRepo.AddTeam(testTeam);

            // act
            await teamManagementController.UpdateTeamSettings(updatedTeam);

            // assert
            Assert.DoesNotContain(testTeam, teamRepo.GetTeams);
            Assert.Contains(updatedTeam, teamRepo.GetTeams);
        }

        [Fact]
        public async Task TestFindTeamById()
        {
            // arrange 
            var testTeam = new Team()
            {
                TeamID = 32,
                TeamName = "test"
            };
            await teamRepo.AddTeam(testTeam);

            // act
            var team = (Team)teamController.ViewTeam(32).Result.ViewData.Model;

            // assert
            Assert.Equal(testTeam, team);
        }

        [Fact]
        public async Task TestTeamSearch()
        {
            // arrange 
            var testTeam = new Team()
            {
                TeamID = 32,
                TeamName = "test"
            };
            await teamRepo.AddTeam(testTeam);
            const string searchTerm = "test";

            // act
            var searchResults = (List<Team>)teamController.SearchTeams(searchTerm).Result.ViewData.Model;

            // assert
            Assert.Contains(testTeam, searchResults);
        }

        [Fact]
        public async Task TestFindTeamByProduct()
        {
            // arrange


            // act


            // assert

            // TODO

            Assert.Equal(true, false);
        }
    }
}
