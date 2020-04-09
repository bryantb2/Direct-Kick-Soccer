using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data;
using dropShippingApp.Models;


namespace dropShippingApp.Controllers
{
    public class InvoicesController : Controller
    {

        //IInvoiceRepo invoiceRepo;

        //public InvoicesController(IInvoiceRepo i)
        //{
        //    invoiceRepo = i;
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateInvoice(Invoice invoice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        invoiceRepo.AddInvoice(invoice);
        //        //_context.Add(invoice);
        //        //await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(invoice);
        //}
    }
}
