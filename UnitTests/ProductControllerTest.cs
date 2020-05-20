
using dropShippingApp.Controllers;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Data.Repositories.FakeRepos;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
     public class ProductControllerTest: IDisposable
     {
        private IRosterProductRepo rosterRepo;
        private ProductController controller;
        //private ICustomProductRepo customProductRepo;
        //private IProductSortRepo sortRepo;
        //private IProductCategoryRepo categoryRepo;
        private IProductGroupRepo productGroupRepo;
        private ITeamRepo teamRepo;

        //setup
        public ProductControllerTest()
        {
            teamRepo = new FakeTeamRepo();
            productGroupRepo = new dropShippingApp.Data.Repositories.FakeRepos.FakeProductGroupRepo();
            //categoryRepo = new FakeCategoryRepo();
            //sortRepo = new FakeSortRepo();
            //customProductRepo = new FakeCustomProductRepo();
            rosterRepo = new FakeRosterProductRepo();
            controller = new ProductController(rosterRepo,productGroupRepo, teamRepo);
        }

        public void Dispose()
        {
            productGroupRepo = null;
            rosterRepo = null;
        }
        [Fact]
        public async Task TestAddRosterProduct()
        {
            //arrange
            RosterProduct p = new RosterProduct
            {
                RosterProductID=1,
                ModelNumber=1
            };
            //act
            await rosterRepo.AddRosterProduct(p);
            List<RosterProduct> products = rosterRepo.GetRosterProducts;

            //assert
            Assert.Equal(p, products.Find(prod => prod == p));
        }
        [Fact]
        public async Task TestGetProdById()
        {
            //arrange
            RosterProduct p = new RosterProduct
            {
                RosterProductID = 5,
                ModelNumber = 1
            };
            await rosterRepo.AddRosterProduct(p);
            //act
            RosterProduct result = await rosterRepo.GetRosterProductById(5);

            //assert
            Assert.Equal(p, result);
        }
        [Fact]
        public async Task TestRemoveProd()
        {
            //arrange
            RosterProduct p = new RosterProduct
            {
                RosterProductID = 5,
                ModelNumber = 1
            };
            await rosterRepo.AddRosterProduct(p);

            //act
            await rosterRepo.RemoveRosterProduct(p.RosterProductID);
            //assert
            Assert.DoesNotContain(p, rosterRepo.GetRosterProducts);

        }
        [Fact]
        public async Task TestUpdateProd()
        {
            //arrange
            RosterProduct p = new RosterProduct
            {
                RosterProductID = 5,
                ModelNumber = 1,
                
            };
            await rosterRepo.AddRosterProduct(p);

            RosterProduct p2 = new RosterProduct
            {
                RosterProductID=5,
                ModelNumber=2
            };

            //act
            await rosterRepo.UpdateRosterProduct(p2);

            //assert
            Assert.Contains(p2, rosterRepo.GetRosterProducts);
            Assert.DoesNotContain(p, rosterRepo.GetRosterProducts);
        }
        ////////////////////Controller Tests///////////////////////
        ///,getprod by sku, get prod by modelnum

        [Fact]
        public async Task TestViewProductAsync()
        {
            //arrange
            RosterProduct p = new RosterProduct
            {
                RosterProductID = 5,
                ModelNumber = 1,
               

            };
            await rosterRepo.AddRosterProduct(p);
            PricingHistory price = new PricingHistory
            {
                PricingHistoryID = 1,
                DateChanged = DateTime.Now,
                NewPrice = 5
            };
            CustomProduct customProduct = new CustomProduct
            {
                BaseProduct=p,
                CustomProductID=1,
                

            };
            customProduct.PricingHistory.Add(price);
            List<CustomProduct> prods = new List<CustomProduct>();
            prods.Add(customProduct);
            ProductGroup group = new ProductGroup
            {
                ProductGroupID=1,
                ChildProducts=prods
            };
            await productGroupRepo.AddProductGroup(group);
            ProductSelectionViewModel pvm = new ProductSelectionViewModel
            {
                ProductGroup=group,
                Quantity=0,
                ProductId=0
            };
            //act
            var result = await controller.ViewProduct(group.ProductGroupID);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
     
            ProductSelectionViewModel r = (ProductSelectionViewModel)viewResult.ViewData.Model;
            Assert.Equal(pvm.ProductId,r.ProductId);
            Assert.Equal(pvm.ProductGroup, r.ProductGroup);
            Assert.Equal(pvm.Quantity, r.Quantity);
        }
        [Fact]
        public async Task TestSearch()
        {
            //arrange
            RosterProduct p = new RosterProduct
            {
                RosterProductID = 5,
                ModelNumber = 1,


            };
            await rosterRepo.AddRosterProduct(p);
            PricingHistory price = new PricingHistory
            {
                PricingHistoryID = 1,
                DateChanged = DateTime.Now,
                NewPrice = 5
            };
            CustomProduct customProduct = new CustomProduct
            {
                BaseProduct = p,
                CustomProductID = 1,


            };
            customProduct.PricingHistory.Add(price);
            List<CustomProduct> prods = new List<CustomProduct>();
            prods.Add(customProduct);
            Tag tag = new Tag 
            {
                TagLine="test line",

            };
            List<Tag> tags = new List<Tag>();
            tags.Add(tag);
            ProductGroup group = new ProductGroup
            {
                Title="Soccer Socks",
                ProductGroupID = 1,
                ChildProducts = prods,
                Description="some socks",
                ProductTags=tags
            };
            await productGroupRepo.AddProductGroup(group);
            List<ProductGroup> groups = new List<ProductGroup>();
            groups.Add(group);
            BrowseViewModel vmToTest = new BrowseViewModel
            {
                ProductGroups = groups
            };
            ///act
            var result = await controller.Search("socks");


            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            
            BrowseViewModel vm = (BrowseViewModel)viewResult.ViewData.Model;
            Assert.Equal(vm.ProductGroups[0].Title, vmToTest.ProductGroups[0].Title);
        }
        //returns a team
        [Fact]
        public async Task ViewTeamProdTest()
        {
            //arrange
            Team t = new Team
            {
                Name="test team",
                TeamID=1
            };
            await teamRepo.AddTeam(t);
            //act
            var result = await controller.ViewTeamProduct(t.TeamID);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Team teamResult = (Team)viewResult.ViewData.Model;

            Assert.Equal(t.TeamID, teamResult.TeamID);
            Assert.Equal(t.Name, teamResult.Name);

        }
        
        
     }
}
