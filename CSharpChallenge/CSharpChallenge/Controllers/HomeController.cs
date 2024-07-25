using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CSharpChallenge.Models;
using CSharpChallenge.Services;
using CSharpChallenge.Evaluator;

namespace CSharpChallenge.Controllers
{
    /// <summary>
    /// Controller responsible for handling home page and problem-related actions.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UsersDAO _usersDAO;
        private readonly ProblemDAO _problemDAO;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger for logging information.</param>
        /// <param name="usersDAO">Data access object for user-related operations.</param>
        /// <param name="problemDAO">Data access object for problem-related operations.</param>
        public HomeController(ILogger<HomeController> logger, UsersDAO usersDAO, ProblemDAO problemDAO)
        {
            _logger = logger;
            _usersDAO = usersDAO;
            _problemDAO = problemDAO;
        }

        /// <summary>
        /// Displays the index page where is list of problems.
        /// </summary>
        /// <returns>The appropriate view based on the user's authentication status and role.</returns>
        public IActionResult Index()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var username = User.Identity.Name;

                UserModel user = _usersDAO.GetUserByName(username!);

                var problemsWithCheckmarks = _problemDAO.GetAllProblems(user);

                if (user.Admin)
                {
                    // If the user is admin then show Admin problem list with option to edit, create and delete problems
                    return View("AdminProblemList", problemsWithCheckmarks);
                }

