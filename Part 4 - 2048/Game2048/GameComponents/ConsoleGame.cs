using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    internal class ConsoleGame
    {
        /*
         This class follows the Singleton design pattern.
         It'd make sense to only play with one instance of the game, especially because you can restart it.
         */

        // --------------------------------- Properties --------------------------------
        private static ConsoleGame instance;
        private Game Game { get; set; }

        private ConsoleGame() { }

        // --------------------------------- Methods -----------------------------------
        public static ConsoleGame GetInstance()
        {
            if (instance == null)
            {
                instance = new ConsoleGame();
            }

            return instance;
        }

        /// <summary>
        /// Starts the 2048 game.
        /// </summary>
        public void StartGame()
        {
            if (Game != null)
                return;

            Game = new Game(); // Init Game
            Console.WriteLine("Use Your Arrow Keys.\nR = Restart\nE = Exit.\nEnjoy :)");

            // Play Game:
            bool stopGame = false;
            bool restartOrExitMode = false;
            while (Game.Status == GameStatus.Idle && !stopGame)
            {
                // Play one game move
                PlayGameMove(ref stopGame);

                // Check for win/lose
                CheckGameStatus(ref restartOrExitMode);

                if (restartOrExitMode)
                {
                    Action restartOrExit = AskRestartOrExit();
                    switch (restartOrExit)
                    {
                        case Action.Restart:
                            Game = new Game();
                            restartOrExitMode = false;
                            break;
                        case Action.Exit:
                            stopGame = true;
                            break;
                    }
                }
            }

            Console.WriteLine("Hope You Enjoyed, Cya :)");
        }

        /// <summary>
        /// Checks the status of the game (Idle, Win or Lose)
        /// </summary>
        /// <param name="restartOrExitMode">ref to restartOrExitMode</param>
        private void CheckGameStatus(ref bool restartOrExitMode)
        {
            switch (Game.Status)
            {
                case GameStatus.Lose:
                    Game.GameBoard.DisplayBoard();
                    Console.WriteLine($"Points: {Game.Points}");

                    Console.WriteLine("You Lost...");
                    Console.WriteLine("R = Restart\nE = Exit");
                    restartOrExitMode = true;
                    break;
                case GameStatus.Win:
                    Game.GameBoard.DisplayBoard();
                    Console.WriteLine($"Points: {Game.Points}");

                    Console.WriteLine("You Won!");
                    Console.WriteLine("R = Restart\nE = Exit");
                    restartOrExitMode = true;
                    break;
                    // If still playing (Idle), there's no need to print anything.
            }
        }

        /// <summary>
        /// Gives the player a move to make.
        /// </summary>
        /// <param name="stopGame">ref to stopGame</param>
        private void PlayGameMove(ref bool stopGame)
        {
            Game.GameBoard.DisplayBoard();
            Console.WriteLine($"Points: {Game.Points}");

            bool validKeyPressed = false;
            while (!validKeyPressed)
            {
                validKeyPressed = true;
                ConsoleKeyInfo pressedKey = Console.ReadKey();                
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        Game.Move(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        Game.Move(Direction.Down);
                        break;
                    case ConsoleKey.RightArrow:
                        Game.Move(Direction.Right);
                        break;
                    case ConsoleKey.LeftArrow:
                        Game.Move(Direction.Left);
                        break;
                    case ConsoleKey.R: // Restart
                        Game = new Game();
                        break;
                    case ConsoleKey.E: // Exit
                        stopGame = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Key Pressed");
                        validKeyPressed = false;
                        break;
                }
            }
            
        }

        /// <summary>
        /// Asks the user if he wishes to restart the game or exit.
        /// </summary>
        /// <returns>The chosen action of the player</returns>
        private Action AskRestartOrExit()
        {
            Action action = Action.Restart; // Default Value

            ConsoleKeyInfo restartOrExit = Console.ReadKey();
            switch (restartOrExit.Key)
            {
                case ConsoleKey.R: // Restart
                    action = Action.Restart;
                    break;
                case ConsoleKey.E: // Exit
                    action = Action.Exit;
                    break;
            }
            while (restartOrExit.Key != ConsoleKey.R && restartOrExit.Key != ConsoleKey.E)
            {
                switch (restartOrExit.Key)
                {
                    case ConsoleKey.R: // Restart
                        action = Action.Restart;
                        break;
                    case ConsoleKey.E: // Exit
                        action = Action.Exit;
                        break;
                }
                restartOrExit = Console.ReadKey();
            }

            return action;
        }
    }
}
