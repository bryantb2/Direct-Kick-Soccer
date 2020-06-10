using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.ViewModels
{
    public class InvoiceVM
    {
        public Order BaseOrder { get; set; }
        public List<ProductVM> PurchasedProducts { get; set; }
    }
}
