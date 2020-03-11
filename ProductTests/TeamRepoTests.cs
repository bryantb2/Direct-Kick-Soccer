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
        public TeamRepoTests(ITeamRepo t)
        {
            teamRepo = t;
            teamController = new TeamController(t);
        }

        // cleanup and dispose
        public void Dispose()
        {

        }

        [Fact]
        public async Task TestAddTeam()
        {

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
