using dropShippingApp.Data;
using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dropShippingApp.Repositories
{
    public class FakeInvoiceRepo : IInvoiceRepo
    {
        ApplicationDbContext context;
        public FakeInvoiceRepo()
        {
            //context = appContext;
        }
        private List<Invoice> invoices = new List<Invoice>();
        public List<Invoice> Invoices { get { return invoices; } }

        IQueryable<Invoice> IInvoiceRepo.Invoices => throw new NotImplementedException();

        private List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
        public List<InvoiceItem> InvoiceItems { get { return invoiceItems; } }

        public Task<bool> AddInvoiceItem(InvoiceItem item)
        {
            bool flag = false;
            if (item != null)
            {
                invoiceItems.Add(item);
                flag = true;
            }
            return Task.FromResult<bool>(flag);
        }

        public void AddInvoice(Invoice i)
        {
            Invoices.Add(i);
        }



        public Task<InvoiceItem> RemoveInvoiceItem(int id)
        {
            InvoiceItem removedItem = null;
            foreach (InvoiceItem i in invoiceItems)
            {
                if (i.InvoiceItemID == id)
                {
                    removedItem = i;
                    invoiceItems.Remove(i);
                    return Task.FromResult<InvoiceItem>(removedItem);
                }
            }
            return Task.FromResult<InvoiceItem>(removedItem); ;
        }


        public Task<InvoiceItem> GetInvoiceItem(int id)
        {
            var list = (from invoiceItem in invoiceItems
                        where invoiceItem.InvoiceItemID == id
                        select invoiceItem).ToList();
            return Task.FromResult<InvoiceItem>(list[0]);
        }

 
        public InvoiceItem AddInvoiceItem(InvoiceItem item, int id)
        {

            if (item!=null && id!=0)
            {
                var i = (from invoice in invoices
                         where invoice.InvoiceID == id
                         select invoice).ToList().FirstOrDefault();

                i.InvoiceItems.Add(item);
                return item;
            }
            else
                return null;

        }

     

        public InvoiceItem RemoveInvoiceItem(InvoiceItem item)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> CalculateGrandTotal()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateInvoiceItem(InvoiceItem newItem)
        {
            throw new NotImplementedException();
        }
    }
}
