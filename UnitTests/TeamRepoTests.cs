using System;
using System.Collections.Generic;
using System.Text;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Data.Repositories.FakeRepos;
using dropShippingApp.Models;
using dropShippingApp.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Xunit;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Castle.Windsor.Installer;
using Microsoft.AspNetCore.Connections;

namespace ProductTests
{

    //public class TeamRepoTests : IDisposable
    //{
    //    //private fields;
    //    private UserManager<AppUser> userManager;
    //    private ICustomProductRepo customProductRepo;
    //    private IProductGroupRepo productGroupRepo;
    //    private ITeamRepo teamRepo;
    //    private ITeamSortRepo teamSortRepo;
    //    private ITeamCategoryRepo categoryRepo;
    //    private IOrderRepo orderRepo;
    //    private IUserRepo userRepo;
    //    private ILocationRepo locationRepo;
    //    private ITeamCreationReqRepo teamRequestRepo;
    //    private TeamController teamController;

    //    // setup
    //    public TeamRepoTests()
    //    {
            
    //        teamController = new TeamController(teamRepo, teamSortRepo, categoryRepo, orderRepo, userRepo, locationRepo, customProductRepo, productGroupRepo, userManager, teamRequestRepo);
    //        teamRepo = new FakeTeamRepo();
    //    }

       
    //    // cleanup and dispose
    //    public void Dispose()
    //    {
    //        teamRepo = null;
    //        teamController = null;
    //    }

    //    [Fact]
    //    public async Task TestAddTeam()
    //    {
    //        // arrange 
           


    //        var testTeam = new Team()
    //        {
    //            TeamID = 32,
    //            Name = "test32"
    //        };
    //        await teamRepo.AddTeam(testTeam);

    //        // act
    //        List<Team> returnedTeams = teamRepo.GetTeams;

    //        // asert
    //        Assert.Equal(testTeam, returnedTeams.Find(team => team == testTeam));
    //    }

    //    [Fact]
    //    public async Task TestRemoveTeam()
    //    {
    //        // arrange 
    //        var testTeam = new Team()
    //        {
    //            TeamID = 32,
    //            Name = "test"
    //        };
    //        await teamRepo.AddTeam(testTeam);

    //        // act
    //        await teamRepo.RemoveTeam(testTeam.TeamID);

    //        // assert
    //        Assert.DoesNotContain(testTeam, teamRepo.GetTeams);
    //    }

    //    [Fact]
    //    public async Task TestUpdateTeam()
    //    {
    //        // arrange 
    //        var testTeam = new Team()
    //        {
    //            TeamID = 32,
    //            Name = "test"
    //        };
    //        var updatedTeam = new Team()
    //        {
    //            TeamID = 32,
    //            Name = "notTest"
    //        };
    //        await teamRepo.AddTeam(testTeam);

    //        // act
    //        await teamRepo.UpdateTeam(updatedTeam);

    //        // assert
    //        Assert.DoesNotContain(testTeam, teamRepo.GetTeams);
    //        Assert.Contains(updatedTeam, teamRepo.GetTeams);
    //    }

    //    [Fact] 
    //    public async Task TestFindTeamById()
    //    {
    //        // arrange 
    //        var testTeam = new Team()
    //        {
    //            TeamID = 321,
    //            Name = "test"
    //        };
    //        await teamRepo.AddTeam(testTeam);

    //        // act
    //        var team = await teamRepo.FindTeamById(321);

    //        // assert
    //        Assert.Equal(testTeam.TeamID.ToString(), team.TeamID.ToString());
    //    }
    //    /*
    //            [Fact]
    //            public async Task TestTeamSearch()
    //            {
    //                // arrange 
    //                var testTeam = new Team()
    //                {
    //                    TeamID = 32,
    //                    Name = "test"
    //                };
    //                await teamRepo.AddTeam(testTeam);
    //                const string searchTerm = "test";
    //                // act
    //                var searchResults = (List<Team>)teamController.SearchTeams(searchTerm).Result.ViewData.Model;
    //                // assert
    //                Assert.Contains(testTeam, searchResults);
    //            }
    //            */

    //    [Fact]
    //    public async Task TestMarkInactive()
    //    {
    //        var testTeam = new Team()
    //        {
    //            TeamID = 321,
    //            Name = "test",
    //            IsTeamInactive = false
    //        };
    //        await teamRepo.AddTeam(testTeam);

           

    //        await teamRepo.MarkInactiveById(321);

    //        var team = await teamRepo.FindTeamById(321);

    //        Assert.Equal(team.IsTeamInactive, testTeam.IsTeamInactive);
    //    }



    //    [Fact]
    //    public async Task TestFindTeamByProduct()
    //    {
    //        // arrange
    //        var groupList = new List<ProductGroup>();
    //        groupList.Add(new ProductGroup());

    //        var colorArr = new string[] { "Blue", "Green", "Red" };
    //        var sizeArr = new string[] { "Small", "Medium", "Large" };
    //        var productCategoryArr = new string[] { "Pants", "Shirt", "Jersey" };
    //        var teamCategoryArr = new string[] { "Sports", "Business", "Casual" };
    //        //var categoryDesc = new string[] { "For your legs", "For your torso", "For your team" };

    //        var colors = new List<ProductColor>();
    //        var sizes = new List<ProductSize>();
    //        var productCategories = new List<ProductCategory>();
    //        var teamCategories = new List<TeamCategory>();

