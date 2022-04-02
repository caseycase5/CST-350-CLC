using CST_350_CLC.Models;
using System;
using System.Data.SqlClient;

namespace CST_350_CLC.Services {
    public class UsersDAO {
        // assume there are no connections
        bool success = false;

        // change to your local database connection. COPY the Connection string property of your database and paste below
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CLC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public bool FindUserByNameAndPassword(UserModel user) {
            string sqlStatement = "SELECT * FROM dbo.Users WHERE USERNAME = @Username AND PASSWORD = @password";

            //allows us to generate block of code that will use connection and terminate connection after block is finished
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                //issues command to db. 2 parameters: SQL statement you want to send and the connection being connected to
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try {
                    // initiate connection to db
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) {
                        success = true;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            // return true if found in db.
            return success;

        }

        public bool RegisterNewUser(UserModel user) {
            string sqlStatement = "INSERT INTO dbo.Users (FIRST_NAME, LAST_NAME, EMAIL, STATE, AGE, SEX, USERNAME, PASSWORD) VALUES (@firstName, @lastName, @email, @state, @age, @sex, @username, @password)";

            //allows us to generate block of code that will use connection and terminate connection after block is finished
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                //issues command to db. 2 parameters: SQL statement you want to send and the connection being connected to
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@firstName", System.Data.SqlDbType.NChar, 30).Value = user.FirstName;
                command.Parameters.Add("@lastName", System.Data.SqlDbType.NChar, 30).Value = user.LastName;
                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 50).Value = user.Email;
                command.Parameters.Add("@state", System.Data.SqlDbType.NChar, 20).Value = user.State;
                command.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = user.Age;
                command.Parameters.Add("@sex", System.Data.SqlDbType.NChar, 10).Value = user.Sex;
                command.Parameters.Add("@username", System.Data.SqlDbType.NChar, 30).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 50).Value = user.Password;

                try {
                    // initiate connection to db
                    connection.Open();
                    Console.WriteLine("About to execute query.");
                    Console.WriteLine(sqlStatement);
                    int k = command.ExecuteNonQuery();
                    if(k != 0) {
                        success = true;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
    }
}
