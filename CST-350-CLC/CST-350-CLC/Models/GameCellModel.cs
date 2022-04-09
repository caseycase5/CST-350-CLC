using System;
using System.Collections.Generic;
using System.Text;
// Created by Casey Huz for CST-250 Milestone Project
namespace CST_350_CLC.Models {
    public class GameCellModel {
        // Properties with getters & setters
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Visited { get; set; }
        public bool Live { get; set; }
        public int LiveNeighbors { get; set; }
        public string CurrentCellText { get; set; }
        public int Id { get; set; }

        // Constructors
        public GameCellModel(int row, int column, int id) {
            Id = id;
            Row = row;
            Column = column;
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
            CurrentCellText = "~";
        }

        public GameCellModel(int id, int row, int column, bool visited, bool live, int liveNeighbors, string currentCellText) {
            Id = id;
            Row = row;
            Column = column;
            Visited = visited;
            Live = live;
            LiveNeighbors = liveNeighbors;
            CurrentCellText = currentCellText;
        }

        public GameCellModel() {
            Row = -1;
            Column = -1;
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
            CurrentCellText = "~";
        }
    }
}
