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
        private IProductGroupRepo customGroupRepo;
        private ICustomProductRepo customProductRepo;

        public AccountController(UserManager<AppUser> usrMgr, 
            SignInManager<AppUser> signInMgr,
            IUserRepo userRepo,
            IOrderRepo orderRepo,
            IConfiguration config,
            IProductGroupRepo customGroupRepo,
            ICustomProductRepo customProductRepo)
        {
            signInManager = signInMgr;
            userManager = usrMgr;
            this.userRepo = userRepo;
            this.orderRepo = orderRepo;
            this.config = config;
            this.customGroupRepo = customGroupRepo;
            this.customProductRepo = customProductRepo;
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
                var currentOrder = userData.UserOrderHistory[i];
                // request order data from paypal
                var orderData = await PaypalTransaction.GetOrder(config, currentOrder.PaypalOrderId);

                // build product list from database order
                var invoiceProductList = new List<ProductVM>();
                foreach(var product in currentOrder.ProductIDs)
                {
                    // get product group and individual product from DB
                    // determine unit price at time of sale
                    var productGroupData = customGroupRepo.GetGroupByProductId(int.Parse(product));
                    var productData = customProductRepo.GetCustomProductById(int.Parse(product));
                    var unitPriceAtSale = productData.GetPriceAtTimeOfSale(currentOrder.DatePlaced);
                    // create product VM
                    var productVM = new ProductVM()
                    {
                        ProductGroupId = productGroupData.ProductGroupID,
                        Title = productGroupData.Title,
                        Description = productGroupData.Description,
                        GeneralThumbnail = productGroupData.GeneralThumbnail,
                        UnitPrice = unitPriceAtSale,
                        Size = productData.BaseProduct.BaseSize,
                        Color = productData.BaseProduct.BaseColor
                    };
                    invoiceProductList.Add(productVM);
                }
                
                // create an invoice view model that will get added to the order list
                var invoiceVM = new InvoiceVM()
                {
                    BaseOrder = currentOrder,
                    PurchasedProducts = invoiceProductList
                };
                userOrderList.Add(invoiceVM);
            }
            return View("ViewInvoices", userOrderList);
        }
    }
}
