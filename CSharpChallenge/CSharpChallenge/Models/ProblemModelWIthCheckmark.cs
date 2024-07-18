using System.ComponentModel;

namespace CSharpChallenge.Models
{
    /// <summary>
    /// Represents a problem with an additional property indicating whether it has been solved by a user.
    /// </summary>
    public class ProblemModelWithCheckmark
    {
        /// <summary>
        /// Gets or sets the unique identifier for the problem.
        /// </summary>
        public int ProblemID { get; set; }

        /// <summary>
        /// Gets or sets the title of the problem.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the problem.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the difficulty level of the problem.
        /// </summary>
        public string Difficulty { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the example test case for the problem.
        /// </summary>
        public string ExampleTestCase { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the solution for the example test case.
        /// </summary>
        public string ExampleTestCaseSolution { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the problem has been solved by the user.
        /// </summary>
        [DisplayName("Solved")]
        public bool Done { get; set; }
    }
}
