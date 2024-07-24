using CSharpChallenge.Models;
using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    /// <summary>
    /// Data Access Object (DAO) for problem-related operations.
    /// </summary>
    public class ProblemDAO
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDAO"/> class with the provided configuration.
        /// </summary>
        /// <param name="configuration">The configuration containing the database connection string.</param>
        public ProblemDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalConnection")!;
        }

        /// <summary>
        /// Retrieves all problems from the database.
        /// </summary>
        /// <returns>A list of <see cref="ProblemModel"/> objects.</returns>
        public List<ProblemModel> GetAllProblems()
        {
            List<ProblemModel> problems = new List<ProblemModel>();
            string sqlStatement = "SELECT * FROM dbo.Problems";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                            ExampleTestCase = reader.GetString(4),
                            ExampleTestCaseSolution = reader.GetString(5),
                            TestCases = reader.GetString(6),
                            TestCasesSolution = reader.GetString(7)
                        };
                        problems.Add(problem);
                    }
                }
            }
            return problems;
        }

        /// <summary>
        /// Retrieves all problems for a specific user, including their completion status.
        /// </summary>
        /// <param name="user">The user for whom to retrieve problems.</param>
        /// <returns>A list of <see cref="ProblemModelWithCheckmark"/> objects.</returns>
        public List<ProblemModelWithCheckmark> GetAllProblems(UserModel user)
        {
            List<ProblemModelWithCheckmark> problems = new List<ProblemModelWithCheckmark>();
            UserProblemDAO userProblemDAO = new UserProblemDAO(_connectionString);
            string sqlStatement = "SELECT * FROM dbo.Problems";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProblemModelWithCheckmark problem = new ProblemModelWithCheckmark
                        {
                            ProblemID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Difficulty = reader.GetString(3),
                            ExampleTestCase = reader.GetString(4),
                            ExampleTestCaseSolution = reader.GetString(5),
                            Done = userProblemDAO.IsProblemDone(reader.GetInt32(0), user.UserID)
                        };
                        problems.Add(problem);
                    }
                }
            }
            return problems;
        }

        /// <summary>
        /// Retrieves a problem by its ID from the database.
        /// </summary>
        /// <param name="problemID">The ID of the problem to retrieve.</param>
        /// <returns>A <see cref="ProblemModel"/> object if found, null otherwise.</returns>
        public ProblemModel GetProblemByID(int problemID)
        {
            ProblemModel? problem = null;
            string sqlStatement = "SELECT * FROM dbo.Problems WHERE problemid = @problemid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                            ExampleTestCase = reader.GetString(4),
                            ExampleTestCaseSolution = reader.GetString(5),
                            TestCases = reader.GetString(6),
                            TestCasesSolution = reader.GetString(7)
                        };
                    }
                }
            }
            return problem!;
        }

        /// <summary>
        /// Creates a new problem in the database.
        /// </summary>
        /// <param name="problem">The problem model containing the details to be created.</param>
        public void CreateProblem(ProblemModel problem)
        {
            string sqlStatement = @"INSERT dbo.Problems (
                                        title, 
                                        description, 
                                        difficulty, 
                                        exampletestcase,
                                        exampletestcasesolution,
                                        testcases,
                                        testcasessolution
                                ) 
                                VALUES (
                                        @title, 
                                        @description, 
                                        @difficulty, 
                                        @exampletestcase, 
                                        @exampletestcasesolution,
                                        @testcases,
                                        @testcasessolution)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 100).Value = problem.Title;
                cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 1000).Value = problem.Description;
                cmd.Parameters.Add("@difficulty", System.Data.SqlDbType.VarChar, 10).Value = problem.Difficulty;
                cmd.Parameters.Add("@exampletestcase", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCase;
                cmd.Parameters.Add("@exampletestcasesolution", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCaseSolution;
                cmd.Parameters.Add("@testcases", System.Data.SqlDbType.VarChar, 1000).Value = problem.TestCases;
                cmd.Parameters.Add("@testcasessolution", System.Data.SqlDbType.VarChar, 1000).Value = problem.TestCasesSolution;
                
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

        /// <summary>
        /// Updates an existing problem in the database.
        /// </summary>
        /// <param name="problem">The problem model containing the updated details.</param>
        public void UpdateProblem(ProblemModel problem)
        {
            string sqlStatement = @"
                UPDATE dbo.Problems
                SET Title = @title,
                    Description = @description,
                    Difficulty = @difficulty,
                    ExampleTestCase = @exampletestcase,
                    ExampleTestCaseSolution = @exampletestcasesolution,
                    TestCases = @testcases,
                    TestCasesSolution = @testcasessolution
                WHERE ProblemID = @problemId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@problemId", System.Data.SqlDbType.Int).Value = problem.ProblemID;
                cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 100).Value = problem.Title;
                cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 1000).Value = problem.Description;
                cmd.Parameters.Add("@difficulty", System.Data.SqlDbType.VarChar, 10).Value = problem.Difficulty;
                cmd.Parameters.Add("@exampletestcase", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCase;
                cmd.Parameters.Add("@exampletestcasesolution", System.Data.SqlDbType.VarChar, 100).Value = problem.ExampleTestCaseSolution;
                cmd.Parameters.Add("@testcases", System.Data.SqlDbType.VarChar, 1000).Value = problem.TestCases;
                cmd.Parameters.Add("@testcasessolution", System.Data.SqlDbType.VarChar, 1000).Value = problem.TestCasesSolution;

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

        /// <summary>
        /// Deletes a problem by its ID from the database.
        /// </summary>
        /// <param name="problemID">The ID of the problem to delete.</param>
        public void DeleteProblemByID(int problemID)
        {
            string sqlStatement = "DELETE FROM dbo.Problems WHERE ProblemID = @problemID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@problemID", System.Data.SqlDbType.Int).Value = problemID;

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

        /// <summary>
        /// Retrieves the test cases and their solutions for a given problem ID.
        /// </summary>
        /// <param name="problemID">The ID of the problem to retrieve test cases for.</param>
        /// <returns>
        /// A tuple containing two strings:
        /// Item1: The test cases for the problem.
        /// Item2: The solutions for the test cases.
        /// </returns>
        public Tuple<string, string> GetTestCasesByID(int problemID)
        {
            string TestCases = string.Empty;
            string TestCasesSolution = string.Empty;
            string sqlStatement = "SELECT * FROM dbo.Problems WHERE problemid = @problemid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@problemid", System.Data.SqlDbType.Int).Value = problemID;

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        TestCases = reader.GetString(6);
                        TestCasesSolution = reader.GetString(7);
                    }
                }
            }

            return new Tuple<string, string>(TestCases, TestCasesSolution);
        }

        /// <summary>
        /// Checks if a problem has been completed by a specific user.
        /// </summary>
        /// <param name="problemID">The ID of the problem to check.</param>
        /// <param name="userID">The ID of the user to check.</param>
        /// <returns>True if the problem is completed by the user; otherwise, false.</returns>
        public bool IsProblemDoneByUser(int problemID, int userID)
        {
            UserProblemDAO userProblemDAO = new UserProblemDAO(_connectionString);
            return userProblemDAO.IsProblemDone(problemID, userID);
        }

        /// <summary>
        /// Marks a problem as completed for a specific user.
        /// </summary>
        /// <param name="problemID">The ID of the problem to mark as completed.</param>
        /// <param name="userID">The ID of the user for whom to mark the problem as completed.</param>
        public void SetProblemAsDoneForUser(int problemID, int userID)
        {
            UserProblemDAO userProblemDAO = new UserProblemDAO(_connectionString);
            userProblemDAO.SetProblemAsDoneForUser(problemID, userID);
        }
    }
}
