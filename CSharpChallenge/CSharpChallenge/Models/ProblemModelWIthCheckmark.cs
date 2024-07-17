using System.ComponentModel;

namespace CSharpChallenge.Models
{
    public class ProblemModelWIthCheckmark
    {
        public int ProblemID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string ExampleTestCase { get; set; }
        public string ExampleTestCaseSolution { get; set; }
        [DisplayName("Solved")]
        public bool Done { get; set; }
    }
}
