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
            throw new NotImplementedException();
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
    }
}
