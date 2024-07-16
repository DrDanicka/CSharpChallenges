using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CSharpChallenge.Models;
using CSharpChallenge.Services;

namespace CSharpChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ProblemDAO problemDAO = new ProblemDAO();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                UsersDAO usersDAO = new UsersDAO();
                var username = User.Identity.Name;

                UserModel user = usersDAO.GetUserByName(username);

                var ProblemsWithCheckmarsk = problemDAO.GetAllProblems(user);
                return View("LoggedInProblemList", ProblemsWithCheckmarsk);
            }
            else
            {
                var Problems = problemDAO.GetAllProblems();
                return View(Problems);
            }
        }

        public IActionResult Details(ProblemModel problemModel)
        {
            return View("Details", problemModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
