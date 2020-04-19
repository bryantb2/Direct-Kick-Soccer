﻿using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration configuration;
        private ITeamRepo teamRepo;
        private IOrderRepo orderRepo;

        public CartController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                IConfiguration envConfig,
                ITeamRepo teamRepo,
                IOrderRepo orderRepo)
        {
            this.userManager = usrMgr;
            this.signInManager = signinMgr;
            this.configuration = envConfig;
            this.teamRepo = teamRepo;
            this.orderRepo = orderRepo;
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

        // update cart contents
        [HttpPost]
        public async Task<IActionResult> UpdateCart(/*List of Cart items (NOT VIEW MODELS)*/)
        {
            // TODO
            // check cart for item id
            // if exists, update
            // return cart index with error message
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            // get user from DB
            var user = await userManager.GetUserAsync(HttpContext.User);
            var paypalOrder = await PaypalOrder.CreateOrder(configuration, teamRepo, user);
            return Ok(paypalOrder);
        }

        [HttpPost]
        public async Task<IActionResult> GetAndSaveOrder([FromBody]int orderId)
        {
            // get user from DB
            // get order from paypal
            // parse response body
            var user = await userManager.GetUserAsync(HttpContext.User);
            var response = await PaypalTransaction.GetOrder(configuration, orderId.ToString());
            var responseData = response.Result<PayPalCheckoutSdk.Orders.Order>();

            var newOrder = new Order()
            {
                PaypalOrderId = responseData.Id
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
