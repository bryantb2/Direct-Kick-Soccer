using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data
{
    public class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.RosterProducts.Any())
            {
                RosterProduct product1 = new RosterProduct
                {
                    
                    ModelNumber = 1,
                    BasePrice = 10,
                    AddOnPrice = 15,
                    IsProductActive = true,
                };
                PricingHistory pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2020, 1, 25),
                    NewPrice = 25
                };
                product1.AddPricingHistory(pricingHistory);
                 pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 2, 20),
                    NewPrice = 30
                };
                product1.AddPricingHistory(pricingHistory);
                context.Add(product1);

                RosterProduct product2 = new RosterProduct
                {
                   
                    ModelNumber = 2,
                    BasePrice = 30,
                    AddOnPrice = 0,
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2019, 5, 25),
                    NewPrice = 50
                };
                product2.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2020, 4, 8),
                    NewPrice = 80
                };
                product2.AddPricingHistory(pricingHistory);
                context.Add(product2);
                //
                RosterProduct product3 = new RosterProduct
                {
                    
                    ModelNumber = 3,
                    BasePrice = 80,
                    AddOnPrice = 10,
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2019, 12, 25),
                    NewPrice = 70
                };
                product3.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 8),
                    NewPrice = 120
                };
                product3.AddPricingHistory(pricingHistory);
                context.Add(product3);
                //
                RosterProduct product4 = new RosterProduct
                {
                  
                    ModelNumber = 4,
                    BasePrice = 80,
                    AddOnPrice = 0,
                    IsProductActive = false,
                };
                pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2019, 12, 25),
                    NewPrice = 75
                };
                product4.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 8),
                    NewPrice = 60
                };
                product4.AddPricingHistory(pricingHistory);
                context.Add(product4);

                //
                CustomProduct customProduct = new CustomProduct
                {
                    
                    BaseProduct = product1,
                    ProductTitle = "Socks",
                    ProductDescription = "These socks make you run so fast!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2020, 2, 25),
                    NewPrice = 75
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 5),
                    NewPrice = 100
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                 customProduct = new CustomProduct
                {
                    
                    BaseProduct = product1,
                    ProductTitle = "Team Goats Socks",
                    ProductDescription = "Official Unofficial socks for the Eugene Goats!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 2, 19),
                    NewPrice = 75
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 1),
                    NewPrice = 100
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product1,
                    ProductTitle = "Ice Squid Socks",
                    ProductDescription = "Unofficial Official socks for the Eugene NHL team the Ice Squids!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2020, 3, 5),
                    NewPrice = 75
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 2),
                    NewPrice = 100
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);


                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product2,
                    ProductTitle = "Hat",
                    ProductDescription = "Great for keeping the sun out of your eyes, can also be used to hold stuff!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 7),
                    NewPrice = 25
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2020, 4, 8),
                    NewPrice = 30
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product2,
                    ProductTitle = "Fighting Honey Badgers Hat",
                    ProductDescription = "Hat supporting the local junior baseball team, the Fighting Honey Badgers",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 11),
                    NewPrice = 20
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 8),
                    NewPrice = 35
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product2,
                    ProductTitle = "Jumping Jellybeans Hat",
                    ProductDescription = "Hat for parents of the local stickball team the Jumping Jellybeans!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 12),
                    NewPrice = 15
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 7),
                    NewPrice = 25
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                   
                    BaseProduct = product2,
                    ProductTitle = "Jets Hat",
                    ProductDescription = "Hat for team members of the Jets Power Walking Team",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 13),
                    NewPrice = 15
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 6),
                    NewPrice = 25
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

               

                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product3,
                    ProductTitle = "Shirt",
                    ProductDescription = "A shirt, looking good!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 14),
                    NewPrice = 25
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 5),
                    NewPrice = 30
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product3,
                    ProductTitle = "'Chainmail' shirt",
                    ProductDescription = "For the local LARPing group, complete with insiginia",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 15),
                    NewPrice = 26
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 6),
                    NewPrice = 31
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);


                customProduct = new CustomProduct
                {
                   
                    BaseProduct = product3,
                    ProductTitle = "The Garden Snails",
                    ProductDescription = "Official shirt for the Competative Gardening Team, The Garden Slugs",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 14),
                    NewPrice = 28
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                   
                    DateChanged = new DateTime(2020, 4, 5),
                    NewPrice = 31
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                   
                    BaseProduct = product3,
                    ProductTitle = "The Sloths",
                    ProductDescription = "Unofficial Shirt of the Eugene HighSchool Sloths tracks and field team",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 13),
                    NewPrice = 29
                };
                customProduct.AddPricingHistory(pricingHistory);
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 4, 3),
                    NewPrice = 31
                };
                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);

                customProduct = new CustomProduct
                {
                    
                    BaseProduct = product4,
                    ProductTitle = "Backyard Brawlers",
                    ProductDescription = "Unofficial Shirt of the backyard wrestling troop. ",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = false,
                };
                pricingHistory = new PricingHistory
                {
                    
                    DateChanged = new DateTime(2020, 3, 13),
                    NewPrice = 29
                };

                customProduct.AddPricingHistory(pricingHistory);
                context.Add(customProduct);
                context.SaveChanges();
            }

        }
    }
}
