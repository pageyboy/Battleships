using System;
using System.Threading;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            int boardSize = 4;
            Game battleships = new Game(boardSize);
            while (battleships.GameFinished == false)
            {
                Console.WriteLine();
                if (battleships.isLastAttack)
                {
                    Console.WriteLine(battleships.LastAttackDetails);
                    Console.WriteLine();
                }
                Console.WriteLine("Where do you want to hit?");
                var coords = Console.ReadLine();
                battleships.Fire(coords.ToUpper());
            }
            Console.WriteLine();
            Console.WriteLine("Game finished. You found the ship");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
