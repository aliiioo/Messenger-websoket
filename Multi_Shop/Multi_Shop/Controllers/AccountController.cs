using Core.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Multi_Shop.Context.Entites;
using Multi_Shop.Models;
using Multi_Shop.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Multi_Shop.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageSender _messageSender;
        private readonly IShopservice _shopservice;

        public AccountController(UserManager<IdentityUser> user, SignInManager<IdentityUser> si, IMessageSender messageSender, IShopservice shopservice)
        {
            _shopservice = shopservice;
            _messageSender = messageSender;
            _userManager = user;
            _signInManager = si;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = vm.Email,
                    UserName = vm.Email,
                    PhoneNumber = vm.Phone,
                    EmailConfirmed = true
                };
                var Result = await _userManager.CreateAsync(user, vm.Password);
                UserList Use = new UserList()
                {
                    UserId = _userManager.GetUserId(User),
                    UserName = user.UserName
                };
                _shopservice.AddCoustemUser(Use);
                return RedirectToAction("Login", "Account");
                //if (Result.Succeeded)
                //{
                //    var emailConfirmationToken =
                //       await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var emailMessage =
                //        Url.Action("ConfirmEmail", "Account",
                //            new { username = user.UserName, token = emailConfirmationToken },
                //            Request.Scheme);
                //    await _messageSender.SendEmailAsync(user.Email, "Email confirmation", emailMessage);

                //    return RedirectToAction("Login", "Account");
                //}
                //else
                //{
                //    foreach (var err in Result.Errors)
                //    {
                //        ModelState.AddModelError(String.Empty, err.Description);
                //    }
                //}
            }
            return View(vm);
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = new LoginVm()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            ViewData["returnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {

                        return RedirectToAction(nameof(HomeController.Index), "Home");

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attemp");
            }



            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return NotFound();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return Content(result.Succeeded ? "Email Confirmed" : "Email Not Confirmed");
        }



        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Account",
                new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remoteError = null)
        {
            returnUrl =
                (returnUrl != null && Url.IsLocalUrl(returnUrl)) ? returnUrl : Url.Content("~/");

            var loginViewModel = new LoginVm()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error : {remoteError}");
                return View("Login", loginViewModel);
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                ModelState.AddModelError("ErrorLoadingExternalLoginInfo", $"مشکلی پیش آمد");
                return View("Login", loginViewModel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, false, true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var userName = email.Split('@')[0];
                    user = new IdentityUser()
                    {
                        UserName = (userName.Length <= 10 ? userName : userName.Substring(0, 10)),
                        Email = email,
                        EmailConfirmed = true
                    };

                    await _userManager.CreateAsync(user);
                }

                await _userManager.AddLoginAsync(user, externalLoginInfo);
                await _signInManager.SignInAsync(user, false);

                return Redirect(returnUrl);
            }

            ViewBag.ErrorTitle = "لطفا با بخش پشتیبانی تماس بگیرید";
            ViewBag.ErrorMessage = $"دریافت کرد {externalLoginInfo.LoginProvider} نمیتوان اطلاعاتی از";
            return View();
        }



        public IActionResult UserPanel(string UserName)
        {
            var Users = _userManager.Users.SingleOrDefault(c => c.UserName == UserName);
            
            var user = new UserVMForAdmin()
            {
                Email = Users.Email,
                Id = Users.Id,
                Mobile = Users.PhoneNumber,
                Picture = Users.PhoneNumberConfirmed
            };
            return View(user);
        }

        public async Task<IActionResult> ChangePhoto(IFormFile img, string email)
        {
            if (img != null)
            {
                var palce = email + Path.GetExtension(img.FileName);
                string Deletimg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users", palce);
                if (System.IO.File.Exists(Deletimg))
                {
                    System.IO.File.Delete(Deletimg);
                }
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users", palce);
                using (var Stream = new FileStream(imgPath, FileMode.Create))
                {
                    img.CopyTo(Stream);
                }
            }
            IdentityUser user = _userManager.Users.SingleOrDefault(c => c.Email == email);
            user.PhoneNumberConfirmed = true;
            var username = _userManager.Users.SingleOrDefault(c => c.Email == email);
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(UserPanel), new { UserName = username.UserName });
        }

        




    }

}
