using CSharpChallenge.Models;
using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    public class ProblemDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CSharpChallenges;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //string connectionString = @"Data Source=charpchallenges.database.windows.net;Initial Catalog=csharpchallenges;User ID=drdanicka;Password=csharpchallenges1+;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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

        public ProblemModel GetProblemByID(int problemID)
        {
            ProblemModel problem = null;
            string sqlStatement = "SELECT * FROM dbo.Problems WHERE problemid = @problemid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@problemid", System.Data.SqlDbType.Int).Value = problemID;

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        problem = new ProblemModel()
                        {
                            ProblemID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Difficulty = reader.GetString(3),
                            ExampleTestCase = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ExampleTestCaseSolution = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                    }
                }
            }
            return problem;
        }

        public void CreateProblem(ProblemModel problem)
        {
            string sqlStatement = @"INSERT dbo.Problems (
                                        title, 
                                        description, 
                                        difficulty, 
                                        exampletestcase,
                                        exampletestcasesolution
                                ) 
                                VALUES (
                                        @title, 
                                        @description, 
                                        @difficulty, 
                                        @exampletestcase, 
                                        @exampletestcasesolution)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 100).Value = problem.Title;
                cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 1000).Value = problem.Description;
                cmd.Parameters.Add("@difficulty", System.Data.SqlDbType.VarChar, 10).Value = problem.Difficulty;
                cmd.Parameters.Add("@exampletestcase", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCase;
                cmd.Parameters.Add("@exampletestcasesolution", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCaseSolution;


                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void UpdateProblem(ProblemModel problem)
        { 
            string sqlStatement = @"
                UPDATE dbo.Problems
                SET Title = @title,
                    Description = @description,
                    Difficulty = @difficulty,
                    ExampleTestCase = @exampletestcase,
                    ExampleTestCaseSolution = @exampletestcasesolution
                WHERE ProblemID = @problemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@problemId", System.Data.SqlDbType.Int).Value = problem.ProblemID;
                cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 100).Value = problem.Title;
                cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 1000).Value = problem.Description;
                cmd.Parameters.Add("@difficulty", System.Data.SqlDbType.VarChar, 10).Value = problem.Difficulty;
                cmd.Parameters.Add("@exampletestcase", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCase;
                cmd.Parameters.Add("@exampletestcasesolution", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCaseSolution;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void DeleteProblemByID(int problemID)
        {
            string sqlStatement = "DELETE FROM dbo.Problems WHERE ProblemID = @problemID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@problemID", System.Data.SqlDbType.Int).Value = problemID;

                try
                {
                    connection.OpenAsync();
                    cmd.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
