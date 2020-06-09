using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dropShippingApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private ICartRepo cartRepo;
        public LoginController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                ICartRepo cartRepo)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
            this.cartRepo = cartRepo;
        }
        public async Task<ViewResult> Index()
        {
            return View();
        } 

        public async Task<ViewResult> Signup()
        {
            return View();
        }

        public async Task<ViewResult> Signout()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        await signInManager.SignOutAsync();
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                        if (result.Succeeded)
                        {
                            return Redirect(returnUrl ?? "/Home/Index");
                        }
                    }
                    ModelState.AddModelError(nameof(LoginViewModel.Email), "Invalid email or password");
                }
                return View("Index", model);
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-0001",
                    Message = "An error occured while trying to login."
                };
                return View("Error", e);
            }
   ;
        }

      
        public async Task<IActionResult> ForgotPassword(LoginViewModel model)
        {
            return View();
        }

        public async Task<IActionResult> ForgotPasswordCtrl(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
          
            return View(model.Password);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(CreateUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ConfirmPassword == model.Password)
                    {
                        // ensuring unique username
                        var userSearchByName = await userManager.FindByNameAsync(model.Username);
                        var userSearchByEmail = await userManager.FindByEmailAsync(model.Email);
                        if (userSearchByName == null && userSearchByEmail == null)
                        {
                            // create user cart
                            // save cart
                            Cart newUserCart = new Cart();
                            await cartRepo.AddCart(newUserCart);

                            AppUser user = new AppUser
                            {
                                UserName = model.Username,
                                Email = model.Email,
                                FirstName = model.FName,
                                LastName = model.LName,
                                Cart = newUserCart
                            };
                            IdentityResult result
                                = await userManager.CreateAsync(user, model.Password);

                            if (result.Succeeded)
                            {
                                var newUser = await userManager.FindByNameAsync(user.UserName);
                                await userManager.AddToRoleAsync(user, "standard");
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                await cartRepo.RemoveCartById(newUserCart.CartID);
                                foreach (IdentityError error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(CreateUserViewModel.Username), "Username must be unique");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(CreateUserViewModel.ConfirmPassword), "Passwords must match");
                    }
                }
                return View(model);
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-008",
                    Message = "An error occured while getting signed up."
                };
                return View("Error", e);
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> SignoutUser()
        {
            try
            {
                var currentUser = userManager.GetUserAsync(HttpContext.User);
                if (currentUser != null)
                {
                    await signInManager.SignOutAsync();
                }
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch
            {
                ErrorViewModel e = new ErrorViewModel
                {
                    RequestId = "DKS-009",
                    Message = "An error occured while singing out."
                };
                return View("Error", e);
            }

        }
    }
}