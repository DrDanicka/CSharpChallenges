namespace CSharpChallenge.Models
{
    /// <summary>
    /// Represents the relationship between a user and a problem, indicating that a specific user has solved a specific problem.
    /// </summary>
    public class UserProblemModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user-problem relationship.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the problem.
        /// </summary>
        public int ProblemID { get; set; }
    }
}
