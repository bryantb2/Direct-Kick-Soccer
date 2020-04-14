using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Controllers
{
    public class CustomProductController : Controller
    {
        private ICustomProductRepo repo;

        public CustomProductController(ICustomProductRepo r)
        {
            repo = r;
        }

        // Not currently implemented

        //public ViewResult GetProductBySKU(int SKU)
        //{
        //    CustomProduct product = new CustomProduct();
        //    product = repo.CustomProducts.First(p => p.SKU == SKU);
        //    return View(product);
        //}

        //public ViewResult GetProductByModelNumber(int productNum)
        //{
        //    CustomProduct product = new CustomProduct();
        //    product = repo.CustomProducts.First(p => p.ModelNumber == productNum);
        //    return View(product);
        //}
    }
}