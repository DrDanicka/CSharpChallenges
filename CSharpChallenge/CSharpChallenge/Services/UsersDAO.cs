using CSharpChallenge.Models;
using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    /// <summary>
    /// Data Access Object (DAO) for user-related operations.
    /// </summary>
    public class UsersDAO
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersDAO"/> class with the provided configuration.
        /// </summary>
        /// <param name="configuration">The configuration containing the database connection string.</param>
        public UsersDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalConnection")!;
        }

        /// <summary>
        /// Checks if a user with the given username and password exists in the database.
        /// </summary>
        /// <param name="user">The user model containing the username and password to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public bool FindUserByUserNameAndPassword(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username AND password = @password";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            success = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        /// <summary>
        /// Checks if a user with the given username exists in the database.
        /// </summary>
        /// <param name="user">The user model containing the username to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public bool FindUserByUserName(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            success = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        /// <summary>
        /// Checks if a user with the given username or email exists in the database.
        /// </summary>
        /// <param name="user">The user model containing the username and email to check.</param>
        /// <returns>True if a user with the given username or email exists, false otherwise.</returns>
        public bool FindUserByUserNameOrEmail(UserModel user)
        {
            bool userExists = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username OR email = @email";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 80).Value = user.Email;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            userExists = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return userExists;
        }

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">The user model containing the user details to be created.</param>
        public void CreateUser(UserModel user)
        {
            string sqlStatement = "INSERT dbo.Users (username, password, email, admin) VALUES (@username, @password, @email, 0)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 80).Value = user.Email;

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
        /// Retrieves a user model by username from the database.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user model if found, null otherwise.</returns>
        public UserModel GetUserByName(string username)
        {
            UserModel? userModel = null;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = username;

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userModel = new UserModel
                        {
                            UserID = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            Email = reader.GetString(3),
                            Admin = reader.GetBoolean(4)
                        };
                    }
                }
            }
            return userModel!;
        }
    }
}
