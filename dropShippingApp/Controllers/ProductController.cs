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
using dropShippingApp.ViewModels;

namespace dropShippingApp.Controllers
{
    public class ProductController : Controller
    {
        private IRosterProductRepo rosterProductRepo;
        private ICustomProductRepo customProductRepo;

        public ProductController(IRosterProductRepo rosterProductRepo,
            ICustomProductRepo customProductRepo)
        {
            this.rosterProductRepo = rosterProductRepo;
            this.customProductRepo = customProductRepo;
        }

        public async Task<IActionResult> Index()
        {
            //IQueryable<PricingHistory> result = await Repository.GetAllPriceHistAsync();
            //return View(result.ToList());
            return View();
        }

        public async Task<IActionResult> Search(string searchString) 
        {
            var csProduct = CustomRepository.CustomProducts;
           
                         
            if (!String.IsNullOrEmpty(searchString))
            {
                csProduct = csProduct.Where(s => s.BaseProduct.Category.Name == searchString).OrderBy(p => p.CustomProductID).ToList();   
            }
               return View(csProduct); 
        }

        public async Task<IActionResult> PopularItems()
        {
            // TODO
            // returns team results page 
            return View();
        }

        public async Task<IActionResult> ViewProduct(int productId)
        {
            // get product
            var foundProduct = await customProductRepo.GetCustomProductById(productId);

            var productViewModel = new ProductViewModel
            {
                Product = foundProduct,
                Quantity = 1
            };

            // send to view
            return View(productViewModel);
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
