using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPal;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace dropShippingApp.HelperUtilities
{
    public class PaypalOrders
    {
        public async static Task<HttpResponse> CreateOrder(AppUser user)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(BuildOrderRequestBody(user));
        }

        private static OrderRequest BuildOrderRequestBody(AppUser user)
        {

        }
    }
}
