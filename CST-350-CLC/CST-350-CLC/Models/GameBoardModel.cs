using System;
using System.Collections.Generic;
using System.Text;
// Created by Casey Huz for CST-250 Milestone Project
namespace CST_350_CLC.Models {
    public class GameBoardModel {
        // Properties
        public int Size { get; set; }
        public int Difficulty { get; set; }
        public GameCellModel[,] Grid { get; set; }
        public int SafeCells { get; set; }
        public int VisitedSafeCells { get; set; }

        // Constructors
        public GameBoardModel(int size, int difficulty) {
            Size = size;
            Difficulty = difficulty;
            Grid = new GameCellModel[size, size];
        }

        public GameBoardModel() {
            Size = 8;
            Difficulty = 2;
            Grid = new GameCellModel[Size, Size];
        }

        // Setting certain squares to be bombs
        public void SetupLiveNeighbors() {
            int row = 0;
            int column = 0;
            int id = 0;
            int chance = 25 / Difficulty;
            Random rnd = new Random();
            int x;
            SafeCells = 0;

            while(row < Size) {
                while(column < Size) {
                    Grid[row, column] = new GameCellModel(row, column, id);
                    x = rnd.Next(0, chance);
                    if (x == 2) {
                        Grid[row, column].Live = true;
                    }
                    else {
                        SafeCells++;
                    }
                    column++;
                    id++;
                }
                row++;
                column = 0;
            }
        }

        // Calculating if nearby spaces are bombs
        public void CalculateLiveNeighbors(int row, int column, int size) {
            int neighbors = 0;

            while (row < size) {
                while (column < size) {
                    neighbors = 0;
                    int newRow = row;
                    int newColumn = column;
                    if (Grid[row, column].Live == true) neighbors = 9;
                    else {
                        // Checking if live
                        newColumn -= 1;
                        newRow -= 1;
                        // Checking row above
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }
                        newColumn += 1;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }
                        newColumn += 1;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }

                        // Checking middle row
                        newRow += 1;
                        newColumn -= 2;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }
                        newColumn += 2;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }

                        // Checking row below
                        newRow += 1;
                        newColumn -= 2;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }
                        newColumn += 1;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }
                        newColumn += 1;
                        if (newRow > -1 && newColumn > -1 && newRow < Size && newColumn < Size) {
                            if (Grid[newRow, newColumn].Live == true) neighbors++;
                        }
                    }
                    Grid[row, column].LiveNeighbors = neighbors;
                    column++;
                }
                row++;
                column = 0;
            }
        } 

        // Recursive method to print cells with no live neighbors
        public void floodFill(int row, int col) {
            Console.WriteLine("Flood Fill Start.");
            if (row > -1 && row < Size && col > -1 && col < Size) {
                if (Grid[row, col].LiveNeighbors > 0 && Grid[row, col].Live == false) {
                    VisitedSafeCells++;
                    return;
                }

                else {
                    Grid[row, col].Visited = true;
                    VisitedSafeCells++;

                    /*
                    Recur checks start with N.W and move row by row
                    N.W -> N -> N.E -> W -> E -> S.W -> S -> S.E
                    Checks for in-range are:
                    Subract -> (number > -1)
                    Addition -> (number < Size)
                    Each execution must verify that it's row/col are valid locations, that it is not a bomb, and that it has not visited that cell before

                    N.W   N   N.E
                      \   |   /
                    W----Cell----E
                       /   |  \
                    S.W    S   S.E
                    */
                    // Checking Row above
                    try {
                        // N.W (Row -1 and Col -1)
                        if (row - 1 > -1 && col - 1 > -1 && Grid[row - 1, col - 1].Live == false && Grid[row - 1, col - 1].Visited == false) {
                            Grid[row - 1, col - 1].Visited = true;
                            Grid[row - 1, col - 1].CurrentCellText = Grid[row - 1, col - 1].LiveNeighbors.ToString();
                            floodFill(row - 1, col - 1);
                        }

                        // N (Row -1 and original Col)
                        if (row - 1 > -1 && col > -1 && Grid[row - 1, col].Live == false && Grid[row - 1, col].Visited == false) {
                            Grid[row - 1, col].Visited = true;
                            Grid[row - 1, col].CurrentCellText = Grid[row - 1, col].LiveNeighbors.ToString();
                            floodFill(row - 1, col);
                        }

                        // N.E (Row -1 and Col +1)
                        if (row - 1 > -1 && col + 1 < Size && Grid[row - 1, col + 1].Live == false && Grid[row - 1, col + 1].Visited == false) {
                            Grid[row - 1, col + 1].Visited = true;
                            Grid[row - 1, col + 1].CurrentCellText = Grid[row - 1, col + 1].LiveNeighbors.ToString();
                            floodFill(row - 1, col + 1);
                        }

                        // Checking Middle Row

                        // W (Original Row and Col -1)
                        if (row > -1 && col - 1 > -1 && Grid[row, col - 1].Live == false && Grid[row, col - 1].Visited == false) {
                            Grid[row, col - 1].Visited = true;
                            Grid[row, col - 1].CurrentCellText = Grid[row, col - 1].LiveNeighbors.ToString();
                            floodFill(row, col - 1);
                        }

                        // E (Original Row and Col + 1)
                        if (row > -1 && col + 1 < Size && Grid[row, col + 1].Live == false && Grid[row, col + 1].Visited == false) {
                            Grid[row, col + 1].Visited = true;
                            Grid[row, col + 1].CurrentCellText = Grid[row, col + 1].LiveNeighbors.ToString();
                            floodFill(row, col + 1);
                        }

                        // Checking Row below

                        // S.W (Row + 1 and Col - 1)
                        if (row + 1 < Size && col - 1 > -1 && Grid[row + 1, col - 1].Live == false && Grid[row + 1, col - 1].Visited == false) {
                            Grid[row + 1, col - 1].Visited = true;
                            Grid[row + 1, col - 1].CurrentCellText = Grid[row + 1, col - 1].LiveNeighbors.ToString();
                            floodFill(row + 1, col - 1);
                        }

                        // S (Row + 1 and Original Col
                        if (row + 1 < Size && col > -1 && Grid[row + 1, col].Live == false && Grid[row + 1, col].Visited == false) {
                            Grid[row + 1, col].Visited = true;
                            Grid[row + 1, col].CurrentCellText = Grid[row + 1, col].LiveNeighbors.ToString();
                            floodFill(row + 1, col);
                        }

                        // S.E (Row + 1 and Col + 1)
                        if (row + 1 < Size && col + 1 < Size && Grid[row + 1, col + 1].Live == false && Grid[row + 1, col + 1].Visited == false) {
                            Grid[row + 1, col + 1].Visited = true;
                            Grid[row + 1, col + 1].CurrentCellText = Grid[row + 1, col + 1].LiveNeighbors.ToString();
                            floodFill(row + 1, col + 1);
                        }
                    }
                    catch(Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            Console.WriteLine("Flood Fill End.");
            return;
        }

    }
}
