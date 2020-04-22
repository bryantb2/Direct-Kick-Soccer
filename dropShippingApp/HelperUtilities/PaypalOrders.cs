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

    // TODO: add shipping cost calculator because it needs to be factored into the final sale price

    public class PaypalOrder
    {
        public async static Task<PayPalCheckoutSdk.Orders.Order> CreateOrder(IConfiguration configuration, ITeamRepo teamRepo, AppUser user, decimal shippingPrice = 0)
        {
            // build out request and order JSON
            var request = new OrdersCreateRequest();
            request.Headers.Add("prefer", "return=representation");
            request.RequestBody(
                await BuildOrderRequestBody(teamRepo, 
                    user.Id.ToString(), 
                    user.Cart, 
                    configuration["PaypalCredentials:MerchantID"]));

            // setup transaction
            var response = await PayPalClient.Client(configuration)
                .Execute(request);
            return response.Result<PayPalCheckoutSdk.Orders.Order>();
        }

        private async static Task<OrderRequest> BuildOrderRequestBody(ITeamRepo teamRepo, string appUserID, Cart cart, string DKMerchantID)
        {
            // build purchase units
            // construct order request object
            var purchaseUnits = await GenerateUnitsByTeam(teamRepo, appUserID, cart.CartItems, DKMerchantID);

            OrderRequest orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",

                // setting up paypal window behavior on client side?
                ApplicationContext = new ApplicationContext()
                {
                    BrandName = "Direct Kick",
                    LandingPage = "BILLING",
                    UserAction = "CONTINUE",
                    ShippingPreference = "GET_FROM_FILE" // this will require either the merchant or client to specify a shipping address
                },
                // purchase unit represents a purchase of one or more items from a seller
                // there are many purchase units if there are many sellers (AKA team shops)
                PurchaseUnits = purchaseUnits
            };

            return orderRequest;
        }

        private async static Task<List<PurchaseUnitRequest>> GenerateUnitsByTeam(ITeamRepo teamRepo, string userID, List<CartItem> cartItems, string merchantID)
        {
            // get unique teams list
            // create unit foreach unique team
                // generate items
                // generate breakdown
            var purchaseUnits = new List<PurchaseUnitRequest>();
            var teamProduct = await FindAndReturnTeamProducts(teamRepo, cartItems);
            foreach(var team in teamProduct)
            {
                var teamItems = GeneratePUnitItems(team.Products);
                var teamBreakdown = GeneratePUnitBreakdown(team.Products);

                purchaseUnits.Add(new PurchaseUnitRequest()
                {
                    ReferenceId = merchantID,
                    Description = "Clothing and Apparel",
                    CustomId = userID, // links transaction to app user
                    AmountWithBreakdown = teamBreakdown,
                    Items = teamItems
                });
            }

            return purchaseUnits;
        }

        private async static Task<List<TeamProduct>> FindAndReturnTeamProducts(ITeamRepo teamRepo, List<CartItem> cartItems)
        {
            // go through all products in cart
            // find team based on productId
            // if teamid doesn't already exist in teamList
                // create team product object and add current custom product id
            // else
                // take team product object in list and add current product id to it
            var teamList = new List<TeamProduct>();
            for(var i = 0; i < cartItems.Count; i++)
            {
                var currentCartItem = cartItems[i];
                var foundTeam = await teamRepo.FindTeamByProductId(currentCartItem.ProductSelection.CustomProductID);

                if (foundTeam != null)
                {
                    // team does NOT exist in list
                    if (!teamList.Exists(team => team.TeamID == foundTeam.TeamID))
                    {
                        var newTeam = new TeamProduct()
                        {
                            TeamID = foundTeam.TeamID,
                            Products = new List<CartItem>() { currentCartItem }
                        };
                        teamList.Add(newTeam);
                    }
                    // team DOES exist in list
                    else
                    {
                        var existingTeamIndex = teamList.FindIndex(team => team.TeamID == foundTeam.TeamID);

                        var productList = teamList[existingTeamIndex].Products;
                        productList.Add(currentCartItem);

                        // remove at index (because structs are val types and not ref)
                        teamList.RemoveAt(existingTeamIndex);

                        // add new team val
                        teamList.Add(new TeamProduct()
                        {
                            TeamID = foundTeam.TeamID,
                            Products = productList
                        });
                    }
                }
            }
            return teamList;
        }

        private static List<Item> GeneratePUnitItems(List<CartItem> cartItems)
        {
            var itemList = new List<Item>();
            foreach(var item in cartItems)
            {
                itemList.Add(new Item()
                {
                    Name = item.ProductSelection.ProductTitle,
                    Description = item.ProductSelection.ProductDescription,
                    Sku = item.ProductSelection.BaseProduct.SKU.ToString(),
                    UnitAmount = new Money()
                    {
                        CurrencyCode = "USD",
                        Value = item.ProductSelection.CurrentPrice.ToString("0.##")
                    },
                    Quantity = item.Quantity.ToString(),
                    Category = "PHYSICAL_GOODS"
                });
            }
            return itemList;
        }

        private static AmountWithBreakdown GeneratePUnitBreakdown(List<CartItem> cartItems)
        {
            // calc cart item total
            var itemTotal = CalculateItemTotal(cartItems);
            var breakdown = new AmountWithBreakdown()
            {
                CurrencyCode = "USD",
                Value = itemTotal.ToString("0.##"),
                AmountBreakdown = new AmountBreakdown()
                {
                    ItemTotal = new Money
                    {
                        CurrencyCode = "USD",
                        Value = itemTotal.ToString("0.##")
                    },
                    Shipping = new Money
                    {
                        CurrencyCode = "USD",
                        Value = "0.00" // fix this later
                    }
                }
            };
            return breakdown;
        }

        private static decimal CalculateItemTotal(List<CartItem> cartItems)
        {
            decimal totalPrice = 0m;
            foreach (var item in cartItems)
            {
                var unitPrice = item.ProductSelection.CurrentPrice;
                totalPrice += (unitPrice * item.Quantity);
            }
            return totalPrice;
        }

        private struct TeamProduct
        {
            public int TeamID { get; set; }
            public List<CartItem> Products { get; set; }
        }
    }
}
