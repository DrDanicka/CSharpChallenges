using CSharpChallenge.Services;
using System.Collections;

namespace CSharpChallenge.Evaluator
{
    /// <summary>
    /// Represents a collection of test cases for a specific problem.
    /// </summary>
    public class TestCases : IEnumerable<TestCases.TestCase>
    {
        private readonly int _problemId;
        private List<string> _inputs;
        private List<string> _outputs;
        private readonly ProblemDAO _problemDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCases"/> class.
        /// </summary>
        /// <param name="problemDAO">The data access object for problems.</param>
        /// <param name="problemId">The ID of the problem for which to load test cases.</param>
        public TestCases(ProblemDAO problemDAO, int problemId)
        {
            _inputs = new List<string>();
            _outputs = new List<string>();
            _problemDAO = problemDAO;
            _problemId = problemId;
            LoadTestCasesFromDB();
        }

        /// <summary>
        /// Loads test cases from the database.
        /// </summary>
        private void LoadTestCasesFromDB()
        {
            Tuple<string, string> testCasesAndSolutions = _problemDAO.GetTestCasesByID(_problemId);
            
            ConvertStringToCases(testCasesAndSolutions.Item1, _inputs);
            ConvertStringToCases(testCasesAndSolutions.Item2, _outputs);
        }

        /// <summary>
        /// Converts a string of cases into a list of individual cases.
        /// </summary>
        /// <param name="stringCases">The string containing multiple cases separated by a delimiter.</param>
        /// <param name="casesSeparatedInList">The list to store the individual cases.</param>
        private void ConvertStringToCases(string stringCases, List<string> casesSeparatedInList)
        {
            // Split the content by the separator (-----)
            var splitCases = stringCases.Split(new string[] { "-----" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(section => section.Trim().Replace("\r", "")); // Trim whitespace

            // Append the resulting sections to the casesSeparatedInList
            casesSeparatedInList.AddRange(splitCases);
        }

        /// <summary>
        /// Represents a single test case.
        /// </summary>
        public struct TestCase
        {
            /// <summary>
            /// Gets the input for the test case.
            /// </summary>
            public string Input { get; }

            /// <summary>
            /// Gets the expected output for the test case.
            /// </summary>
            public string Output { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="TestCase"/> struct.
            /// </summary>
            /// <param name="input">The input for the test case.</param>
            /// <param name="output">The expected output for the test case.</param>
            public TestCase(string input, string output)
            {
                Input = input;
                Output = output;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of test cases.
        /// </summary>
        /// <returns>An enumerator for the collection of test cases.</returns>
        public IEnumerator<TestCase> GetEnumerator()
        {
            for (int i = 0; i < _inputs.Count; i++)
            {
                yield return new TestCase(_inputs[i], _outputs[i]);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
