using dropShippingApp.Data.Repositories;
using dropShippingApp.Data.Repositories.FakeRepos;
using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class TeamCreationReqTests:IDisposable
    {
        private ITeamCreationReqRepo reqRepo;

        public TeamCreationReqTests()
        {
            reqRepo = new FakeCreationReqRepo();
        }
        public void Dispose()
        {
            reqRepo = null;
        }
        [Fact]
        public async Task TestAddReq()
        {
            //arrange
            TeamCreationRequest req = new TeamCreationRequest
            {
                TeamName="TestReq",

            };
            //act
            reqRepo.AddReq(req);
            List<TeamCreationRequest> requests = await reqRepo.GetAll();
            //assert
            Assert.Contains(req, requests);
        }
        [Fact]
        public async Task TestGetById()
        {
            //arrange
            TeamCreationRequest req = new TeamCreationRequest
            {
                TeamName = "TestReq",
                TeamCreationRequestID=1
            };
            await reqRepo.AddReq(req);
            //act
            TeamCreationRequest result = await reqRepo.GetById(1);
            //assert
            Assert.Equal(req, result);
        }
        [Fact]
        public async Task TestMarkAsRejected()
        {
            //arrange
            TeamCreationRequest req = new TeamCreationRequest
            {
                TeamName = "TestReq",
                TeamCreationRequestID = 1
            };
            await reqRepo.AddReq(req);
            //act
            await reqRepo.MarkAsRejected(req.TeamCreationRequestID);
            //assert
            Assert.False(req.IsApproved); 
            Assert.True(req.IsApproved);
        }
        [Fact]
        public async Task TestUpdateTeamReq()
        {
            //arrange
            TeamCreationRequest req = new TeamCreationRequest
            {
                TeamName = "TestReq",
                TeamCreationRequestID = 1
            };
            await reqRepo.AddReq(req);
            TeamCreationRequest reqUpdated = new TeamCreationRequest
            {
                TeamName = "UpdatedReq",
                TeamCreationRequestID = 1
            };
            //act
            await reqRepo.UpdateReq(reqUpdated);
            var result = reqRepo.GetAll();
           
            //assert
            //Assert.DoesNotContain(req, req.get);
        }
    }
}
