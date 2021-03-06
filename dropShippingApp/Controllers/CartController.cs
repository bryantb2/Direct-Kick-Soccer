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
        private IProductGroupRepo groupRepo;

        public CartController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                IConfiguration envConfig,
                ITeamRepo teamRepo,
                IOrderRepo orderRepo,
                ICartRepo cartRepo,
                IUserRepo userRepo,
                ICustomProductRepo customProductRepo,
                IProductGroupRepo groupRepo)
        {
            this.userManager = usrMgr;
            this.signInManager = signinMgr;
            this.configuration = envConfig;
            this.teamRepo = teamRepo;
            this.orderRepo = orderRepo;
            this.cartRepo = cartRepo;
            this.userRepo = userRepo;
            this.customProductRepo = customProductRepo;
            this.groupRepo = groupRepo;
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
            try
            {
                // get user
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                if (user != null)
                {
                    // build cart item VM objects
                    var totalCartPrice = 0m;
                    var cartItemVMList = new List<CartItemVM>();
                    for (var i = 0; i < user.Cart.CartItems.Count; i++)
                    {
                        // get the actual cart item
                        // add to running cart total
                        var currentCartitem = user.Cart.CartItems[i];
                        totalCartPrice += (currentCartitem.Quantity * currentCartitem.ProductSelection.CurrentPrice);

                        // get the product group item belongs to
                        var productGroup = groupRepo.GetGroupByProductId(currentCartitem.ProductSelection.CustomProductID);

                        // build cart item VM object and add to list
                        var newCartItemVM = new CartItemVM()
                        {
                            CartItem = currentCartitem,
                            ProductTitle = productGroup.Title,
                            ProductDescription = productGroup.Description,
                            GeneralThumbnail = productGroup.GeneralThumbnail
                        };
                        cartItemVMList.Add(newCartItemVM);
                    }
                    // create cart view model
                    CartViewModel cartVM = new CartViewModel()
                    {
                        CartPrice = totalCartPrice,
                        CartItemVMs = cartItemVMList
                    };
                    return View(cartVM);
                }

                return View("");
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-002",
                    Message = "An error occured while returning the page."
                };
                return View("Error", e);
                
            }
            
        }

        // remove item from cart
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                // get user
                var user = await userRepo.GetUserDataAsync(HttpContext.User);

                // verify they havae the cartItemId
                var foundItem = user.Cart.CartItems.Find(item => item.CartItemID == cartItemId);

                // remove cart item
                if (foundItem != null)
                    await cartRepo.RemoveCartItem(cartItemId);
                return RedirectToAction("Index");
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-003",
                    Message = "An error occured removing the item from the cart."
                };
                return View("Error", e);
            }

        }

        // add item to cart
        public async Task<IActionResult> AddToCart(int productGroupId, int? productId, int? quantity)
        {
            try
            {
                if (productId != null && quantity != null)
                {
                    // get user
                    var user = await userRepo.GetUserDataAsync(HttpContext.User);

                    if (quantity > 0)
                    {
                        // check if product already in cart
                        var existingCartItem = user.Cart.CartItems.Find(item => item.ProductSelection.CustomProductID == productId);
                        if (existingCartItem != null)
                        {
                            // update existing
                            existingCartItem.Quantity += (int)quantity;
                            await cartRepo.UpdateCartItem(existingCartItem);
                        }
                        else
                        {
                            // add new item
                            // get product
                            var foundProduct = customProductRepo.GetCustomProductById((int)productId);
                            if (foundProduct == null)
                                return NotFound();

                            // add to DB
                            var newItem = new CartItem()
                            {
                                Quantity = (int)quantity,
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
                }

                return RedirectToAction("ViewProduct", "Product", new
                {
                    productGroupId
                });
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-004",
                    Message = "An error occured adding the item to the cart."
                };
                return View("Error", e);
            }
        }

        // update cart contents
        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] List<UpdateCartItemVM> cartItems)
        {
            try
            {
                // change cart items quantities
                var user = await userManager.GetUserAsync(HttpContext.User);

                // update cart items
                for (var i = 0; i < cartItems.Count; i++)
                {
                    // find item and update
                    var currentItem = cartItems[i];
                    if (currentItem.Quantity > 0)
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
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-005",
                    Message = "An error occured updating the item in the cart."
                };
                return View("Error", e);
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                // get user from DB
                var user = await userRepo.GetUserDataAsync(HttpContext.User);
                // create custom order provider
                var orderProvider = new PaypalOrder(configuration, teamRepo, groupRepo, user);
                // get order from provider
                var paypalOrder = await orderProvider.GetOrder();
                // serialize order and return
                var orderJSON = JsonConvert.SerializeObject(paypalOrder);
                return Ok(orderJSON);
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-006",
                    Message = "An error occured creating the order"
                };
                return View("Error", e);
            }
 
        }

        [HttpPost]
        public async Task<IActionResult> GetAndSaveOrder([FromBody] OrderResponse order)
        {
            try
            {
            // get user from DB
            // process paypal order
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            var processedOrder = await PaypalTransaction.ProcessOrder(configuration, order.OrderID);

            // create order log to store in DB
            // save order to DB
            var newDatabaseOrder = await PaypalTransaction.BuildDatabaseOrder(groupRepo, teamRepo, orderRepo, user, order.OrderID);
            await orderRepo.AddOrder(newDatabaseOrder);

            // update user in DB
            // clear user cart
            // update user
            user.AddPurchaseOrder(newDatabaseOrder);
            await ClearCart(user.Cart.CartItems.GetRange(0, user.Cart.CartItems.Count));
            await userManager.UpdateAsync(user);

            // redirect to main cart page
            return RedirectToAction("Index");
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-007",
                    Message = "An error occured while getting the order"
                };
                return View("Error", e);
            }
        }

        private async Task ClearCart(List<CartItem> cartItems)
        {
            // loop through cart items
            for(var i = 0; i < cartItems.Count; i++)
            {
                // remove cart items from DB
                var currentCartItem = cartItems[i];
                await cartRepo.RemoveCartItem(currentCartItem.CartItemID);
            }
        }
    }
}
