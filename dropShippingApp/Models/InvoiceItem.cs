using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class InvoiceItem
    {
        // public properties
        public int InvoiceItemID { get; set; }
        public CustomProduct PurchasedProduct { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public int ItemQuantity { get; set; }

        // methods
        public decimal CalulateSubtotal()
        {
            return (ItemQuantity * ProductUnitPrice);
        }
    }
}
