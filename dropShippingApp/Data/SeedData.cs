using dropShippingApp.Models;
using dropShippingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;

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
                var productCategoryArr = new string[] { "Pants", "Shirt", "Jersey" };
                var teamCategoryArr = new string[] { "Sports", "Business", "Casual" };

                var colors = new List<ProductColor>();
                var sizes = new List<ProductSize>();
                var productCategories = new List<ProductCategory>();
                var teamCategories = new List<TeamCategory>();

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
                    var productCategory = new ProductCategory()
                    {
                        Name = productCategoryArr[i],
                        //BriefDescription = categoryDesc[i]
                    };
                    var teamCategory = new TeamCategory()
                    {
                        Name = teamCategoryArr[i],
                    };

                    context.ProductColors.Add(color);
                    context.ProductSizes.Add(size);
                    context.ProductCategories.Add(productCategory);
                    context.TeamCategories.Add(teamCategory);

                    await context.SaveChangesAsync();

                    // add to local lists
                    colors.Add(color);
                    sizes.Add(size);
                    productCategories.Add(productCategory);
                    teamCategories.Add(teamCategory);
                }


                // ------------------------------------------- ADDING ROSTER PRODUCTS AND ROSTER GROUPS ------------------------------------------- //
                RosterGroup group1 = new RosterGroup()
                {
                    Description = "The shirt group from sandmar",
                    Title = "Shirts",
                    ModelNumber = 1,
                    GeneralThumbnail = "https://picsum.photos/200",
                    Category = productCategories[0]
                };
                RosterGroup group2 = new RosterGroup()
                {
                    Description = "Sandmar Jeans",
                    Title = "Jeans",
                    ModelNumber = 2,
                    GeneralThumbnail = "https://picsum.photos/200",
                    Category = productCategories[1]
                };
                RosterGroup group3 = new RosterGroup()
                {
                    Description = "Athletic Shoes",
                    Title = "Shoes",
                    ModelNumber = 3,
                    GeneralThumbnail = "https://picsum.photos/200",
                    Category = productCategories[2]
                };

                RosterProduct product1 = new RosterProduct
                {
                    IsProductActive = true,
                    SKU = 1,
                    BaseColor = colors[0],
                    BaseSize = sizes[0],
                    RosterGroup = group1
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
                    IsProductActive = true,
                    SKU = 2,
                    BaseColor = colors[1],
                    BaseSize = sizes[1],
                    RosterGroup = group1
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
                    IsProductActive = true,
                    SKU = 1,
                    BaseColor = colors[2],
                    BaseSize = sizes[2],
                    RosterGroup = group2
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
                    IsProductActive = false,
                    SKU = 2,
                    BaseColor = colors[0],
                    BaseSize = sizes[1],
                    RosterGroup = group2
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
                context.RosterGroups.Add(group1);
                context.RosterGroups.Add(group2);
                context.RosterGroups.Add(group3);
                context.RosterProducts.Add(product1);
                context.RosterProducts.Add(product2);
                context.RosterProducts.Add(product3);
                context.RosterProducts.Add(product4);


                // ------------------------------------------- ADDING CUSTOM PRODUCTS ------------------------------------------- //
                CustomProduct customProduct = new CustomProduct
                {
                    BaseProduct = product1,
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                customProduct2.AddPricingHistory(pricingHistory11);
                customProduct2.AddPricingHistory(pricingHistory12);


                CustomProduct customProduct3 = new CustomProduct
                {
                    BaseProduct = product1,
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                    ProductPNG = "http://placekitten.com/200/300",
                    IsProductActive = true
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
                await context.SaveChangesAsync();

                
                // ------------------------------------------- ADDING PRODUCT FAMILIES ------------------------------------------- //
                var groupNames = new string[] { "Best Socks", "Lazy Pants", "Bedazzled Shirts" };
                var groupDesc = new string[] { "Mhmmmm tasty", "For the morbidly obese", "Extremely cool" };
                var groupList = new List<ProductGroup>();

                for (var i = 0; i < 3; i++)
                {
                    ProductGroup group = new ProductGroup()
                    {
                        Title = groupNames[i],
                        Description = groupDesc[i],
                        GeneralThumbnail = "https://i.picsum.photos/id/174/200/200.jpg",
                        PrintDesignPNG = "https://i.picsum.photos/id/174/200/200.jpg",
                        BaseGroupModelNumber = i + 1
                    };

                    context.ProductGroups.Add(group);
                    await context.SaveChangesAsync();
                    groupList.Add(group);
                }

                // adding products to group
                groupList[0].ChildProducts = new List<CustomProduct>() { customProduct, customProduct2, customProduct3 };
                groupList[1].ChildProducts = new List<CustomProduct>() { customProduct4, customProduct5, customProduct6 };
                groupList[2].ChildProducts = new List<CustomProduct>() { customProduct7 };

                context.ProductGroups.Update(groupList[0]);
                context.ProductGroups.Update(groupList[1]);
                context.ProductGroups.Update(groupList[2]);
                await context.SaveChangesAsync();


                // ------------------------------------------- ADDING COUNTRY/STATE ------------------------------------------- //
                List<Country> countries = JsonUtil.DeserializeCountry();
                Country aCountry;

                foreach (Country myCountry in countries)
                {
                    aCountry = new Country
                    {
                        CountryName = myCountry.CountryName,


                    };
                    context.Add(aCountry);
                }
                await context.SaveChangesAsync();

                Province aProvience;
                List<Province> provinces = JsonUtil.DeserializeProvinces();
                Country us = (from theCountry in countries
                              where theCountry.CountryName == "United States of America"
                              select theCountry).First();
                foreach (Province myProvience in provinces)
                {
                    aProvience = new Province
                    {
                        ProvinceName = myProvience.ProvinceName,
                        ProvienceAbbreviation = myProvience.ProvienceAbbreviation


                    };
                    context.Add(aProvience);
                    us.AddProvidence(aProvience);

                }
                await context.SaveChangesAsync();

                Province california = new Province()
                {
                    ProvinceName = "California"
                };

                Province florida = new Province()
                {
                    ProvinceName = "Florida"
                };

                // ------------------------------------------- ADDING AND ASSIGNMENT CARTS TO USERS ------------------------------------------- //

                Country america = (from country in context.Countries
                                   where country.CountryName.ToLower() == "united states of america"
                                   select country).First();

                Province oregon = (from state in context.Provinces
                                   where state.ProvinceName.ToLower() == "oregon"
                                   select state).First();

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
                    IsHostTeam = true,
                    Category = teamCategories[0],
                    TeamBannerPNG = "https://picsum.photos/id/237/200/300"
                };

                Team team1 = new Team()
                {
                    Name = "Non-owner test team",
                    Description = "Soccer Team",
                    Country = america,
                    Providence = california,
                    DateJoined = DateTime.Now,
                    StreetAddress = "apple avenue",
                    ZipCode = "37401",
                    CorporatePageURL = "bing.com",
                    BusinessEmail = "abc@yahoo.com",
                    PhoneNumber = "541-554-4157",
                    IsTeamInactive = false,
                    IsHostTeam = false,
                    Category = teamCategories[1],
                    TeamBannerPNG = "https://picsum.photos/id/237/200/300"
                };

                Team team2 = new Team()
                {
                    Name = "Beach Test Team",
                    Description = "Hunting alligators",
                    Country = america,
                    Providence = florida,
                    DateJoined = DateTime.Now,
                    StreetAddress = "Florida Beach 123 Street",
                    ZipCode = "55555",
                    CorporatePageURL = "youtube.com",
                    BusinessEmail = "assgaurd2020@gmail.com",
                    PhoneNumber = "377-849-9071",
                    IsTeamInactive = false,
                    IsHostTeam = false,
                    Category = teamCategories[2],
                    TeamBannerPNG = "https://picsum.photos/id/237/200/300"
                };

                team.AddProductGroup(groupList[0]);
                team1.AddProductGroup(groupList[1]);
                team2.AddProductGroup(groupList[2]);
                context.Teams.Add(team);
                context.Teams.Add(team1);
                context.Teams.Add(team2);
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
                    Email = "standard@test.com",
                    NormalizedEmail = "STANDARD@TEST.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Cart = carts[0]
                };
                var user2 = new AppUser
                {
                    FirstName = "Straight",
                    LastName = "Fire",
                    UserName = "Kalashnikov",
                    NormalizedUserName = "KALASHNIKOV",
                    Email = "manager@test.com",
                    NormalizedEmail = "MANAGER@TEST.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Cart = carts[1],
                    ManagedTeam = team
                };
                var user3 = new AppUser
                {
                    FirstName = "Luigi",
                    LastName = "Gangsta",
                    UserName = "ItalianCowboy",
                    NormalizedUserName = "ITALIANCOWBODY",
                    Email = "admin@test.com",
                    NormalizedEmail = "ADMIN@TEST.COM",
                    DateJoined = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Cart = carts[2],
                };

                

                var userArr = new AppUser[3] { user1, user2, user3 };
                var userPasswordArr = new String[3] { "WhoaDude123!", "MotherRussia123!", "MeatBallRevolver123!" };
                var roles = new string[3] { "Standard", "Manager", "Admin" };

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

                // ------------------------------------------- ADDING PRODUCT CATAGORIES ------------------------------------------- //
                var c = new string[] { "T-Shirts", "Polos/Knits", "Sweatshirts/Fleece", "Caps", "Activewear", "Outerwear", 
                    "Woven/Dress Shirts", "Workwear/Medical/Scrubs", "Bags", "Acessories", "Ladies/Women", "Youth" };

                for (int i = 0; i < c.Length; i++)
                {
                    ProductCategory newCategory = new ProductCategory()
                    {
                        Name = c[i]
                    };
                    // Add to the DB
                    context.ProductCategories.Add(newCategory);

                    // Add to the local list 
                    productCategories.Add(newCategory);
                }
                await context.SaveChangesAsync();


                // ------------------------------------------- ADDING PRODUCT SORTS ------------------------------------------- //
                var sortNames = new string[] { "Lowest Price", "Highest Price" };
                
                ProductSort highestPriceSort = new ProductSort()
                {
                    SortName = sortNames[0]
                };
                ProductSort lowestPriceSort = new ProductSort()
                {
                    SortName = sortNames[1]
                };

                context.ProductSorts.Add(highestPriceSort);
                context.ProductSorts.Add(lowestPriceSort);
                await context.SaveChangesAsync();

                // ------------------------------------------- ADDING TEAM SORTS ------------------------------------------- //
                var teamSortNames = new string[] { "Oldest", "Newest", "Most Popular" };
                TeamSort oldestSort = new TeamSort()
                {
                    SortName = teamSortNames[0]
                };
                TeamSort newestSort = new TeamSort()
                {
                    SortName = teamSortNames[1]
                };
                TeamSort mostPopular = new TeamSort()
                {
                    SortName = teamSortNames[2]
                };

                context.TeamSorts.Add(oldestSort);
                context.TeamSorts.Add(newestSort);
                context.TeamSorts.Add(mostPopular);
                await context.SaveChangesAsync();

            }
        }
    }
}
