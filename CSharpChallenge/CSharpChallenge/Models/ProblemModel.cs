using System.ComponentModel.DataAnnotations;

namespace CSharpChallenge.Models
{
    /// <summary>
    /// Represents a problem in the system.
    /// </summary>
    public class ProblemModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the problem.
        /// </summary>
        [Required]
        public int ProblemID { get; set; }

        /// <summary>
        /// Gets or sets the title of the problem.
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the problem.
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the difficulty level of the problem.
        /// </summary>
        [Required]
        public string Difficulty { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an example test case for the problem.
        /// </summary>
        [Required]
        public string ExampleTestCase { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the solution for the example test case.
        /// </summary>
        [Required]
        public string ExampleTestCaseSolution { get; set; } = string.Empty;
    }
}
