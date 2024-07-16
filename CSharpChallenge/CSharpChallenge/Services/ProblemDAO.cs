using CSharpChallenge.Models;
using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    public class ProblemDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CSharpChallenges;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<ProblemModel> GetAllProblems()
        {
            List<ProblemModel> problems = new List<ProblemModel>();

            string sqlStatement = "SELECT * FROM dbo.Problems";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProblemModel problem = new ProblemModel
                        {
                            ProblemID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Difficulty = reader.GetString(3),
                            ExampleTestCase = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ExampleTestCaseSolution = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                        problems.Add(problem);
                    }
                }
            }
            return problems;
        }

        public List<ProblemModelWIthCheckmark> GetAllProblems(UserModel user)
        {
            List<ProblemModelWIthCheckmark> problems = new List<ProblemModelWIthCheckmark>();
            UserProblemDAO userProblemDAO = new UserProblemDAO();

            string sqlStatement = "SELECT * FROM dbo.Problems";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProblemModelWIthCheckmark problem = new ProblemModelWIthCheckmark
                        {
                            ProblemID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Difficulty = reader.GetString(3),
                            ExampleTestCase = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ExampleTestCaseSolution = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Done = false
                        };
                        if (userProblemDAO.IsProblemDone(problem.ProblemID, user.UserID))
                        {
                            problem.Done = true;
                        }
                        
                        problems.Add(problem);
                    }
                }
            }
            return problems;
        }
    }
}
