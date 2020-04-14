using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Cart
    {
        // private fields
        private List<CartItem> cartItems = new List<CartItem>();

        // public properties
        public int CartID { get; set; }
        public int SessionID { get; set; } // optional for guest users that do not have accounts
        public List<CartItem> CartItems { get { return this.cartItems; } }

        // public methods
        public void AddItem(CartItem item)
        {
            this.cartItems.Add(item);
        }
        public void RemoveItem(int itemId)
        {
            var foundItem = cartItems.Find(item => item.CartItemID == itemId);
            this.cartItems.Remove(foundItem);
        }
    }
}
