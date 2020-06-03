using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using dropShippingApp.Data.Repositories;
using dropShippingApp.HelperUtilities;
using Microsoft.Extensions.Configuration;

namespace dropShippingApp.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserRepo userRepo;
        private IOrderRepo orderRepo;
        private IConfiguration config;

        public AccountController(UserManager<AppUser> usrMgr, 
            SignInManager<AppUser> signInMgr,
            IUserRepo userRepo,
            IOrderRepo orderRepo,
            IConfiguration config)
        {
            signInManager = signInMgr;
            userManager = usrMgr;
            this.userRepo = userRepo;
            this.orderRepo = orderRepo;
            this.config = config;
        }

        public async Task<IActionResult> Index()
        {
            return View("ViewAccount");
        }

        public async Task<IActionResult> Invoices()
        {
            // get user data
            var userData = await userRepo.GetUserDataAsync(HttpContext.User);
            // loop through user orders, create product vm
            var userOrderList = new List<InvoiceVM>();
            for (var i = 0; i < userData.UserOrderHistory.Count; i++)
            {
                // request order data from paypal
                var currentOrder = userData.UserOrderHistory[i];
                var orderData = await PaypalTransaction.GetOrder(config, currentOrder.PaypalOrderId);
                // get date sold
            }
            return View("ViewInvoices", userOrderList);
        }
    }
}
