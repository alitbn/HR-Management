﻿using HR_Management.MVC.Contracts;
using HR_Management.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.MVC.Controllers
{
    public class UsersController : Controller
    {
        private IAuthenticateService _authenticateService;

        public UsersController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                
                return View(register);
            }

            var isCreated = await _authenticateService.Register(register);
            if (isCreated)
            {
                return LocalRedirect("/");
            }
            ModelState.AddModelError("", "Registration Failed.");
            return View(register);
        }
        #endregion

        #region Login

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticateService.Authenticate(login.Email, login.Passsword);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError("","Login Failed. Please Try again.");
            return View(login);
        }

        #endregion

        #region Logout
        [HttpPost]
        public async Task<IActionResult > Logout()
        {
            await _authenticateService.Logout();
            return LocalRedirect("/Users/Login");
        }

        #endregion
    }
}
