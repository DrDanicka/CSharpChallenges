using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    /// <summary>
    /// Data Access Object (DAO) for operations related to user and problem associations.
    /// </summary>
    public class UserProblemDAO
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProblemDAO"/> class with the provided database connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the database.</param>
        public UserProblemDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Checks if a specific problem has been marked as done by a user.
        /// </summary>
        /// <param name="problemID">The ID of the problem to check.</param>
        /// <param name="userID">The ID of the user to check.</param>
        /// <returns>True if the user has solved the problem, false otherwise.</returns>
        public bool IsProblemDone(int problemID, int userID)
        {
            bool done = false;

            string sqlStatement = "SELECT * FROM dbo.UserProblem WHERE userid = @userid AND problemid = @problemid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                // Adding parameters to the SQL command to prevent SQL injection
                cmd.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@problemid", System.Data.SqlDbType.Int).Value = problemID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // If there are rows in the result, the problem is marked as done by the user
                    if (reader.HasRows)
                    {
                        done = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); // Exception handling for database connection or query errors
                }
            }
            return done;
        }

        /// <summary>
        /// Marks a problem as completed for a specific user by inserting a record into the UserProblem table.
        /// </summary>
        /// <param name="problemID">The ID of the problem that is being marked as completed.</param>
        /// <param name="userID">The ID of the user for whom the problem is being marked as completed.</param>
        /// <remarks>
        /// This method inserts a new record into the UserProblem table with the specified userID and problemID.
        /// If the operation is successful, it indicates that the user has completed the problem.
        /// If an exception occurs during the execution of the SQL command, the exception message is written to the console.
        /// </remarks>
        public void SetProblemAsDoneForUser(int problemID, int userID)
        {
            string sqlStatement = @"INSERT dbo.UserProblem (
                                    userid, 
                                    problemid) 
                            VALUES (
                                    @userid, 
                                    @problemid)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@problemid", System.Data.SqlDbType.Int).Value = problemID;

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
    }
}
