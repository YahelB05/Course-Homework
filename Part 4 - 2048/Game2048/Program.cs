using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleGame consoleGame = ConsoleGame.GetInstance();
            consoleGame.StartGame();

            Console.ReadLine();
        }
    }
}
