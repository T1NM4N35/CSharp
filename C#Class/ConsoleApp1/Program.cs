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
                
                // 1. Calculate the DESIRED next position
                int nextX = playerX;
                int nextY = playerY;

                if (key.Key == ConsoleKey.W) nextY--;
                if (key.Key == ConsoleKey.S) nextY++;
                if (key.Key == ConsoleKey.A) nextX--;
                if (key.Key == ConsoleKey.D) nextX++;

                // 2. COLLISION CHECK: Is the next spot NOT a wall?
                // map[y][x] corresponds to Row then Column
                if (map[nextY][nextX] != '#')
                {
                    playerX = nextX;
                    playerY = nextY;
                }

                // Press Escape to quit
                if (key.Key == ConsoleKey.Escape) running = false;
            }
        }
    }
}