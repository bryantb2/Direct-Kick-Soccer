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


namespace dropShippingApp.Controllers
{
    public class ProductController : Controller
    {
        public IRepository Repository { get; set; }

        public ProductController(IRepository repo)
        {
            Repository = repo;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<RosterProduct> result = await Repository.GetAllRosterProdAsync();
            return View(result.ToList());
        }

        public async Task<int> AddRosterProduct(RosterProduct rosterProduct) => await Repository.AddRosterProdAsync(rosterProduct);
        public async Task<RosterProduct> RemoveRosterProduct(int rosterId)
        {
             RosterProduct roster;
             roster = await Repository.RemoveRosterProdAsync(rosterId);
          //  var removedHistory = this.pricingHistory.Find(hstry => hstry.PricingHistoryID == historyId);
          //  this.pricingHistory.Remove(removedHistory);
            return roster;
        }

    }
}
