using dropShippingApp.Models;
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

        public CartController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
        }

        // index
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                // get the cart
                // loop through each cart item
                // parse cart information into CartItemVM object
                // calculate cart total
                // return cart vm

            }
            return View();
        }

        // remove item from cart
        public async Task<IActionResult> RemoveFromCart(/* cart item id */)
        {
            return View("Index");
        }

        // add item to cart
        public async Task<IActionResult> AddToCart(/* cart item object*/)
        {
            // TODO
            // add item to user's cart
            // return 200 status
            return Ok();
        }

        // change quantity
        [HttpPost]
        public async Task<IActionResult> UpdateCart(/*List of Cart items (NOT VIEW MODELS)*/)
        {
            // TODO
            // check cart for item id
            // if exists, update
            // return cart index with error message
            throw new NotImplementedException();
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
