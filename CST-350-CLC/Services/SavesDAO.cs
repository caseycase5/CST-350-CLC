using CST_350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CST_350_CLC.Services
{
    public class SavesDAO : ISavesDataService
    {
        // change to your local database connection. COPY the Connection string property of your database and paste below
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CLC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Save Game method
        public int Insert(GameDataModel gameDataModel)
        {
            int newIdNumber = -1;

            string query = "INSERT INTO dbo.Saves (USER_ID, SAVE_DATE, GAME_DATA) VALUES (@USER_ID, @SAVE_DATE, @GAME_DATA)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@USER_ID", gameDataModel.UserId);
                command.Parameters.AddWithValue("@SAVE_DATE", gameDataModel.DateTime);
                command.Parameters.AddWithValue("@GAME_DATA", gameDataModel.GameData);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return newIdNumber;
        }

        // Get all saves method
        public GameDataModel GetGameById(int id)
        {
            GameDataModel gameDataModel = null;

            string sqlStatement = "SELECT * FROM dbo.Saves WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        gameDataModel = new GameDataModel((int)reader[0], (int)reader[1], (string)reader[2], (string)reader[3]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return gameDataModel;
        }

        public List<GameDataModel> GetGames()
        {
            List<GameDataModel> gameDataList = new List<GameDataModel>();
            string query = "SELECT * FROM dbo.Saves";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        gameDataList.Add(new GameDataModel((int)reader[0], (int)reader[1], (string)reader[2], (string)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return gameDataList;
        }
        // Delete a single save
        public int Delete(GameDataModel gameDataModel)
        {
            int newIdNumber = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM dbo.Saves WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", gameDataModel.Id);

                try
                {
                    connection.Open();

                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return newIdNumber;
        }


    }
}
