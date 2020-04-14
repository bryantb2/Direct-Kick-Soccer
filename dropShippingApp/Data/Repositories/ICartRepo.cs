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
    }
}
