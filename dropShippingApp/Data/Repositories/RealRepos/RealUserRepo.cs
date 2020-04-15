using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealUserRepo : IUserRepo
    {
        private UserManager<AppUser> userManager;
        public RealUserRepo(UserManager<AppUser> usrMgr)
        {
            this.userManager = usrMgr;
        }

        public List<AppUser> GetAllUsersAndData()
        {
            var userList = userManager.Users
                .Include(user => user.CreationRequestHistory) // include request country
                    .ThenInclude(request => request.Country)
                .Include(user => user.CreationRequestHistory) // include request state
                    .ThenInclude(request => request.Providence)
                    
                .Include(user => user.UserOrderHistory)

                .Include(user => user.ManagedTeam) // get team products and base roster product
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                .Include(user => user.ManagedTeam) // get base roster product color
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(user => user.ManagedTeam) // get base roster product size
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(user => user.ManagedTeam) // get base roster product tags
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(user => user.ManagedTeam) // get base product pricing history
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(user => user.ManagedTeam) // get team product tags
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.ProductTags)
                .Include(user => user.ManagedTeam) // get team tags
                    .ThenInclude(team => team.TeamTags)
                .Include(user => user.ManagedTeam) // get team country
                    .ThenInclude(team => team.Country)
                .Include(user => user.ManagedTeam) // get team provence
                    .ThenInclude(team => team.Providence)

                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.ProductTags)

                .Include(user => user.AskedQuestions)

                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.ProductTags)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.PricingHistory)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.PricingHistory)

                .Include(user => user.AnsweredQuestions)
                    .ThenInclude(answer => answer.ParentMessage)

                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.BaseSize)
                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.BaseColor)
                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.ProductTags)
                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.PricingHistory)

                .Include(user => user.ApprovedTeamRequests)
                    .ThenInclude(request => request.Country)
                .Include(user => user.ApprovedTeamRequests)
                    .ThenInclude(request => request.Providence)

                .Include(user => user.ApprovedOrders)

                .Include(user => user.ActivityLog)
                .ToList();
            return userList;
        }

        public async Task<AppUser> GetUserDataAsync(ClaimsPrincipal httpUser)
        {
            // get user async to extract id
            var foundUser = await userManager.GetUserAsync(httpUser);
            return userManager.Users
                .Include(user => user.CreationRequestHistory) // include request country
                    .ThenInclude(request => request.Country)
                .Include(user => user.CreationRequestHistory) // include request state
                    .ThenInclude(request => request.Providence)

                .Include(user => user.UserOrderHistory)

                .Include(user => user.ManagedTeam) // get team products and base roster product
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                .Include(user => user.ManagedTeam) // get base roster product color
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(user => user.ManagedTeam) // get base roster product size
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(user => user.ManagedTeam) // get base roster product tags
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(user => user.ManagedTeam) // get base product pricing history
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.BaseProduct)
                            .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(user => user.ManagedTeam) // get team product tags
                    .ThenInclude(team => team.TeamProducts)
                        .ThenInclude(product => product.ProductTags)
                .Include(user => user.ManagedTeam) // get team tags
                    .ThenInclude(team => team.TeamTags)
                .Include(user => user.ManagedTeam) // get team country
                    .ThenInclude(team => team.Country)
                .Include(user => user.ManagedTeam) // get team provence
                    .ThenInclude(team => team.Providence)

                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.BaseProduct)
                        .ThenInclude(baseProduct => baseProduct.PricingHistory)
                .Include(user => user.CreatedCustomProducts)
                    .ThenInclude(product => product.ProductTags)

                .Include(user => user.AskedQuestions)

                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.ProductTags)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.PricingHistory)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseSize)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.BaseColor)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.ProductTags)
                .Include(user => user.Cart)
                    .ThenInclude(cart => cart.CartItems)
                        .ThenInclude(cartItem => cartItem.ProductSelection)
                            .ThenInclude(selectedProduct => selectedProduct.BaseProduct)
                                .ThenInclude(baseProduct => baseProduct.PricingHistory)

                .Include(user => user.AnsweredQuestions)
                    .ThenInclude(answer => answer.ParentMessage)

                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.BaseSize)
                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.BaseColor)
                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.ProductTags)
                .Include(user => user.RosterProducts)
                    .ThenInclude(product => product.PricingHistory)

                .Include(user => user.ApprovedTeamRequests)
                    .ThenInclude(request => request.Country)
                .Include(user => user.ApprovedTeamRequests)
                    .ThenInclude(request => request.Providence)

                .Include(user => user.ApprovedOrders)

                .Include(user => user.ActivityLog)

                .ToList().Find(user => user.Id == foundUser.Id);
        }
    }
}
