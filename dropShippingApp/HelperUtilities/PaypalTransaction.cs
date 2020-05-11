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
using dropShippingApp.Data.Repositories;

namespace dropShippingApp.HelperUtilities
{
    public class PaypalTransaction
    {
        /*
         *                           --- DOCUMENTATION --- 
         *                           
         * Authorize payments after order is created: https://developer.paypal.com/docs/integration/direct/payments/authorize-and-capture-payments/#
         * 
         * General Order API Info: https://developer.paypal.com/docs/checkout/reference/server-integration/set-up-transaction/#on-the-server
         * 
         * Orders API Reference Guide: https://developer.paypal.com/docs/api/orders/v2/
         * 
         */

        public async static Task<HttpResponse> GetOrder(IConfiguration configuration, string orderId)
        {
            var request = new OrdersGetRequest(orderId);
            var response = await PayPalClient.Client(configuration).Execute(request);
            return response;
        }

        public async static Task<PayPalCheckoutSdk.Orders.Order> ProcessOrder(IConfiguration configuration, string orderId)
        {
            // setup call to paypal servers for processing order (move funds from buyer to seller)
            var request = new OrdersCaptureRequest(orderId);
            request.Headers.Add("prefer", "return=representation");
            request.RequestBody(new OrderActionRequest());

            // call paypal servers
            var response = await PayPalClient.Client(configuration).Execute(request);

            // return order object
            return response.Result<PayPalCheckoutSdk.Orders.Order>();
        }

        public async static Task<dropShippingApp.Models.Order> BuildDatabaseOrder(
            IProductGroupRepo groupRepo,
            ITeamRepo teamRepo,
            AppUser purchaser,
            string paypalOrderID)
        {
            var cartItems = purchaser.Cart.CartItems;
            // get all: team, product family, product ids from user's cart items
            var teamIdsList = new List<string>();
            var productIdsList = new List<string>();
            var groupIdsList = new List<string>();
            for(var i = 0; i < cartItems.Count; i++)
            {
                var currentItem = cartItems[i];
                // check product id
                if (!productIdsList.Contains(currentItem.CartItemID.ToString()))
                    productIdsList.Add(currentItem.CartItemID.ToString());
                // get and check group id
                var groupId = groupRepo.GetGroupByProductId(currentItem.CartItemID);
                if (!groupIdsList.Contains(groupId.ToString()))
                    groupIdsList.Add(groupId.ToString());
                // get and check team id
                var teamid = await teamRepo.FindTeamByProductId(currentItem.CartItemID);
                if (!teamIdsList.Contains(teamid.ToString()))
                    teamIdsList.Add(teamid.ToString());
            }

            dropShippingApp.Models.Order newDBOrder = new dropShippingApp.Models.Order()
            {
                PaypalOrderId = paypalOrderID,
                ProductFamilyIDs = groupIdsList.ToArray(),
                ProductIDs = productIdsList.ToArray(),
                TeamIDs = teamIdsList.ToArray()
            };
            return newDBOrder;
        }
    }
}
