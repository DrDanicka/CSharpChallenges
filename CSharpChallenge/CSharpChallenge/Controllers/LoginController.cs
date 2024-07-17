﻿using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSharpChallenge.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValidLogin(user))
            {
                UsersDAO usersDAO = new UsersDAO();
                var LoggedUser = usersDAO.GetUserByName(user.UserName);

                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, LoggedUser.UserName) ,
                    new Claim(ClaimTypes.Role, LoggedUser.Admin ? "Admin" : "User") // Add role claim based on user.Admin
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else if (securityService.DoesUserNameExist(user))
            {
                ModelState.AddModelError("Password", "Password is incorrect.");
            }
            else
            {
                ModelState.AddModelError("UserName", "Username does not exist.");
            }

            return View("Index", user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
