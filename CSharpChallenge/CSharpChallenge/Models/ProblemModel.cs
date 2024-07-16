using System.ComponentModel.DataAnnotations;

namespace CSharpChallenge.Models
{
    public class ProblemModel
    {
        [Required]
        public int ProblemID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Difficulty { get; set; }
        public string ExampleTestCase { get; set; }
        public string ExampleTestCaseSolution { get; set; }
    }
}
