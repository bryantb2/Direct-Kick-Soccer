using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Repositories
{
    public interface IInvoiceRepo
    {
        public IQueryable<Invoice> Invoices { get; }

        public InvoiceItem AddInvoiceItem(InvoiceItem item, int id);
        InvoiceItem RemoveInvoiceItem(InvoiceItem item);
        public Task<decimal> CalculateGrandTotal();
        public Task<bool> UpdateInvoiceItem(InvoiceItem newItem);
        public void AddInvoice(Invoice i);

    }
}
