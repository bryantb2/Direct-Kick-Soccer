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
        public IRosterProductRepo RosterRepository { get; set; }

        public ICustomProductRepo CustomRepository {get; set; }

        public ProductController(IRosterProductRepo rosterrepo, ICustomProductRepo customrepo)
        {
            RosterRepository = rosterrepo;
            CustomRepository = customrepo;
        }

        public async Task<IActionResult> Index()
        {
            //IQueryable<PricingHistory> result = await Repository.GetAllPriceHistAsync();
            //return View(result.ToList());
            throw new NotImplementedException();
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
    }
}
