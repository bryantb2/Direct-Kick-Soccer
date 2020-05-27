using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class ProductSelectionViewModel
    {
        public ProductGroup ProductGroup { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
