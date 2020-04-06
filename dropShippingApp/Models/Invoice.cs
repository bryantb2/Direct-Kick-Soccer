using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Invoice
    {
        // private fields
        private List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        // public properties
        public int InvoiceID { get; set; }
        public DateTime DatePlaced { get; set; }
        public List<InvoiceItem> InvoiceItems { get { return invoiceItems; } }

        // methods
        public void AddInvoiceItem(InvoiceItem item) => invoiceItems.Add(item);
        public InvoiceItem RemoveInvoiceItem(int id)
        {
            InvoiceItem removedItem = null;
            foreach(InvoiceItem i in invoiceItems)
            {
                if(i.InvoiceItemID == id)
                {
                    removedItem = i;
                    invoiceItems.Remove(i);
                    return removedItem;
                }
            }
            return removedItem;
        }

        public decimal CalculateGrandTotal()
        {
            decimal finalPrice = 0.00m;
            foreach(InvoiceItem item in invoiceItems)
            {
                finalPrice += item.CalulateSubtotal();
            }
            return finalPrice;
        }
    }
}
