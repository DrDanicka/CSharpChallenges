using CSharpChallenge.Models;
using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    public class UsersDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CSharpChallenges;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //string connectionString = @"Data Source=charpchallenges.database.windows.net;Initial Catalog=csharpchallenges;User ID=drdanicka;Password=csharpchallenges1+;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public bool FindUserByUserNameAndPassword(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username AND password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        public bool FindUserByUserName(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        public bool FindUserByUserNameOrEmail(UserModel user)
        {
            bool userExists = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username OR email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 80).Value = user.Email;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        userExists = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return userExists;
        }

        public void CreateUser(UserModel user)
        {
            string sqlStatement = "INSERT dbo.Users (username, password, email, admin) VALUES (@username, @password, @email, 0)";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public UserModel GetUserByName(string username)
        {
            UserModel userModel = null;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
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
            return userModel;
        }
    }
}