    //        for (var i = 0; i < 3; i++)
    //        {
    //            var color = new ProductColor()
    //            {
    //                IsColorActive = true,
    //                ColorName = colorArr[i]
    //            };
    //            var size = new ProductSize()
    //            {
    //                IsSizeActive = true,
    //                SizeName = sizeArr[i]
    //            };
    //            var productCategory = new ProductCategory()
    //            {
    //                Name = productCategoryArr[i],
    //                //BriefDescription = categoryDesc[i]
    //            };
    //            var teamCategory = new TeamCategory()
    //            {
    //                Name = teamCategoryArr[i],
    //            };

    //            colors.Add(color);
    //            sizes.Add(size);
    //            productCategories.Add(productCategory);
    //            teamCategories.Add(teamCategory);

    //        }

    //            RosterProduct product1 = new RosterProduct
    //        {
    //            RosterProductID = 1,
    //            ModelNumber = 1,
    //            BasePrice = 10,
    //            IsProductActive = true,
    //            SKU = 1,
    //            BaseColor = colors[0],
    //            BaseSize = sizes[0],
    //            Category = productCategories[0]
    //        };
    //        PricingHistory pricingHistory = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 1, 25),
    //            NewPrice = 25
    //        };
    //        PricingHistory pricingHistory2 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 2, 20),
    //            NewPrice = 30
    //        };
    //        product1.AddPricingHistory(pricingHistory);
    //        product1.AddPricingHistory(pricingHistory2);


    //        RosterProduct product2 = new RosterProduct
    //        {
    //            RosterProductID = 2,
    //            ModelNumber = 2,
    //            BasePrice = 30,
    //            IsProductActive = true,
    //            SKU = 1,
    //            BaseColor = colors[1],
    //            BaseSize = sizes[1],
    //            Category = productCategories[1]
    //        };
    //        PricingHistory pricingHistory3 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2019, 5, 25),
    //            NewPrice = 50
    //        };
    //        PricingHistory pricingHistory4 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 4, 8),
    //            NewPrice = 80
    //        };
    //        product2.AddPricingHistory(pricingHistory3);
    //        product2.AddPricingHistory(pricingHistory4);


    //        RosterProduct product3 = new RosterProduct
    //        {
    //            RosterProductID = 3,
    //            ModelNumber = 3,
    //            BasePrice = 80,
    //            IsProductActive = true,
    //            SKU = 1,
    //            BaseColor = colors[2],
    //            BaseSize = sizes[2],
    //            Category = productCategories[2]
    //        };
    //        PricingHistory pricingHistory5 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2019, 12, 25),
    //            NewPrice = 70
    //        };
    //        PricingHistory pricingHistory6 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 3, 8),
    //            NewPrice = 120
    //        };
    //        product3.AddPricingHistory(pricingHistory5);
    //        product3.AddPricingHistory(pricingHistory6);


    //        CustomProduct customProduct = new CustomProduct
    //        {
    //            CustomProductID = 1,
    //            BaseProduct = product1,
    //            ProductPNG = "http://placekitten.com/200/300",
    //            IsProductActive = true
    //        };
    //        PricingHistory pricingHistory9 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 2, 25),
    //            NewPrice = 75
    //        };
    //        PricingHistory pricingHistory10 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 4, 5),
    //            NewPrice = 100
    //        };
    //        customProduct.AddPricingHistory(pricingHistory9);
    //        customProduct.AddPricingHistory(pricingHistory10);


    //        CustomProduct customProduct2 = new CustomProduct
    //        {
    //            CustomProductID = 2,
    //            BaseProduct = product2,
    //            ProductPNG = "http://placekitten.com/200/300",
    //            IsProductActive = true
    //        };
    //        PricingHistory pricingHistory11 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 2, 19),
    //            NewPrice = 75
    //        };
    //        PricingHistory pricingHistory12 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 4, 1),
    //            NewPrice = 100
    //        };
    //        customProduct2.AddPricingHistory(pricingHistory11);
    //        customProduct2.AddPricingHistory(pricingHistory12);


    //        CustomProduct customProduct3 = new CustomProduct
    //        {
    //            CustomProductID = 3,
    //            BaseProduct = product3,
    //            ProductPNG = "http://placekitten.com/200/300",
    //            IsProductActive = true
    //        };
    //        PricingHistory pricingHistory13 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 3, 5),
    //            NewPrice = 75
    //        };
    //        PricingHistory pricingHistory14 = new PricingHistory
    //        {
    //            DateChanged = new DateTime(2020, 4, 2),
    //            NewPrice = 100
    //        };
    //        customProduct3.AddPricingHistory(pricingHistory13);
    //        customProduct3.AddPricingHistory(pricingHistory14);


    //        groupList[0].ChildProducts = new List<CustomProduct>() { customProduct, customProduct2, customProduct3 };


           

    //        var testTeam = new Team()
    //        {
    //            TeamID = 321,
    //            Name = "test"
    //        };

    //        testTeam.AddProductGroup(groupList[0]);
    //        await teamRepo.AddTeam(testTeam);

    //        // act
    //        Team t = await teamRepo.FindTeamByProductId(2);

    //        // assert

    //        // TODO

    //        Assert.Equal("test", t.Name);
    //    }
    //}


}
