using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class CartViewModel
    {
        public Decimal CartPrice { get; set; }
        public List<CartItemVM> CartItems { get; set; }
    }
}
