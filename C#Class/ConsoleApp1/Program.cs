using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. SETUP: These stay OUTSIDE the loop
            bool running = true;
            int playerX = 2; // Starting X position
            int playerY = 2; // Starting Y position

            string[] map = {
                "##########",
                "#........#",
                "#........#",
                "#..####..#",
                "#..#..#..#",
                "#........#",
                "##########"
            };

            // 2. THE GAME LOOP
            while (running)
            {
                Console.Clear(); // Clear the screen for a fresh frame

                // DRAW THE MAP
                Console.SetCursorPosition(0, 0);
                foreach (string line in map)
                {
                    Console.WriteLine(line);
                }

                // DRAW THE PLAYER
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("@");

                // WAIT FOR INPUT
                ConsoleKeyInfo key = Console.ReadKey(true);

                // LOGIC: Move based on key
                if (key.Key == ConsoleKey.W) playerY--;
                if (key.Key == ConsoleKey.S) playerY++;
                if (key.Key == ConsoleKey.A) playerX--;
                if (key.Key == ConsoleKey.D) playerX++;

                // Press Escape to quit
                if (key.Key == ConsoleKey.Escape) running = false;
            }
        }
    }
}