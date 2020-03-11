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
        public IRProductRepo Repository { get; set; }

        public ProductController(IRProductRepo repo)
        {
            Repository = repo;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<PricingHistory> result = await Repository.GetAllPriceHistAsync();
            return View(result.ToList());
        }

        public async Task<int> AddPricingHistory(PricingHistory history) => await Repository.AddPriceHistAsync(history);
        public async Task<PricingHistory> RemovePricingHistory(int historyId)
        {
            PricingHistory removedHistory;
            removedHistory = await Repository.RemovePriceHistAsync(historyId);
          //  var removedHistory = this.pricingHistory.Find(hstry => hstry.PricingHistoryID == historyId);
          //  this.pricingHistory.Remove(removedHistory);
            return removedHistory;
        }

    }
}
