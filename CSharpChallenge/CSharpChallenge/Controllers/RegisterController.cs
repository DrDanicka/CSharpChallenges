using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSharpChallenge.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register(UserModel user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.AreValidRegistrationDetails(user))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                UsersDAO usersDAO = new UsersDAO();
                usersDAO.CreateUser(user);

                return RedirectToAction("Index", "Home");
            }
            else if(securityService.DoesUserNameExist(user))
            {
                ModelState.AddModelError("UserName", "Username already exists.");
            }
            else
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }
            return View("Index", user);
        }
    }
}
