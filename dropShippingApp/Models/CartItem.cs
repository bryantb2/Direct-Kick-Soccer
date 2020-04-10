using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public CustomProduct ProductSelection { get; set; }
        public int Quantity { get; set; }
    }
}
