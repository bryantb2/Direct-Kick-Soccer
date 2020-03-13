using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dropShippingApp.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Controllers
{
    public class CustomProductController : Controller
    {
        private ICustomProductRepo repo;
        private FakeCustomProductRepo repo1;

        public CustomProductController(ICustomProductRepo r)
        {
            repo = r;
        }

        // just for testing... ICustomProductRepo is having issues
        // will fix later
        public CustomProductController(FakeCustomProductRepo repo1)
        {
            this.repo1 = repo1;
        }

        public ViewResult GetProductBySKU(int SKU)
        {
            CustomProduct product = new CustomProduct();
            product = repo.GetProductBySKU(SKU);
            return View(product);
        }
    }
}