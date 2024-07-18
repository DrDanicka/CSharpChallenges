using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSharpChallenge.Controllers
{
    /// <summary>
    /// Controller responsible for handling user registration.
    /// </summary>
    public class RegisterController : Controller
    {
        private readonly UsersDAO _usersDAO;
        private readonly SecurityService _securityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterController"/> class.
        /// </summary>
        /// <param name="usersDAO">Data access object for user-related operations.</param>
        /// <param name="securityService">Service responsible for security operations.</param>
        public RegisterController(UsersDAO usersDAO, SecurityService securityService)
        {
            _usersDAO = usersDAO;
            _securityService = securityService;
        }

        /// <summary>
        /// Displays the registration page.
        /// </summary>
        /// <returns>The registration view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the user registration process.
        /// </summary>
        /// <param name="user">The user model containing registration details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (_securityService.AreValidRegistrationDetails(user))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                // Writes user information to the databse
                _usersDAO.CreateUser(user);

                // Redirects to Problem list on home 
                return RedirectToAction("Index", "Home");
            }
            
            if (_securityService.DoesUserNameExist(user))
            {
                ModelState.AddModelError("UserName", "Username already exists.");
            }
            else
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }

            // Returns Register View with error information added to Model
            return View("Index", user);
        }
    }
}
