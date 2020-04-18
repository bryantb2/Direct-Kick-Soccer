using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPal;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using PayPalHttp;

namespace dropShippingApp.HelperUtilities
{
    /*
     *                                          --- DOCUMENTATION --- 
     *                                          
     * Paypal API documentation, definitions, and expected behaviors can be found in the following sources:
     * 
     * Definitions: https://developer.paypal.com/docs/api/glossary/#p AND https://developer.paypal.com/docs/api/orders/v1/#definition-item
     * 
     * Client/server integration use case quick guide: https://developer.paypal.com/docs/checkout/reference/server-integration/#
     * 
     * Client side integration guide: https://developer.paypal.com/docs/checkout/integrate/#
     * 
     * Server side enivornment setup guide: https://developer.paypal.com/docs/checkout/reference/server-integration/setup-sdk/#set-up-the-environment
     * 
     * Server side integration guide: https://developer.paypal.com/docs/checkout/reference/server-integration/#
     * 
     */
    public class PaypalOrders
    {
        public async static Task<HttpResponse> CreateOrder(IConfiguration configuration, AppUser user, decimal shippingPrice = 0)
        {
            // build out request and order JSON
            var request = new OrdersCreateRequest();
            request.Headers.Add("prefer", "return=representation");
            request.RequestBody(BuildOrderRequestBody(user.Cart));

            // setup transaction
            var response = await PayPalClient.Client(configuration).Execute(request);
            return response;
        }

        private static OrderRequest BuildOrderRequestBody(Cart cart)
        {
            // call cart calculate
            // build purchase units
            // construct order request object
            var cartTotal = CalculateItemTotal(cart.CartItems);
            var purchaseUnits = GenerateUnitsByTeam(cart.CartItems);

            OrderRequest orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",

                // setting up paypal window behavior on client side?
                ApplicationContext = new ApplicationContext()
                {
                    BrandName = "Direct Kick",
                    LandingPage = "Login",
                    UserAction = "CONTINUE",
                    ShippingPreference = "SET_PROVIDED_ADDRESS" // this will require either the merchane or client to specify a shipping address
                },
                // purchase unit represents a purchase of one or more items from a seller
                // there are many purchase units if there are many sellers (AKA team shops)
                PurchaseUnits = purchaseUnits
            };
        }

        private static decimal CalculateItemTotal(List<CartItem> cartItems)
        {
            decimal totalPrice = 0m;
            foreach(var item in cartItems)
            {
                var unitPrice = item.ProductSelection.CurrentPrice;
                totalPrice += (unitPrice * item.Quantity);
            }
            return totalPrice;
        }

        private static List<PurchaseUnitRequest> GenerateUnitsByTeam(List<CartItem> cartItems)
        {
            var purchaseUnits = new List<PurchaseUnitRequest>();
        }

        private static List<int> FindAndReturnUniqueTeamIDs(List<CartItem> cartItems)
        {
            var teamList = new List<int>();
            foreach(var item in cartItems)
            {

            }
        }

        private static List<Item> GenerateOrderItems(List<CartItem> cartItems)
        {

        }

        private static AmountWithBreakdown GenerateBreakdown(List<CartItem> cartItems)
        {

        }
    }
}
