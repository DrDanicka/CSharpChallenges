using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpChallenge.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValidLogin(userModel))
            {
                return View("ProblemsPage", userModel);
            }
            else if (securityService.DoesUserNameExist(userModel))
            {
                return View("IncorrectPasswordLogin", userModel);
            }
            else
            {
                return View("IncorrectUserNameLogin", userModel);
            }
        }
    }
}
