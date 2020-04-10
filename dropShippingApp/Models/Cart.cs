using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int SessionID { get; set; } // optional for guest users that do not have accounts
        public List<CartItem> CartItems { get; set; }
    }
}
