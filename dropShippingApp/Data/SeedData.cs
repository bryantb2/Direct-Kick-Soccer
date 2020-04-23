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
        public static async void Seed(IServiceProvider prov)
        {
            // get services
            UserManager<AppUser> userManager = prov.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = prov.GetRequiredService<RoleManager<IdentityRole>>();
            ApplicationDbContext context = prov.GetRequiredService<ApplicationDbContext>();

            // check context
            context.Database.EnsureCreated();

            if (!context.RosterProducts.Any())
            {
                // ------------------------------------------- ADDING PRODUCT PROPERTIES ------------------------------------------- //
                var colorArr = new string[] { "Blue", "Green", "Red" };
                var sizeArr = new string[] { "Small", "Medium", "Large" };
                var categoryArr = new string[] { "Pants", "Shirt", "Jersey" };
                //var categoryDesc = new string[] { "For your legs", "For your torso", "For your team" };

                var colors = new List<ProductColor>();
                var sizes = new List<ProductSize>();
                var categories = new List<ProductCategory>();

                for (var i = 0; i < 3; i++)
                {
                    var color = new ProductColor()
                    {
                        IsColorActive = true,
                        ColorName = colorArr[i]
                    };
                    var size = new ProductSize()
                    {
                        IsSizeActive = true,
                        SizeName = sizeArr[i]
                    };
                    var category = new ProductCategory()
                    {
                        Name = categoryArr[i],
                        //BriefDescription = categoryDesc[i]
                    };

                    context.ProductColors.Add(color);
                    context.ProductSizes.Add(size);
                    context.Categories.Add(category);

                    await context.SaveChangesAsync();

                    // add to local lists
                    colors.Add(color);
                    sizes.Add(size);
                    categories.Add(category);
                }


                // ------------------------------------------- ADDING ROSTER PRODUCTS ------------------------------------------- //
                RosterProduct product1 = new RosterProduct
                {
                    ModelNumber = 1,
                    BasePrice = 10,
                    IsProductActive = true,
                    SKU = 1,
                    BaseColor = colors[0],
                    BaseSize = sizes[0],
                    Category = categories[0]
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
                    IsProductActive = true,
                    SKU = 1,
                    BaseColor = colors[1],
                    BaseSize = sizes[1],
                    Category = categories[1]
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
                    IsProductActive = true,
                    SKU = 1,
                    BaseColor = colors[2],
                    BaseSize = sizes[2],
                    Category = categories[2]
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
                    IsProductActive = false,
                    SKU = 1,
                    BaseColor = colors[0],
                    BaseSize = sizes[1],
                    Category = categories[2]
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
                Country america = new Country()
                {
                    CountryName = "America"
                };

                Province oregon = new Province()
                {
                    ProvinceName = "Oregon"
                };

                context.Provinces.Add(oregon);
                context.Countries.Add(america);
                await context.SaveChangesAsync();
                america.AddProvidence(oregon);
                context.Countries.Update(america);
                await context.SaveChangesAsync();

                Team team = new Team()
                {
                    Name = "Test",
                    Description = "Test team",
                    Country = america,
                    Providence = oregon,
                    StreetAddress = "abc123 street",
                    ZipCode = "97490",
                    CorporatePageURL = "google.com",
                    BusinessEmail = "abc@gmail.com",
                    PhoneNumber = "541-234-4040",
                    IsTeamInactive = false,
                    IsHostTeam = true
                };

                team.AddProduct(customProduct);
                team.AddProduct(customProduct2);
                team.AddProduct(customProduct3);
                team.AddProduct(customProduct4);
                team.AddProduct(customProduct5);
                team.AddProduct(customProduct6);
                team.AddProduct(customProduct7);
                context.Teams.Add(team);
                await context.SaveChangesAsync();

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
                CartItem item3 = new CartItem()
                {
                    ProductSelection = customProduct,
                    Quantity = 2
                };
                CartItem item4 = new CartItem()
                {
                    ProductSelection = customProduct7,
                    Quantity = 1
                };
                CartItem item5 = new CartItem()
                {
                    ProductSelection = customProduct5,
                    Quantity = 2
                };
                CartItem item6 = new CartItem()
                {
                    ProductSelection = customProduct7,
                    Quantity = 2
                };

                // save cart items to context
                context.CartItems.Add(item1);
                context.CartItems.Add(item2);
                context.CartItems.Add(item3);
                context.CartItems.Add(item4);
                context.CartItems.Add(item5);
                context.CartItems.Add(item6);

                Cart cart1 = new Cart();
                Cart cart2 = new Cart();
                Cart cart3 = new Cart();
                cart1.AddItem(item1);
                cart1.AddItem(item2);
                cart2.AddItem(item3);
                cart2.AddItem(item4);
                cart3.AddItem(item5);
                cart3.AddItem(item6);

                // save carts to context
                context.Carts.Add(cart1);
                context.Carts.Add(cart2);
                context.Carts.Add(cart3);

                // save charts to appuser
                var carts = new List<Cart>() { cart1, cart2, cart3 };

                // ------------------------------------------- ADDING APP USERS AND ROLES ------------------------------------------- //
                var user1 = new AppUser
                {
                    FirstName = "Thor69",
                    LastName = "Of Assguard",
                    UserName = "NoobSlayer",
                    NormalizedUserName = "NOOBSLAYER",
                    Email = "abc123@gmail.com",
                    NormalizedEmail = "ABC123@GMAIL.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Cart = carts[0]
                };
                var user2 = new AppUser
                {
                    FirstName = "Straight",
                    LastName = "Fire",
                    UserName = "Kalashnikov",
                    NormalizedUserName = "KALASHNIKOV",
                    Email = "ak47@yahoo.com",
                    NormalizedEmail = "AK47@YAHOO.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Cart = carts[1]
                };
                var user3 = new AppUser
                {
                    FirstName = "Luigi",
                    LastName = "Gangsta",
                    UserName = "ItalianCowboy",
                    NormalizedUserName = "ITALIANCOWBODY",
                    Email = "cowboy@gmail.com",
                    NormalizedEmail = "COWBOY@GMAIL.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Cart = carts[2]
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


                // ------------------------------------------- ADDING PRODUCT CATAGORIES ------------------------------------------- //
                var c = new string[] { "T-Shirts", "Polos/Knits", "Sweatshirts/Fleece", "Caps", "Activewear", "Outerwear", 
                    "Woven/Dress Shirts", "Workwear/Medical/Scrubs", "Bags", "Acessories", "Ladies/Women", "Youth" };

                for (int i = 0; i < c.Length; i++)
                {
                    ProductCategory newCatagory = new ProductCategory()
                    {
                        Name = c[i]
                    };
                    // Add to the DB
                    context.Categories.Add(newCatagory);

                    // Add to the local list 
                    categories.Add(newCatagory);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
