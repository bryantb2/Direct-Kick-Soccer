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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using dropShippingApp.APIModels;
using Newtonsoft.Json;

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
        private ICustomProductRepo customProductRepo;

        public CartController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                IConfiguration envConfig,
                ITeamRepo teamRepo,
                IOrderRepo orderRepo,
                ICartRepo cartRepo,
                IUserRepo userRepo,
                ICustomProductRepo customProductRepo)
        {
            this.userManager = usrMgr;
            this.signInManager = signinMgr;
            this.configuration = envConfig;
            this.teamRepo = teamRepo;
            this.orderRepo = orderRepo;
            this.cartRepo = cartRepo;
            this.userRepo = userRepo;
            this.customProductRepo = customProductRepo;
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
            var user = await userRepo.GetUserDataAsync(HttpContext.User);

            // verify they havae the cartItemId
            var foundItem = user.Cart.CartItems.Find(item => item.CartItemID == cartItemId);

            // remove cart item
            if(foundItem != null)
                await cartRepo.RemoveCartItem(cartItemId);
            return RedirectToAction("Index");
        }

        // add item to cart
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // get user
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            
            if(quantity > 0)
            {
                // check if product already in cart
                var existingCartItem = user.Cart.CartItems.Find(item => item.ProductSelection.CustomProductID == productId);
                if (existingCartItem != null)
                {
                    // update existing
                    existingCartItem.Quantity += quantity;
                    await cartRepo.UpdateCartItem(existingCartItem);
                }
                else
                {
                    // add new item
                    // get product
                    var foundProduct = await customProductRepo.GetCustomProductById(productId);
                    if (foundProduct == null)
                        return NotFound();

                    // add to DB
                    var newItem = new CartItem()
                    {
                        Quantity = quantity,
                        ProductSelection = foundProduct
                    };
                    await cartRepo.AddCartItem(newItem);

                    // add to cart and user
                    user.Cart.AddItem(newItem);
                    await userManager.UpdateAsync(user);
                }

                // redirect to cart
                return RedirectToAction("Index");
            }

            return RedirectToAction("ViewProduct", "Product", new
            {
                productId = productId
            });
        }

        // update cart contents
        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] List<CartItemViewModel> cartItems) //UpdateCartVM cart)
        {
            // change cart items quantities
            var user = await userManager.GetUserAsync(HttpContext.User);

            // update cart items
            for (var i = 0; i < cartItems.Count; i++) //cart.CartItems.Count; i++)
            {
                // find item and update
                var currentItem = cartItems[i]; //cart.CartItems[i];
                if(currentItem.Quantity > 0)
                {
                    // update if greater than 0
                    var foundItem = await cartRepo.GetCartItemById(currentItem.ItemID);
                    if (foundItem != null)
                    {
                        foundItem.Quantity = currentItem.Quantity;
                        await cartRepo.UpdateCartItem(foundItem);
                    }
                }
                else
                {
                    // remove item (less than or equal to 0 in quantity)
                    await cartRepo.RemoveCartItem(currentItem.ItemID);
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
