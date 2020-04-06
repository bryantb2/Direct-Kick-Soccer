using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data;
using dropShippingApp.Models;
using dropShippingApp.Repositories;

namespace dropShippingApp.Controllers
{
    public class InvoicesController : Controller
    {
        IInvoiceRepo invoiceRepo;

        public InvoicesController(IInvoiceRepo i)
        {
            invoiceRepo = i;
        }

        /*public InvoiceItem CreateInvoiceItem(CustomProduct prod, decimal price, int qtny, int id)
        {

            if (prod != null && id != 0 && qtny != 0 && price != 0)
            {

                InvoiceItem item = new InvoiceItem
                {
                    PurchasedProduct = prod,
                    ProductUnitPrice = price,
                    ItemQuantity = qtny,

                };
               InvoiceItem i=iRepo.AddInvoiceItem(item, id);
                return i;
            }
            return null;
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInvoice(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoiceRepo.AddInvoice(invoice);
                //_context.Add(invoice);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }
    }
}
