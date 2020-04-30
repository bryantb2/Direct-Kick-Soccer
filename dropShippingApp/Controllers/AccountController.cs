using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dropShippingApp.Controllers
{
    public class AccountController:Controller
    {
        // Private fields
        private IUserRepo userRepo;
        private UserManager<AppUser> userManager;
        private IConfiguration configuration;

        public AccountController(
            IUserRepo userRepo,
            UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            this.userRepo = userRepo;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            // get user account info
            var user = await userRepo.GetUserDataAsync(HttpContext.User);
            var accountData = BuildAccountData(user);

            // return view
            return View(accountData);
        }

        public async Task<IActionResult> ViewOrders()
        {
            // get user account info
            var user = await userRepo.GetUserDataAsync(HttpContext.User);

            // compile orders from paypal
            var accountInfo = new AccountViewModel();
            var orderList = new List<PayPalCheckoutSdk.Orders.Order>();
            for(var i = 0; i < user.UserOrderHistory.Count; i++)
            {
                var currentUserOrder = user.UserOrderHistory[i];
                var paypalOrder = await PaypalTransaction
                    .GetOrder(configuration, currentUserOrder.PaypalOrderId);
                orderList.Add(paypalOrder);
            }

            accountInfo.OrderList = orderList;

            // return view
            return View(accountInfo);
        }

        public async Task<IActionResult> UpdateEmail(string newEmail)
        {
            // set data in user manager

            // update user

            // setup user data view model

            // return view
            return View("Index");
        }

        private AccountViewModel BuildAccountData(AppUser userData)
        {
            return new AccountViewModel()
            {
                Email = userData.Email,
                FName = userData.FirstName,
                LName = userData.LastName
            };
        }
    }
}
