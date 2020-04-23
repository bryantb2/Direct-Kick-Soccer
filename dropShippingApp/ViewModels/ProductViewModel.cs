using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class ProductViewModel
    {
        public CustomProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}
