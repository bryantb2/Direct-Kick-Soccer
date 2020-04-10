using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Controllers
{
    public class CartController : Controller
    {
        // index
        public async Task<IActionResult> Index(/*Pass in cart object */)
        {
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
        public async Task<IActionResult> ChangeItemQuantity(/*cart item object*/)
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
