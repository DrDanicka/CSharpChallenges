using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.Data.SqlClient;

namespace CSharpChallenge.Services
{
    public class UserProblemDAO
    {
        public bool IsProblemDone(int problemID, int userID)
        {
            bool done = false;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CSharpChallenges;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            string sqlStatement = "SELECT * FROM dbo.UserProblem WHERE userid = @userid AND problemid = @problemid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@problemid", System.Data.SqlDbType.Int).Value = problemID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        done = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return done;
        }
    }
}
