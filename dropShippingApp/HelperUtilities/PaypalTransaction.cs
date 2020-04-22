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
    }
}
