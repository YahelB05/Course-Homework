using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    internal class Board
    {
        // --------------------------------- Constants ---------------------------------
        public const int ROWS = 4;
        public const int COLUMNS = 4;
        public const int RANDOM_INIT_CELLS = 2;
        public readonly int[] RANDOM_INIT_CELLS_OPTIONS = { 2, 4 };

        // --------------------------------- Properties --------------------------------
        public int[ , ] Data {  get; protected set; }

        public Board()
        {
            Data = new int[ROWS, COLUMNS];
            ResetBoard();
        }

        // --------------------------------- Methods -----------------------------------
        
        /// <summary>
        /// Sets all the board cells to 0
        /// </summary>
        private void ResetBoard()
        {
            for (int i = 0; i <  ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    Data[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Sets {RANDOM_INIT_CELLS} random cells to a random value.
        /// </summary>
        public void InitRandomCells()
        {
            Random random = new Random();

            for (int i = 0; i < RANDOM_INIT_CELLS; i++)
            {
                int randomNumber = GenerateRandomInitNumber();
                var (row, column) = GenerateRandomFreeCell();
                Data[row, column] = randomNumber;
            }
        }

        /// <summary>
        /// Handles the move operation on the Data 2D array.
        /// </summary>
        /// <param name="direction">The direction of the move</param>
        /// <returns>Points earned during the move</returns>
        public int Move(Direction direction)
        {
            int pointsEarned = 0;
            int movedCells = 0;
            switch (direction)
            {
                case Direction.Up:
                    MoveUp(out pointsEarned, out movedCells);
                    break;
                case Direction.Down:
                    MoveDown(out pointsEarned, out movedCells);
                    break;
                case Direction.Right:
                    MoveRight(out pointsEarned, out movedCells);
                    break;
                case Direction.Left:
                    MoveLeft(out pointsEarned, out movedCells);
                    break;
            }

            if (movedCells > 0)
                AddNumberToRandomFreeCell();

            return pointsEarned;
        }

        /// <summary>
        /// Move Up
        /// </summary>
        /// <param name="pointsEarned">ref to pointsEarned</param>
        /// <param name="movedCells">ref to movedCells</param>
        private void MoveUp(out int pointsEarned, out int movedCells)
        {
            pointsEarned = 0;
            movedCells = 0;
            HashSet<Cell> mergedPositions = new HashSet<Cell>(); // Cell positions that a cell-merge occurred

            for (int j = 0; j < COLUMNS; j++)
            {
                for (int i = 1; i < ROWS; i++)
                {
                    int row = i;
                    while (row > 0 && !mergedPositions.Contains(Cell.CreateCell(row, j)) && !mergedPositions.Contains(Cell.CreateCell(row - 1, j)) && ((Data[row, j] == Data[row - 1, j] && Data[row, j] != 0) || (Data[row, j] > 0 && Data[row - 1, j] == 0)))
                    {
                        if (Data[row, j] == Data[row - 1, j] && Data[row, j] != 0)
                        {
                            pointsEarned += Data[row, j] + Data[row - 1, j];
                            mergedPositions.Add(Cell.CreateCell(row - 1, j));
                        }

                        Data[row - 1, j] += Data[row, j];
                        Data[row, j] = 0;

                        movedCells++;
                        row--;
                    }
                }
            }
        }

        /// <summary>
        /// Move Down
        /// </summary>
        /// <param name="pointsEarned">ref to pointsEarned</param>
        /// <param name="movedCells">ref to movedCells</param>
        private void MoveDown(out int pointsEarned, out int movedCells)
        {
            pointsEarned = 0;
            movedCells = 0;
            HashSet<Cell> mergedPositions = new HashSet<Cell>(); // Cell positions that a cell-merge occurred

            for (int j = 0; j < COLUMNS; j++)
            {
                for (int i = ROWS - 2; i >= 0; i--)
                {
                    int row = i;
                    while (row < ROWS - 1 && !mergedPositions.Contains(Cell.CreateCell(row, j)) && !mergedPositions.Contains(Cell.CreateCell(row + 1, j)) && ((Data[row, j] == Data[row + 1, j] && Data[row, j] != 0) || (Data[row, j] > 0 && Data[row + 1, j] == 0)))
                    {
                        if (Data[row, j] == Data[row + 1, j] && Data[row, j] != 0)
                        {
                            pointsEarned += Data[row, j] + Data[row + 1, j];
                            mergedPositions.Add(Cell.CreateCell(row + 1, j));
                        }

                        Data[row + 1, j] += Data[row, j];
                        Data[row, j] = 0;

                        movedCells++;
                        row++;
                    }
                }
            }
        }

        /// <summary>
        /// Move Right
        /// </summary>
        /// <param name="pointsEarned">ref to pointsEarned</param>
        /// <param name="movedCells">ref to movedCells</param>
        private void MoveRight(out int pointsEarned, out int movedCells)
        {
            pointsEarned = 0;
            movedCells = 0;
            HashSet<Cell> mergedPositions = new HashSet<Cell>(); // Cell positions that a cell-merge occurred

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = COLUMNS - 2; j >= 0; j--)
                {
                    int col = j;
                    while (col < COLUMNS - 1 && !mergedPositions.Contains(Cell.CreateCell(i, col)) && !mergedPositions.Contains(Cell.CreateCell(i, col + 1)) && ((Data[i, col] == Data[i, col + 1] && Data[i, col] != 0) || (Data[i, col] > 0 && Data[i, col + 1] == 0)))
                    {
                        if (Data[i, col] == Data[i, col + 1] && Data[i, col] != 0)
                        {
                            pointsEarned += Data[i, col] + Data[i, col + 1];
                            mergedPositions.Add(Cell.CreateCell(i, col + 1));
                        }

                        Data[i, col + 1] += Data[i, col];
                        Data[i, col] = 0;

                        movedCells++;
                        col++;
                    }
                }
            }
        }

        /// <summary>
        /// Move Left
        /// </summary>
        /// <param name="pointsEarned">ref to pointsEarned</param>
        /// <param name="movedCells">ref to movedCells</param>
        private void MoveLeft(out int pointsEarned, out int movedCells)
        {
            pointsEarned = 0;
            movedCells = 0;
            HashSet<Cell> mergedPositions = new HashSet<Cell>(); // Cell positions that a cell-merge occurred

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 1; j < COLUMNS; j++)
                {
                    int col = j;
                    while (col > 0 && !mergedPositions.Contains(Cell.CreateCell(i, col)) && !mergedPositions.Contains(Cell.CreateCell(i, col - 1)) && ((Data[i, col] == Data[i, col - 1] && Data[i, col] != 0) || (Data[i, col] > 0 && Data[i, col - 1] == 0)))
                    {
                        if (Data[i, col] == Data[i, col - 1] && Data[i, col] != 0)
                        {
                            pointsEarned += Data[i, col] + Data[i, col - 1];
                            mergedPositions.Add(Cell.CreateCell(i, col - 1));
                        }

                        Data[i, col - 1] += Data[i, col];
                        Data[i, col] = 0;

                        movedCells++;
                        col--;
                    }
                }
            }
        }

        /// <summary>
        /// Chooses a random free cell and gives it a random number.
        /// </summary>
        private void AddNumberToRandomFreeCell()
        {
            var (row, column) = GenerateRandomFreeCell();
            Data[row, column] = GenerateRandomInitNumber();
        }

        /// <summary>
        /// Generates a random cell position and returns it as a tuple.
        /// </summary>
        /// <returns>Tuple of the row and the column of the randomly chosen cell</returns>
        private (int randomRow, int randomCol) GenerateRandomCell()
        {
            Random random = new Random();
            int randomRow = random.Next(ROWS);
            int randomCol = random.Next(COLUMNS);

            return (randomRow, randomCol);
        }

        /// <summary>
        /// Checks whether the given cell position is occupied or not.
        /// </summary>
        /// <param name="row">Row of the cell</param>
        /// <param name="column">Column of the cell</param>
        /// <returns>True if free, otherwise - false.</returns>
        private bool IsFreeCell(int row, int column)
        {
            return Data[row, column] <= 0;
        }

        /// <summary>
        /// Generates a random and unoccupied cell.
        /// </summary>
        /// <returns>Tuple of the row and column of the random unoccupied cell.</returns>
        private (int row, int column) GenerateRandomFreeCell()
        {
            var (row, column) = GenerateRandomCell();
            while (!IsFreeCell(row, column))
            {
                (row, column) = GenerateRandomCell();
            }

            return (row, column);
        }

        /// <summary>
        /// Generates a random number out of RANDOM_INIT_CELLS_OPTIONS array.
        /// </summary>
        /// <returns>Random number from the RANDOM_INIT_CELLS_OPTIONS array</returns>
        private int GenerateRandomInitNumber()
        {
            Random random = new Random();
            return RANDOM_INIT_CELLS_OPTIONS[random.Next(RANDOM_INIT_CELLS_OPTIONS.Length)];
        }

        /// <summary>
        /// Displays the board in stdout.
        /// </summary>
        public void DisplayBoard()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    string charToDisplay = Data[i, j] == 0 ? "." : Data[i, j].ToString();
                    Console.Write(charToDisplay + (j + 1 != COLUMNS ? "\t" : ""));
                }
                Console.WriteLine();
            }
        }
    }
}
