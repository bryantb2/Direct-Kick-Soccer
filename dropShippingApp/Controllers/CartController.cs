using Abp.Web.Mvc.Models;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Controllers
{
    public class CartController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private ICartRepo cRepo;
        

        public CartController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                ICartRepo c)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
            cRepo = c;
        }

        // index
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                CartItemVM cIVM = new CartItemVM();
                CartViewModel cartVM = new CartViewModel();
                foreach(CartItem c in user.Cart.CartItems)
                {
                    cIVM.BaseProduct = c.ProductSelection;
                    cIVM.Quantity = c.Quantity;
                    cartVM.CartItems.Add(cIVM);
                    cartVM.CartPrice += (cIVM.Quantity * cIVM.Quantity);
                }
                return View(cartVM);
                    
                // get the cart
                // loop through each cart item
                // parse cart information into CartItemVM object
                // calculate cart total
                // return cart vm

            }
            
            return View("");
        }

        // remove item from cart
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {

            await cRepo.RemoveCartItem(cartItemId);
 
            return View("Index");
        }

        // add item to cart
        public async Task<IActionResult> AddToCart(int cartItemId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            cRepo.f
            user.Cart.AddItem()
            // TODO
            // add item to user's cart
            // return 200 status
            return Ok();
        }

        // change quantity
        [HttpPost]
        public async Task<IActionResult> UpdateCart(List<CartItem> cartItems)
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                foreach (CartItem c in cartItems)
                {
                    if (user.Cart.CartItems.Contains(c))
                    {
                        await cRepo.UpdateCartItem(c);
                    }
                }
                return Ok();
            }
            catch
            {
                return NotFound(cartItems);
            }

            // TODO
            // check cart for item id
            // if exists, update
            // return cart index with error message
         
        }

        // checkout
        [HttpPost]
        public async Task<IActionResult> Checkout(/* WTF will we do? */)
        {
            // will bundle a user order together (ship engine tracking number, paypal transaction id, ect.)
            throw new NotImplementedException();
        }
    }
}