                // Otherwise display just basic problem list with only ability to solve
                return View("LoggedInProblemList", problemsWithCheckmarks);
            }
            else
            {
                // If no user is logged in then display just problems without ability to solve
                var problems = _problemDAO.GetAllProblems();
                return View(problems);
            }
        }

        /// <summary>
        /// Displays the admin problem list.
        /// </summary>
        /// <returns>The admin problem list view.</returns>
        public IActionResult AdminProblemList()
        {
            var username = User.Identity!.Name;
            UserModel user = _usersDAO.GetUserByName(username!);

            var problemsWithCheckmarks = _problemDAO.GetAllProblems(user);

            return View("AdminProblemList", problemsWithCheckmarks);
        }

        /// <summary>
        /// Displays the details of a specific problem.
        /// </summary>
        /// <param name="problemID">The ID of the problem.</param>
        /// <returns>The problem details view.</returns>
        public IActionResult Details(int problemID)
        {
            ProblemModel problem = _problemDAO.GetProblemByID(problemID);

            // Set Description and Test cases to Markdown
            problem.Description = Markdig.Markdown.ToHtml(problem.Description);
            problem.ExampleTestCase = Markdig.Markdown.ToHtml(problem.ExampleTestCase);
            problem.ExampleTestCaseSolution = Markdig.Markdown.ToHtml(problem.ExampleTestCaseSolution);

            return View("Details", problem);
        }

        /// <summary>
        /// Displays the problem creation page. This is available only to the admin.
        /// </summary>
        /// <returns>The create problem view.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new problem.
        /// </summary>
        /// <param name="problemModel">The problem model containing the problem details.</param>
        /// <returns>A redirect to the admin problem list.</returns>
        public IActionResult CreateProblem(ProblemModel problemModel)
        {
            // Writes new problem to the database
            _problemDAO.CreateProblem(problemModel);

            return RedirectToAction("AdminProblemList");
        }

        /// <summary>
        /// Displays the edit problem page. This is available only to the admin.
        /// </summary>
        /// <param name="problemID">The ID of the problem to edit.</param>
        /// <returns>The edit problem view.</returns>
        public IActionResult Edit(int problemID)
        {
            ProblemModel problem = _problemDAO.GetProblemByID(problemID);

            return View(problem);
        }

        /// <summary>
        /// Handles the updating of a problem. This is available only to the admin.
        /// </summary>
        /// <param name="problemModel">The problem model containing the updated problem details.</param>
        /// <returns>A redirect to the admin problem list.</returns>
        public IActionResult EditProblem(ProblemModel problemModel)
        {
            // Updates problem in the database
            _problemDAO.UpdateProblem(problemModel);

            return RedirectToAction("AdminProblemList");
        }

        /// <summary>
        /// Handles the deletion of a problem. This is available only to the admin.
        /// </summary>
        /// <param name="problemID">The ID of the problem to delete.</param>
        /// <returns>A redirect to the admin problem list.</returns>
        public IActionResult Delete(int problemID)
        {
            // Deletes problem from database
            _problemDAO.DeleteProblemByID(problemID);

            return RedirectToAction("AdminProblemList");
        }

        /// <summary>
        /// Converts the Markdown content of the problem's description, example test case, 
        /// and example test case solution to HTML.
        /// </summary>
        /// <param name="problem">The problem model containing Markdown content.</param>
        /// <returns>The problem model with HTML content.</returns>
        private ProblemModel ConvertProblemToMarkdown(ProblemModel problem)
        {
            // Convert the problem's description from Markdown to HTML
            problem.Description = Markdig.Markdown.ToHtml(problem.Description);

            // Convert the example test case from Markdown to HTML
            problem.ExampleTestCase = Markdig.Markdown.ToHtml(problem.ExampleTestCase);

            // Convert the example test case solution from Markdown to HTML
            problem.ExampleTestCaseSolution = Markdig.Markdown.ToHtml(problem.ExampleTestCaseSolution);
    
            // Return the modified problem model
            return problem;
        }

        
        /// <summary>
        /// Handles the submission of a solution file for a specific problem.
        /// </summary>
        /// <param name="file">The submitted solution file.</param>
        /// <param name="problemId">The ID of the problem being solved.</param>
        /// <returns>The view displaying the problem details, along with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> SubmitSolution(IFormFile file, int problemId)
        {
            // Initialize the C# file validator and solution evaluator
            CSharpFileValidator csValidator = new CSharpFileValidator();
            SolutionEvaluator solutionEvaluator = new SolutionEvaluator(_problemDAO);
            
            // Define the path to the upload folder and retrieve the problem details using the problemID
            string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var problem = _problemDAO.GetProblemByID(problemId);
            
            // Check if the submitted file is valid and in .cs format
            if (!csValidator.IsValidCsFile(file))
            {
                // Add an error message to the ModelState if the file is invalid
                ModelState.AddModelError("", "Please upload a valid .cs file.");
                // Return the problem details view with the error message
                return View("Details", ConvertProblemToMarkdown(problem));
            }

            // Save the uploaded file to the server
            var filePath = Path.Combine(uploadFolderPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // Check if the uploaded file can be compiled
            if (!csValidator.IsFileAbleToCompile(filePath))
            {
                // Add an error message to the ModelState if the compilation failed
                ModelState.AddModelError("", "Compilation failed.");
                // Return the problem details view with the error message
                return View("Details", ConvertProblemToMarkdown(problem));
            }

            // Process the uploaded file and evaluate the solution
            Result result = solutionEvaluator.ProcessSolution(filePath, problemId);

            // Handle the result of the solution evaluation
            if (result.Success)
            {
                // Set a success message
                ViewBag.Message = "Solution submitted successfully :)";
                ViewData["Success"] = true;
                
                // Retrieve the logged-in user
                UserModel user = _usersDAO.GetUserByName(User.Identity!.Name!);

                // Mark the problem as done for the user if not already done
                if (!_problemDAO.IsProblemDoneByUser(problemId, user.UserID))
                {
                    _problemDAO.SetProblemAsDoneForUser(problemId, user.UserID);
                }
            }
            else
            {
                // Set an error message with the result message
                ViewBag.Message = result.Message!.Replace("\r\n", "\n").Replace("\n", "<br>").Replace("\t", "&emsp; ");
                ViewData["Success"] = false;
            }

            // Return the problem details view with the appropriate message
            return View("Details", ConvertProblemToMarkdown(problem));
        }

        
        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
