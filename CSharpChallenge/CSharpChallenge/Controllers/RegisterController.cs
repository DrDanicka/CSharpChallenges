using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpChallenge.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.AreValidRegistrationDetails(userModel))
            {
                UsersDAO usersDAO = new UsersDAO();
                usersDAO.CreateUser(userModel);
                return View("testPage", userModel);
            }
            else
            {
                return View("InvalidRegistration", userModel);
            }
        }
    }
}
