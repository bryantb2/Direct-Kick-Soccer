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
        public IRosterProductRepo rosterRepo { get; set; }
        private ICustomProductRepo customRepo;

        public ProductController(IRosterProductRepo repo,ICustomProductRepo customProdRepo)
        {
            rosterRepo = repo;
            customRepo = customProdRepo;

        }

        public async Task<IActionResult> Index()
        {
            //IQueryable<PricingHistory> result = await Repository.GetAllPriceHistAsync();
            //return View(result.ToList());
            throw new NotImplementedException();
        }

        public async Task<IActionResult> PopularItems()
        {
            // TODO
            // returns team results page 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SortView()
        {
            List<CustomProduct> prods = (from p in customRepo.CustomProducts
                                         select p).ToList();
            return View(prods);
        }
        [HttpPost]
        public async Task<IActionResult> SortView(string command)
        {
            if (command == "Cheap")
            {
                List<CustomProduct> prods = (from p in customRepo.CustomProducts
                                             select p).ToList();

                List<CustomProduct> sortedProd = prods.OrderBy(prod => prod.CurrentPrice).ToList();

                return View(sortedProd);
            }
            else
            {
                List<CustomProduct> prods = (from p in customRepo.CustomProducts
                                             select p).ToList();

                List<CustomProduct> sortedProd = prods.OrderByDescending(prod => prod.CurrentPrice).ToList();

                return View(sortedProd);
            }

        }

       

        public ViewResult GetProductBySKU(int SKU)
        {
            RosterProduct product = new RosterProduct();
            product = rosterRepo.GetRosterProducts.First(p => p.SKU == SKU);
            
            return View(product);
        }

        public ViewResult GetProductByModelNumber(int productNum)
        {
            RosterProduct product = new RosterProduct();
            product = rosterRepo.GetRosterProducts.First(p => p.ModelNumber == productNum);
            return View(product);
        }

    }
}
