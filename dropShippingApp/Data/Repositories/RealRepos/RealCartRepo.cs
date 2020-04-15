using dropShippingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealCartRepo : ICartRepo
    {
        private ApplicationDbContext context;
        public RealCartRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public async Task UpdateCart(Cart cart)
        {
            this.context.Carts.Update(cart);
            await this.context.SaveChangesAsync();
        }

        public List<Cart> GetCarts
        {
            get
            {
                var carts = this.context.Carts
                    .Include(cart => cart.CartItems) // get custom product
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                    .Include(cart => cart.CartItems) // get custom product tags
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.ProductTags)
                    .Include(cart => cart.CartItems) // get custom pricing history
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.PricingHistory)
                    .Include(cart => cart.CartItems) // get roster base color
                        .ThenInclude(cartItem => cartItem.ProductSelection) 
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseColor)
                    .Include(cart => cart.CartItems) // get roster base size
                        .ThenInclude(cartItem => cartItem.ProductSelection) 
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseSize)
                    .Include(cart => cart.CartItems) // get roster product tags
                        .ThenInclude(cartItem => cartItem.ProductSelection) 
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.ProductTags)
                    .Include(cart => cart.CartItems) // get pricing history for roster product
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.PricingHistory)
                    .ToList();
                return carts;
            }
        }

        public async Task<Cart> FindCartById(int cartId)
        {
            return this.context.Carts
                    .Include(cart => cart.CartItems) // get custom product
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                    .Include(cart => cart.CartItems) // get custom product tags
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.ProductTags)
                    .Include(cart => cart.CartItems) // get custom pricing history
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.PricingHistory)
                    .Include(cart => cart.CartItems) // get roster base color
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseColor)
                    .Include(cart => cart.CartItems) // get roster base size
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseSize)
                    .Include(cart => cart.CartItems) // get roster product tags
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.ProductTags)
                    .Include(cart => cart.CartItems) // get pricing history for roster product
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.PricingHistory)
                    .ToList()
                    .Find(cart => cart.CartID == cartId);
        }

        public async Task<Cart> FindCartByItemId(int itemId)
        {
            var foundCart = this.context.Carts
                .Include(cart => cart.CartItems) // get custom product
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                    .Include(cart => cart.CartItems) // get custom product tags
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.ProductTags)
                    .Include(cart => cart.CartItems) // get custom pricing history
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.PricingHistory)
                    .Include(cart => cart.CartItems) // get roster base color
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseColor)
                    .Include(cart => cart.CartItems) // get roster base size
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseSize)
                    .Include(cart => cart.CartItems) // get roster product tags
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.ProductTags)
                    .Include(cart => cart.CartItems) // get pricing history for roster product
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.PricingHistory)
                    .ToList()
                    .Find(cart => 
                        cart.CartItems.Find(item => item.CartItemID == itemId) != null);
            return foundCart;
        }

        public async Task AddCartItem(CartItem item)
        {
            this.context.CartItems.Add(item);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateCartItem(CartItem item)
        {
            this.context.CartItems.Update(item);
            await this.context.SaveChangesAsync();
        }
    }
}
