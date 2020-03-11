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
        FakeRProductRepo fr;
        ProductController controller;
        PricingHistory ph;
        
        
        public  UnitTest1()
        {
            fr = new FakeRProductRepo();
           // var controller = new ProductController(fr);


        }

        [Fact]
        public async Task TestAdd()
        {
            controller = new ProductController(fr);
            ph = new PricingHistory { PricingHistoryID = 1, DateChanged = new DateTime(2020, 3, 10, 6, 30, 0), NewPrice = 100.00m };
            int x = await controller.AddPricingHistory(ph);
           // var result = await controller.Index();
           // var viewResult = (ViewResult)result;
            //var pricelist = (List<PricingHistory>)viewResult.Model;
            //Assert.Equal(1,x);
            Assert.Equal(ph, fr.Ph.Find(x => x.PricingHistoryID == 1));
        }

        [Fact]
        public async Task TestRemove()
        {
            controller = new ProductController(fr);
            ph = new PricingHistory { PricingHistoryID = 1, DateChanged = new DateTime(2020, 3, 10, 6, 30, 0), NewPrice = 100.00m };
            int x = await controller.AddPricingHistory(ph);
            PricingHistory pxh = await controller.RemovePricingHistory(1);
            Assert.False(fr.Ph.Exists(x => x.PricingHistoryID == 1));
        }

    }
}
