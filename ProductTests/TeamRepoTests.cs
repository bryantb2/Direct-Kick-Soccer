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

        // setup
        public TeamRepoTests()
        {
            teamRepo = new FakeTeamRepo();
            teamController = new TeamController(teamRepo);
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

        }

        [Fact]
        public async Task TestUpdateTeam()
        {

        }

        [Fact]
        public async Task TestFindTeamById()
        {

        }

        [Fact]
        public async Task TestFindTeamByProduct()
        {

        }
    }
}
