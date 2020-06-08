﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private IEmailSender emailSender;
        public LoginController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                ICartRepo cartRepo,
                IEmailSender emailSender)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
            this.cartRepo = cartRepo;
            this.emailSender = new EmailSender(emailConfig);
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

        [HttpPost]
        public async Task<IActionResult> Signup(CreateUserViewModel model)
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
        public async Task<IActionResult> FgtPwdr()
        {
            return View();
        }
        public async Task<IActionResult> FgtPwd(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null || (await userManager.IsEmailConfirmedAsync(user)))
                {             // Don't reveal that the user does not exist or is not confirmed             return View("ForgotPasswordConfirmation");         } 
                    
                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    var message = new Message(new string[] { model.Email }, "Test email async", "This is the content from our async email.", null);
                    await emailSender.SendEmailAsync(message);
                    //  var callbackUrl = Url.Action("ResetPassword", "Account", new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //  await userManager.SetEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                    return View(message);
                }

                // If we got this far
            }

                    return View("no good");
        }

        [HttpGet]
        public async Task<IActionResult> SignoutUser()
        {
            var currentUser = userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}