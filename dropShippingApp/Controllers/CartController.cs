using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using dropShippingApp.APIModels;

namespace dropShippingApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IConfiguration configuration;
        private ITeamRepo teamRepo;
        private IOrderRepo orderRepo;
        private ICartRepo cartRepo;
        private IUserRepo userRepo;

        public CartController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                IConfiguration envConfig,
                ITeamRepo teamRepo,
                IOrderRepo orderRepo,
                ICartRepo cartRepo,
                IUserRepo userRepo)
        {
            this.userManager = usrMgr;
            this.signInManager = signinMgr;
            this.configuration = envConfig;
            this.teamRepo = teamRepo;
            this.orderRepo = orderRepo;
            this.cartRepo = cartRepo;
            this.userRepo = userRepo;
        }

        // ------------------- PHASE 1
        // user sends request to create order
        // server send back order as JSON
        // paypal completes order
        // when order completes, data is sent from client back to server as JSON
        // log order id for proper Appuser object


        // ------------------- PHASE 2
        // admin goes through list of orders and picks order
        // order gets packaged at facility
        // admin clicks button on site to generate shipping label, initial shipping status is set
        // shipping id is then hooked into the user's order object
        // package is sent 


        // view cart
        public async Task<IActionResult> Index()
        {
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            if (user != null)
            {
                CartViewModel cartVM = new CartViewModel();
                var cartItemList = new List<CartItem>();
                foreach(CartItem item in user.Cart.CartItems)
                {
                    // add to running cart total
                    cartVM.CartPrice += (item.Quantity * item.ProductSelection.CurrentPrice);
                    // add item to list
                    cartItemList.Add(item);
                }
                // set item list
                cartVM.CartItems = cartItemList;
                return View(cartVM);
            }
            
            return View("");
        }

        // remove item from cart
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            // get user
            var user = await userManager.GetUserAsync(HttpContext.User);
            // verify they havae the cartItemId
            var hasItem = false;
            foreach(var item in user.Cart.CartItems)
            {
                if (item.CartItemID == cartItemId)
                {
                    hasItem = true;
                    break;
                }
            }
            // remove cart item
            if(hasItem)
                await cartRepo.RemoveCartItem(cartItemId);
            return RedirectToAction("Index");
        }

        // add item to cart
        public async Task<IActionResult> AddToCart(int cartItemId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            // TODO
            // add item to user's cart
            // return 200 status
            return Ok();
        }

        // update cart contents
        [HttpPost]
        public async Task<IActionResult> UpdateCart(List<CartItem> cartItems)
        {
            // change cart items quantities
            var user = await userManager.GetUserAsync(HttpContext.User);
            foreach (CartItem c in cartItems)
            {
                if (user.Cart.CartItems.Contains(c))
                {
                    await cartRepo.UpdateCartItem(c);
                }
            }

            // return refreshed cart page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            // get user from DB
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            var paypalOrder = await PaypalOrder.CreateOrder(configuration, teamRepo, user);
            var orderJSON = JsonConvert.SerializeObject(paypalOrder);
            return Ok(orderJSON);
        }

        [HttpPost]
        public async Task<IActionResult> GetAndSaveOrder([FromBody] OrderResponse order)
        {
            // get user from DB
            // get order from paypal
            // parse response body
            var user = await userManager.GetUserAsync(HttpContext.User);
            //var response = await PaypalTransaction.GetOrder(configuration, order.OrderID);
            //var responseData = response.Result<PayPalCheckoutSdk.Orders.Order>();

            var processedOrder = await PaypalTransaction.ProcessOrder(configuration, order.OrderID);

            var newOrder = new Order()
            {
                PaypalOrderId = order.OrderID
            };

            // save order to DB
            await orderRepo.AddOrder(newOrder);

            // update user in DB
            user.AddPurchaseOrder(newOrder);
            await userManager.UpdateAsync(user);

            // redirect to main cart page
            return RedirectToAction("Index");
        }
    }
}
