using CST_350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CST_350_CLC.Services {
    public class GameDAO {
        // change to your local database connection. COPY the Connection string property of your database and paste below
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CLC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public GameBoardModel startup() {
            // Creation of the gameboard done when index is called.
            GameBoardModel gameBoard = new GameBoardModel();
            Console.WriteLine("Game board created");
            gameBoard.SetupLiveNeighbors();
            Console.WriteLine("Neighbors setup");
            gameBoard.CalculateLiveNeighbors(0, 0, 8);
            Console.WriteLine("Live Neighbors Calculated");

            // Updates all cells in the database
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                int row = 0;
                int col = 0;
                int id = 0;

                while (row < 8) {
                    while(col < 8) {
                    
                        string query = "UPDATE dbo.Cells SET [ROW] = @Row, [COLUMN] = @Col, [VISITED] = @Visited, [LIVE] = @Live, [LIVE_NEIGHBORS] = @LiveNeighbors, [CURRENT_CELL_TEXT] = @CellText WHERE [ID] = @Id";

                        // Define the value of the placeholder in the SQL query
                        SqlCommand command = new SqlCommand(query, connection);

                        // Setting the parameters to defaults
                        
                        command.Parameters.AddWithValue("@Row", gameBoard.Grid[row, col].Row);
                        command.Parameters.AddWithValue("@Col", gameBoard.Grid[row, col].Column);
                        command.Parameters.AddWithValue("@Visited", gameBoard.Grid[row, col].Visited);
                        command.Parameters.AddWithValue("@Live", gameBoard.Grid[row, col].Live);
                        command.Parameters.AddWithValue("@LiveNeighbors", gameBoard.Grid[row, col].LiveNeighbors);
                        command.Parameters.AddWithValue("@CellText", gameBoard.Grid[row, col].CurrentCellText);
                        command.Parameters.AddWithValue("@Id", id);

                        try {
                            connection.Open();
                            Convert.ToInt32(command.ExecuteScalar());
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }
                        connection.Close();
                        col++;
                        id++;
                    }
                    row++;
                    col = 0;
                }
                return gameBoard;
            }
        }

        // This updates the stored gameboard based on the database data. Changes are made to the local gameboard before being pushed to the database
        public GameBoardModel updateBoard(int Id) {
            int id = Id;
            int startingRow = 0;
            int startingCol = 0;

            GameBoardModel gameBoard = new GameBoardModel();
                    string query = "SELECT * FROM dbo.Cells";

            // Updating the locally stored gameboard.
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(query, connection);

                try {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read()) {
                        // Case for the one cell that is clicked. Visited status is updated, cell text is changed to the cell's # of live neighbors and the floodfill algorithm is called
                        if(id == (int)reader[0]) {
                            gameBoard.Grid[((int)reader[1]), ((int)reader[2])] = new GameCellModel((int)reader[0], (int)reader[1], (int)reader[2], true, (bool)reader[4], (int)reader[5], reader[5].ToString());
                            Console.WriteLine("Game cell at " + (int)reader[1] + " " + (int)reader[2] + " updated.");
                            startingRow = (int)reader[1];
                            startingCol = (int)reader[2];
                        }

                        // Case for all other cells that were not clicked on
                        else {
                            gameBoard.Grid[((int)reader[1]), ((int)reader[2])] = new GameCellModel((int)reader[0], (int)reader[1], (int)reader[2], (bool)reader[3], (bool)reader[4], (int)reader[5], (string)reader[6]);
                            Console.WriteLine("Game cell at " + (int)reader[1] + " " + (int)reader[2] + " updated.");
                        }
                    }
                    connection.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            // Recursion algorithm executing after all cells have been recreated
            Console.WriteLine("Starting r/c: " + startingRow + startingCol);
            gameBoard.floodFill(startingRow, startingCol);

            // Updating the SQL server for all cells. Only things that need updates are visited & cell text
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                int row = 0;
                int col = 0;
                id = 0;

                while (row < 8) {
                    while (col < 8) {

                        query = "UPDATE dbo.Cells SET [VISITED] = @Visited, [CURRENT_CELL_TEXT] = @CellText WHERE [ID] = @Id";

                        // Define the value of the placeholder in the SQL query
                        SqlCommand command = new SqlCommand(query, connection);

                        // Setting the parameters to defaults
                        command.Parameters.AddWithValue("@Visited", gameBoard.Grid[row, col].Visited);
                        command.Parameters.AddWithValue("@CellText", gameBoard.Grid[row, col].CurrentCellText);
                        command.Parameters.AddWithValue("@Id", id);

                        try {
                            connection.Open();
                            Convert.ToInt32(command.ExecuteScalar());
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }
                        connection.Close();
                        col++;
                        id++;
                        Console.WriteLine("ID during creation is: " + id);
                    }
                    row++;
                    col = 0;
                }
            }
                return gameBoard;
        }

        // Flood Fill recursion algorithm


    }
}
