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
        private readonly ApplicationDbContext _context;
        IInvoiceRepo iRepo;

        public InvoicesController(IInvoiceRepo i)
        {
            iRepo = i;

        }





        //public Task<decimal> CalculateGrandTotal(Invoice i)
        //{
        //    decimal finalPrice = 0.00m;
        //    foreach (InvoiceItem item in i.InvoiceItems)
        //    {
        //        finalPrice += item.CalulateSubtotal();
        //    }
        //    return Task.FromResult<decimal>(finalPrice);
        //}








        //    // GET: Invoices
        //    public async Task<IActionResult> Index()
        //    {
        //        return View(await _context.Invoice.ToListAsync());
        //    }

        //    // GET: Invoices/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var invoice = await _context.Invoice
        //            .FirstOrDefaultAsync(m => m.InvoiceID == id);
        //        if (invoice == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(invoice);
        //    }

        //    // GET: Invoices/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Invoices/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("InvoiceID,DatePlaced")] Invoice invoice)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(invoice);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(invoice);
        //    }

        //    // GET: Invoices/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var invoice = await _context.Invoice.FindAsync(id);
        //        if (invoice == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(invoice);
        //    }

        //    // POST: Invoices/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("InvoiceID,DatePlaced")] Invoice invoice)
        //    {
        //        if (id != invoice.InvoiceID)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(invoice);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!InvoiceExists(invoice.InvoiceID))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(invoice);
        //    }

        //    // GET: Invoices/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var invoice = await _context.Invoice
        //            .FirstOrDefaultAsync(m => m.InvoiceID == id);
        //        if (invoice == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(invoice);
        //    }

        //    // POST: Invoices/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var invoice = await _context.Invoice.FindAsync(id);
        //        _context.Invoice.Remove(invoice);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool InvoiceExists(int id)
        //    {
        //        return _context.Invoice.Any(e => e.InvoiceID == id);
        //    }
    }
}
