using CSharpChallenge.Services;
using System.Diagnostics;

namespace CSharpChallenge.Evaluator
{
    /// <summary>
    /// Represents the result of processing a solution.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets a value indicating whether the processing was successful.
        /// </summary>
        public readonly bool Success;
        
        /// <summary>
        /// Gets the message associated with the result.
        /// </summary>
        public readonly string? Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success">A value indicating whether the processing was successful.</param>
        /// <param name="message">The message associated with the result.</param>
        public Result(bool success, string? message)
        {
            Success = success;
            Message = message;
        }
    }

    /// <summary>
    /// Evaluates solutions for problems by compiling and running the submitted code.
    /// </summary>
    public class SolutionEvaluator
    {
        private readonly ProblemDAO _problemDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionEvaluator"/> class.
        /// </summary>
        /// <param name="problemDAO">The data access object for problems.</param>
        public SolutionEvaluator(ProblemDAO problemDAO)
        {
            _problemDAO = problemDAO;
        }

        /// <summary>
        /// Processes the solution file for the specified problem.
        /// </summary>
        /// <param name="filePath">The path to the solution file.</param>
        /// <param name="problemId">The ID of the problem being solved.</param>
        /// <returns>A <see cref="Result"/> indicating the success or failure of the solution.</returns>
        public Result ProcessSolution(string filePath, int problemId)
        {
            TestCases testCases = new TestCases(_problemDAO, problemId);
            bool success = true;
            string? firstErrorMessage = null;

            // Run test cases in parallel
            testCases.AsParallel().ForAll(testCase =>
            {
                string? message = ExecuteCode(testCase.Input, testCase.Output);
                // If an error message is returned, update success flag and store the first error message
                if (message is not null)
                {
                    success = false;
                    if (firstErrorMessage is null)
                    {
                        firstErrorMessage = message;
                    }
                }
            });

            return new Result(success, firstErrorMessage);
        }

        /// <summary>
        /// Executes the compiled code with the provided input and compares the output with the expected output.
        /// </summary>
        /// <param name="input">The input for the program.</param>
        /// <param name="expectedOutput">The expected output of the program.</param>
        /// <returns>A string containing an error message if the execution fails or the output is incorrect; otherwise, null.</returns>
        private string? ExecuteCode(string input, string expectedOutput)
        {
            string projectDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/TempProject");

            // Run the compiled DLL using dotnet run
            string dllFilePath = Path.Combine(projectDir, "bin", "Release", "net8.0", "TempProject.dll");

            Process runProcess = new Process();
            runProcess.StartInfo.FileName = "dotnet";
            runProcess.StartInfo.Arguments = dllFilePath;
            runProcess.StartInfo.RedirectStandardInput = true;
            runProcess.StartInfo.RedirectStandardOutput = true;
            runProcess.StartInfo.RedirectStandardError = true;
            runProcess.StartInfo.UseShellExecute = false;

            runProcess.Start();

            // Pass input to the program
            using (StreamWriter writer = runProcess.StandardInput)
            {
                writer.WriteLine(input); // Simulate console input
            }

            // Read output from the program
            string output = runProcess.StandardOutput.ReadToEnd().Trim().Replace("\r", "");
            string errors = runProcess.StandardError.ReadToEnd();
            
            runProcess.WaitForExit();

            // If there was an error in program execution, return the error message
            if (!string.IsNullOrEmpty(errors))
            {
                return errors.Split("at")[0];
            }

            // If the output is incorrect, return a detailed error message
            if (expectedOutput != output)
            {
                return $@"Wrong Answer. In test:
                &emsp; {input.Replace("\r\n", "\n").Replace("\n", "\n\t")}
                Expected output:
                &emsp; {expectedOutput.Replace("\r\n", "\n").Replace("\n", "\n\t")}
                But got:
                &emsp; {output.Replace("\r\n", "\n").Replace("\n", "\n\t")}";
            }

            // If the output is correct, return null
            return null;
        }
    }
}
