﻿using System;
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
    public class PaypalOrders
    {
        public async static Task<HttpResponse> CreateOrder(IConfiguration configuration, ITeamRepo teamRepo, AppUser user, decimal shippingPrice = 0)
        {
            // build out request and order JSON
            var request = new OrdersCreateRequest();
            request.Headers.Add("prefer", "return=representation");
            request.RequestBody(BuildOrderRequestBody(teamRepo, user.Cart));

            // setup transaction
            var response = await PayPalClient.Client(configuration).Execute(request);
            return response;
        }

        private static OrderRequest BuildOrderRequestBody(ITeamRepo teamRepo, Cart cart)
        {
            // build purchase units
            // construct order request object
            var purchaseUnits = GenerateUnitsByTeam(teamRepo, cart.CartItems);

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

            return orderRequest;
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

        private async static List<PurchaseUnitRequest> GenerateUnitsByTeam(ITeamRepo teamRepo, List<CartItem> cartItems)
        {
            // get unique teams list
            // create unit foreach unique team
                // call cart calculate
                // generate items
                // generate breakdown
            var purchaseUnits = new List<PurchaseUnitRequest>();

            var cartTotal = CalculateItemTotal(cartItems);

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
                var customProduct = cartItems[i].ProductSelection;
                var foundTeam = await teamRepo.FindTeamByProductId(customProduct.CustomProductID);

                if (foundTeam != null)
                {
                    // team does NOT exist in list
                    if (!teamList.Exists(team => team.TeamID == foundTeam.TeamID))
                    {
                        var newTeam = new TeamProduct()
                        {
                            TeamID = foundTeam.TeamID,
                            ProductIDs = new List<int>() { customProduct.CustomProductID }
                        };
                        teamList.Add(newTeam);
                    }
                    // team DOES exist in list
                    else
                    {
                        var existingTeamIndex = teamList.FindIndex(team => team.TeamID == foundTeam.TeamID);

                        var productList = teamList[existingTeamIndex].ProductIDs;
                        productList.Add(customProduct.CustomProductID);

                        // remove at index
                        teamList.RemoveAt(existingTeamIndex);

                        // add new team val
                        teamList.Add(new TeamProduct()
                        {
                            TeamID = foundTeam.TeamID,
                            ProductIDs = productList
                        });
                    }
                }
            }
            return teamList;
        }

        /*
         * private async static Task<List<int>> FindAndReturnUniqueTeamIDs(ITeamRepo teamRepo, List<CartItem> cartItems)
        {
            // go through all products in cart
            // find team based on product id
            // add found team's id to teamList if it doesn't already exist as an element
            var teamList = new List<int>();
            for(var i = 0; i < cartItems.Count; i++)
            {
                var customProduct = cartItems[i].ProductSelection;
                var foundTeam = await teamRepo.FindTeamByProductId(customProduct.CustomProductID);

                if (foundTeam != null && !teamList.Exists(teamId => teamId == foundTeam.TeamID))
                    teamList.Add(foundTeam.TeamID);
            }
            return teamList;
        }
         */

        private static List<Item> GeneratePUnitItems(List<CartItem> cartItems)
        {

        }

        private static AmountWithBreakdown GeneratePUnitBreakdown(List<CartItem> cartItems)
        {

        }

        private struct TeamProduct
        {
            public int TeamID { get; set; }
            public List<int> ProductIDs { get; set; }
        }
    }
}
