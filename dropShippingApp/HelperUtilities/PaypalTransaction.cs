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
        public async static Task<HttpResponse> GetOrder(IConfiguration configuration, string orderId)
        {
            var request = new OrdersGetRequest(orderId);
            var response = await PayPalClient.Client(configuration).Execute(request);
            return response;
        }
    }
}
