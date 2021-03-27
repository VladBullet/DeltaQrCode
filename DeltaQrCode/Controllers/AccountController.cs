using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQrCode.Controllers
{
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;

    using DeltaQrCode.HelpersAndExtensions;
    using DeltaQrCode.Models;
    using DeltaQrCode.Services;
    using DeltaQrCode.ViewModels;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _authService;
        public AccountController(IUserService dbContext)
        {
            _authService = dbContext;
        }
        private const string LoginViewPath = @"~/Views/Account/Login.cshtml";

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(LoginViewPath, new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            // test user vlad.bulete    P455w0rd 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (ModelState.IsValid)
            {
                var user = AuthenticateUser(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(LoginViewPath, model);
                }

                var claims = new List<Claim>
                                 {
                                     new Claim(ClaimTypes.Name, user.UserAccount),
                                     new Claim("Mail", user.UserEmailAddress),
                                     new Claim(ClaimTypes.Role, "admin")
                                 };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties());

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            // Something failed. Redisplay the form.
            return View(LoginViewPath, model);
        }
        private CaUsers AuthenticateUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var loggedInUser = _authService.FindUserByLoginAndPass(login, password.ToMd5());
            if (loggedInUser == null)
            {
                return null;
            }
            else
            {
                return new CaUsers(loggedInUser);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //[AllowAnonymous]
        //public IActionResult ForgotPass()
        //{
        //    return View("ForgotPassword");
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult ForgotPass(LoginViewModel model)
        //{

        //    return View("EmailConfirmation");
        //}

        //[AllowAnonymous]
        //public IActionResult AccessDenied()
        //{
        //    return View("AccessDenied");
        //}
    }
}
