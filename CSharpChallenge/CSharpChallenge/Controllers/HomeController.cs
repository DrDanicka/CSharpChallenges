using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CSharpChallenge.Models;
using CSharpChallenge.Services;
using Markdig;

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
                
                if (user.Admin)
                {
                    return View("AdminProblemList", ProblemsWithCheckmarsk);
                }

                return View("LoggedInProblemList", ProblemsWithCheckmarsk);
            }
            else
            {
                var Problems = problemDAO.GetAllProblems();
                return View(Problems);
            }
        }

        public IActionResult AdminProblemList()
        {
            ProblemDAO problemDAO = new ProblemDAO();
            UsersDAO usersDAO = new UsersDAO();

            var username = User.Identity.Name;
            UserModel user = usersDAO.GetUserByName(username);

            var ProblemsWithCheckmarsk = problemDAO.GetAllProblems(user);

            return View("AdminProblemList", ProblemsWithCheckmarsk);
        }

        public IActionResult Details(int id)
        {
            ProblemDAO problemDAO = new ProblemDAO();
            ProblemModel problem = problemDAO.GetProblemByID(id);

            // Set Description and Test cases to Markdown
            problem.Description = Markdig.Markdown.ToHtml(problem.Description);
            problem.ExampleTestCase = Markdig.Markdown.ToHtml(problem.ExampleTestCase);
            problem.ExampleTestCaseSolution = Markdig.Markdown.ToHtml(problem.ExampleTestCaseSolution);

            return View("Details", problem);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult CreateProblem(ProblemModel problemModel)
        {
            ProblemDAO problemDAO = new ProblemDAO();
            problemDAO.CreateProblem(problemModel);

            return RedirectToAction("AdminProblemList");
        }

        public IActionResult Edit(int id) 
        {
            ProblemDAO problemDAO = new ProblemDAO();
            ProblemModel problem = problemDAO.GetProblemByID(id);

            return View(problem);
        }


        public IActionResult EditProblem(ProblemModel problemModel)
        {
            ProblemDAO problemDAO = new ProblemDAO();
            problemDAO.UpdateProblem(problemModel);

            return RedirectToAction("AdminProblemList");
        }

        public IActionResult Delete(int id)
        {
            ProblemDAO problemDAO = new ProblemDAO();
            problemDAO.DeleteProblemByID(id);

            return RedirectToAction("AdminProblemList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
