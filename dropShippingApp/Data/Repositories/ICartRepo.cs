using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ICartRepo
    {
        Task UpdateCart(Cart cart);
        List<Cart> GetCarts { get; }
        Task<Cart> FindCartById(int cartId);
        Task<Cart> FindCartByItemId(int itemId);
        Task AddCartItem(CartItem item); // JUST for adding to DB context
        Task<CartItem> RemoveCartItem(int itemId); // JUST for removing from DB context
        Task UpdateCartItem(CartItem item); // JUST for updating to DB context
    }
}
