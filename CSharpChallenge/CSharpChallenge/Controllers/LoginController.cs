using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSharpChallenge.Controllers
{
    /// <summary>
    /// Controller responsible for handling user login and logout.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly UsersDAO _usersDAO;
        private readonly SecurityService _securityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="usersDAO">Data access object for user-related operations.</param>
        /// <param name="securityService">Service responsible for security operations.</param>
        public LoginController(UsersDAO usersDAO, SecurityService securityService)
        {
            _usersDAO = usersDAO;
            _securityService = securityService;
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the user login process.
        /// </summary>
        /// <param name="user">The user model containing login details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (_securityService.IsValidLogin(user))
            {
                var loggedUser = _usersDAO.GetUserByName(user.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loggedUser.UserName),
                    new Claim(ClaimTypes.Role, loggedUser.Admin ? "Admin" : "User") // Add role claim based on user.Admin
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                // Redirects to Problem list on home 
                return RedirectToAction("Index", "Home");
            }

            if (_securityService.DoesUserNameExist(user))
            {
                // If the username exists => password had to be incorrect
                ModelState.AddModelError("Password", "Password is incorrect.");
            }
            else
            {
                ModelState.AddModelError("UserName", "Username does not exist.");
            }

            return View("Index", user);
        }

        /// <summary>
        /// Handles the user logout process.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Redirects to the login page
            return RedirectToAction("Index", "Login");
        }
    }
}
