﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    internal class Game
    {
        // --------------------------------- Constants ---------------------------------
        public const int CELL_GOAL = 2048;

        // --------------------------------- Properties --------------------------------
        public Board GameBoard {  get; set; }
        public GameStatus Status { get; set; }
        public int Points { get; protected set; }

        // --------------------------------- Methods -----------------------------------
        public Game()
        {
            this.GameBoard = new Board();
            this.Status = GameStatus.Idle;
            this.Points = 0;

            // Game Setup:
            GameBoard.InitRandomCells();
        }

        /// <summary>
        /// Makes a move on the board
        /// </summary>
        /// <param name="direction">Direction to move</param>
        public void Move(Direction direction)
        {
            if (this.Status == GameStatus.Lose)
                return;

            this.Points += this.GameBoard.Move(direction);
            UpdateGameStatus();
        }

        /// <summary>
        /// Checks if there's a win or lose - if so, updates Status property.
        /// </summary>
        private void UpdateGameStatus()
        {
            if (!CanMakeMove())
                this.Status = GameStatus.Lose;
            if (ReachedCellGoal())
                this.Status = GameStatus.Win;
        }

        /// <summary>
        /// Checks if there is a possible move to make on the board.
        /// </summary>
        /// <returns>If there is a possible move left - true, otherwise - false.</returns>
        private bool CanMakeMove()
        {
            for (int i = 0; i < Board.ROWS; i++)
            {
                for (int j = 0; j < Board.COLUMNS; j++)
                {
                    bool hasPossibleMove = false;

                    // Checking if there are 2 equal numbers next to each other:
                    if (i - 1 >= 0)
                        hasPossibleMove |= GameBoard.Data[i, j] == GameBoard.Data[i - 1, j];
                    if (i + 1 < Board.ROWS)
                        hasPossibleMove |= GameBoard.Data[i, j] == GameBoard.Data[i + 1, j];
                    if (j - 1 >= 0)
                        hasPossibleMove |= GameBoard.Data[i, j] == GameBoard.Data[i, j - 1];
                    if (j + 1 < Board.COLUMNS)
                        hasPossibleMove |= GameBoard.Data[i, j] == GameBoard.Data[i, j + 1];

                    // If there is an empty cell - there is another move to make:
                    hasPossibleMove |= GameBoard.Data[i, j] == 0;

                    // If hasPossibleMove=true, we can stop searching, and just return true.
                    if (hasPossibleMove)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the player has won and reached a cell with the number {CELL_GOAL}.
        /// </summary>
        /// <returns>If won - true, otherwise - false.</returns>
        private bool ReachedCellGoal()
        {
            for (int i = 0; i < Board.ROWS; i++)
            {
                for (int j = 0; j < Board.ROWS; j++)
                {
                    if (GameBoard.Data[i, j] == CELL_GOAL)
                        return true;
                }
            }

            return false;
        }
    }
}
