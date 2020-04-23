using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;
using dropShippingApp.Data;
using dropShippingApp.Models;
using dropShippingApp.Data.Repositories;

namespace dropShippingApp.Controllers
{
    public class ProductController : Controller
    {
        public IRosterProductRepo Repository { get; set; }

        public ProductController(IRosterProductRepo repo)
        {
            Repository = repo;
        }

        public async Task<IActionResult> Index()
        {
            //IQueryable<PricingHistory> result = await Repository.GetAllPriceHistAsync();
            //return View(result.ToList());
            return View();
        }

        public async Task<IActionResult> PopularItems()
        {
            // TODO
            // returns team results page 
            return View();
        }

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
