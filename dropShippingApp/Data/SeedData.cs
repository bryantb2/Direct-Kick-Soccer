using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data
{
    public class SeedData
    {
        public static async void Seed(ApplicationDbContext context, IServiceProvider prov)
        {
            // check context
            context.Database.EnsureCreated();

            // get services
            UserManager<AppUser> userManager = prov.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = prov.GetRequiredService<RoleManager<IdentityRole>>(); 

            if (!context.RosterProducts.Any())
            {
                // ------------------------------------------- ADDING APP USERS AND ROLES ------------------------------------------- //
                var user1 = new AppUser
                {
                    UserName = "NoobSlayer",
                    NormalizedUserName = "NOOBSLAYER",
                    Email = "abc123@gmail.com",
                    NormalizedEmail = "ABC123@GMAIL.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };
                var user2 = new AppUser
                {
                    UserName = "Kalashnikov",
                    NormalizedUserName = "KALASHNIKOV",
                    Email = "ak47@yahoo.com",
                    NormalizedEmail = "AK47@YAHOO.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };
                var user3 = new AppUser
                {
                    UserName = "ItalianCowboy",
                    NormalizedUserName = "ITALIANCOWBODY",
                    Email = "cowboy@gmail.com",
                    NormalizedEmail = "COWBOY@GMAIL.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                var userArr = new AppUser[3] { user1, user2, user3 };
                var userPasswordArr = new String[3] { "WhoaDude123!", "MotherRussia123!", "MeatBallRevolver123!" };
                var roles = new string[3] { "standard", "manager", "admin" };

                // assign passwords and add users to DB
                for (var i = 0; i < userPasswordArr.Length; i++)
                {
                    // hash and assign password
                    var hasher = new PasswordHasher<AppUser>();
                    var hashedPassword = hasher.HashPassword(userArr[i], userPasswordArr[i]);
                    userArr[i].PasswordHash = hashedPassword;
                    // add user
                    await userManager.CreateAsync(userArr[i]);
                }

                // adding roles to DB
                for (var i = 0; i < roles.Length; i++)
                {
                    // add role if it doesn't exist
                    if (await roleManager.FindByNameAsync(roles[i]) == null)
                        await roleManager.CreateAsync(new IdentityRole(roles[i]));
                }

                // add role to users
                await userManager.AddToRoleAsync(user1, roles[0]);
                await userManager.AddToRoleAsync(user2, roles[1]);
                await userManager.AddToRoleAsync(user3, roles[2]);


                // ------------------------------------------- ADDING ROSTER PRODUCTS ------------------------------------------- //
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
                PricingHistory pricingHistory2 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 2, 20),
                    NewPrice = 30
                };
                product1.AddPricingHistory(pricingHistory);
                product1.AddPricingHistory(pricingHistory2);

                
                RosterProduct product2 = new RosterProduct
                {
                    ModelNumber = 2,
                    BasePrice = 30,
                    AddOnPrice = 0,
                    IsProductActive = true,
                };
                PricingHistory pricingHistory3 = new PricingHistory
                {
                    DateChanged = new DateTime(2019, 5, 25),
                    NewPrice = 50
                };
                PricingHistory pricingHistory4 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 8),
                    NewPrice = 80
                };
                product2.AddPricingHistory(pricingHistory3);
                product2.AddPricingHistory(pricingHistory4);


                RosterProduct product3 = new RosterProduct
                {
                    ModelNumber = 3,
                    BasePrice = 80,
                    AddOnPrice = 10,
                    IsProductActive = true,
                };
                PricingHistory pricingHistory5 = new PricingHistory
                {
                    DateChanged = new DateTime(2019, 12, 25),
                    NewPrice = 70
                };
                PricingHistory pricingHistory6 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 8),
                    NewPrice = 120
                };
                product3.AddPricingHistory(pricingHistory5);
                product3.AddPricingHistory(pricingHistory6);


                RosterProduct product4 = new RosterProduct
                {
                    ModelNumber = 4,
                    BasePrice = 80,
                    AddOnPrice = 0,
                    IsProductActive = false,
                };
                PricingHistory pricingHistory7 = new PricingHistory
                {
                    DateChanged = new DateTime(2019, 12, 25),
                    NewPrice = 75
                };
                PricingHistory pricingHistory8 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 8),
                    NewPrice = 60
                };
                product4.AddPricingHistory(pricingHistory7);
                product4.AddPricingHistory(pricingHistory8);

                // SAVING ROSTER PRODUCTS TO CONTEXT
                context.RosterProducts.Add(product1);
                context.RosterProducts.Add(product2);
                context.RosterProducts.Add(product3);
                context.RosterProducts.Add(product4);

                // ------------------------------------------- ADDING CUSTOM PRODUCTS ------------------------------------------- //
                CustomProduct customProduct = new CustomProduct
                {
                    BaseProduct = product1,
                    ProductTitle = "Socks",
                    ProductDescription = "These socks make you run so fast!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory9 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 2, 25),
                    NewPrice = 75
                };
                PricingHistory pricingHistory10 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 5),
                    NewPrice = 100
                };
                customProduct.AddPricingHistory(pricingHistory9);
                customProduct.AddPricingHistory(pricingHistory10);


                CustomProduct customProduct2 = new CustomProduct
                {
                    BaseProduct = product1,
                    ProductTitle = "Team Goats Socks",
                    ProductDescription = "Official Unofficial socks for the Eugene Goats!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory11 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 2, 19),
                    NewPrice = 75
                };
                PricingHistory pricingHistory12 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 1),
                    NewPrice = 100
                };
                customProduct2.AddPricingHistory(pricingHistory);
                customProduct2.AddPricingHistory(pricingHistory);


                CustomProduct customProduct3 = new CustomProduct
                {
                    BaseProduct = product1,
                    ProductTitle = "Ice Squid Socks",
                    ProductDescription = "Unofficial Official socks for the Eugene NHL team the Ice Squids!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory13 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 5),
                    NewPrice = 75
                };
                PricingHistory pricingHistory14 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 2),
                    NewPrice = 100
                };
                customProduct3.AddPricingHistory(pricingHistory13);
                customProduct3.AddPricingHistory(pricingHistory14);


                CustomProduct customProduct4 = new CustomProduct
                {
                    BaseProduct = product2,
                    ProductTitle = "Hat",
                    ProductDescription = "Great for keeping the sun out of your eyes, can also be used to hold stuff!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory15 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 7),
                    NewPrice = 25
                };
                PricingHistory pricingHistory16 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 8),
                    NewPrice = 30
                };
                customProduct4.AddPricingHistory(pricingHistory15);
                customProduct4.AddPricingHistory(pricingHistory16);


                CustomProduct customProduct5 = new CustomProduct
                {
                    BaseProduct = product2,
                    ProductTitle = "Fighting Honey Badgers Hat",
                    ProductDescription = "Hat supporting the local junior baseball team, the Fighting Honey Badgers",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory17 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 11),
                    NewPrice = 20
                };
                PricingHistory pricingHistory18 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 8),
                    NewPrice = 35
                };
                customProduct5.AddPricingHistory(pricingHistory17);
                customProduct5.AddPricingHistory(pricingHistory18);


                CustomProduct customProduct6 = new CustomProduct
                {
                    BaseProduct = product2,
                    ProductTitle = "Jumping Jellybeans Hat",
                    ProductDescription = "Hat for parents of the local stickball team the Jumping Jellybeans!",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory19 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 12),
                    NewPrice = 15
                };
                PricingHistory pricingHistory20 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 7),
                    NewPrice = 25
                };
                customProduct6.AddPricingHistory(pricingHistory19);
                customProduct6.AddPricingHistory(pricingHistory20);


                CustomProduct customProduct7 = new CustomProduct
                {
                    BaseProduct = product2,
                    ProductTitle = "Jets Hat",
                    ProductDescription = "Hat for team members of the Jets Power Walking Team",
                    CustomImagePNG = "http://placekitten.com/200/300",
                    IsProductActive = true,
                };
                PricingHistory pricingHistory21 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 3, 13),
                    NewPrice = 15
                };
                PricingHistory pricingHistory22 = new PricingHistory
                {
                    DateChanged = new DateTime(2020, 4, 6),
                    NewPrice = 25
                };
                customProduct7.AddPricingHistory(pricingHistory21);
                customProduct7.AddPricingHistory(pricingHistory22);

                // SAVING CUSTOM PRODUCTS TO CONTEXT
                context.CustomProducts.Add(customProduct);
                context.CustomProducts.Add(customProduct2);
                context.CustomProducts.Add(customProduct3);
                context.CustomProducts.Add(customProduct4);
                context.CustomProducts.Add(customProduct5);
                context.CustomProducts.Add(customProduct6);
                context.CustomProducts.Add(customProduct7);


                // ------------------------------------------- ADDING AND ASSIGNMENT CARTS TO USERS ------------------------------------------- //
                CartItem item1 = new CartItem()
                {
                    ProductSelection = customProduct,
                    Quantity = 2
                };
                CartItem item2 = new CartItem()
                {
                    ProductSelection = customProduct5,
                    Quantity = 1
                };
                Cart cart1 = new Cart()
                {

                };





































                /*customProduct = new CustomProduct
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
                context.Add(customProduct);*/
                context.SaveChanges();
            }

        }
    }
}
