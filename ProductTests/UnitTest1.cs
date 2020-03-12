using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dropShippingApp.Data;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using dropShippingApp.Controllers;

using Xunit;



namespace ProductTests
{
    public class UnitTest1
    {
        FakeRepository fr;
        ProductController controller;
        RosterProduct rosterProduct;
        PricingHistory ph;
        
        
        public  UnitTest1()
        {
            fr = new FakeRepository();
            controller = new ProductController(fr);
            ph = new PricingHistory { PricingHistoryID = 1, DateChanged = new DateTime(2020, 3, 10, 6, 30, 0), NewPrice = 100.00m };
            rosterProduct = new RosterProduct { AddOnPrice = 100.00m, BaseColor = new ProductColor { ColorName = "red", IsColorActive = true, ProductColorID = 230 }, BaseSize = new ProductSize { IsSizeActive = true, SizeName = "10w", ProductSizeID = 135 }, BasePrice = 50.00m, IsProductActive = true, ModelNumber = 123, ProductName = "sports shirt", ProductDescription = "sports", RosterProductID = 5, SKU = 80 };
        }

        [Fact]
        public async Task TestAdd()
        {
            rosterProduct.AddPricingHistory(ph);
            Assert.True(rosterProduct.PricingHistory.Exists(x => x.PricingHistoryID == 1));
            int x = await controller.AddRosterProduct(rosterProduct);
          
            Assert.True( fr.Rp.Exists(x => x.RosterProductID == 5));
        }

        [Fact]
        public async Task TestRemove()
        {
            rosterProduct.RemovePricingHistory(1);
            Assert.False(rosterProduct.PricingHistory.Exists(x => x.PricingHistoryID == 1));
            int x = await controller.AddRosterProduct(rosterProduct);
            RosterProduct rp = await controller.RemoveRosterProduct(5);
            Assert.False(fr.Rp.Exists(x => x.RosterProductID == 5));
        }

    }
}
