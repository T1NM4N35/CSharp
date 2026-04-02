using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO.MemoryMappedFiles;

namespace ConsoleApp1
{
    internal class Program
    {
        // Change from string to int and initialize to 0
        private static int coins = 0;

        static void Main(string[] args)
        {
            // 1. SETUP: These stay OUTSIDE the loop
            bool running = true;
            int playerX = 2; // Starting X position
            int playerY = 2; // Starting Y position
            int currentMap = 0; // Track which map we are currently on

            string[] map1 = {
                "##########",
                "#......0.#",
                "#0.......#",
                "#..####..#",
                "#..#.0#..#",
                "#........]",
                "##########"
            };
            
            string [] map2 = {
                "##########",
                "]........#",
                "#..#...###",
                "#...0.#.0#",
                "#.#####..#",
                "#....0...]",
                "##########"
            };

            string[] map3 = { 
                "###############################",
                "].........0...........#......##",
                "###.#.#####.#.#######.#.####.##",
                "#...#.#...#.#.#.....#.#....#..#",
                "#.###.#.#.#.#.#.#####.#.###.##.#",
                "#.#.0...#...#...#..........#..#",
                "#.#.#####.#######.#######.##.##",
                "#.#.#...0...#.......#...#.#.0.#",
                "#...#.#.###.#.......#.###.#.#.#",
                "#####.#...#.#.......#.#...#.#.#",
                "#.....###.#.#.......#.#.###.#.#",
                "#.#######.#.#.......#.#.#...#.#",
                "#.#.....#.#.#########.#.#.###.#",
                "#...###.0.#...........#...0.../",
                "###############################"
            };

            List<string[]> mapList = new List<string[]>();
            mapList.Add(map1);
            mapList.Add(map2);
            mapList.Add(map3);

            // 2. THE GAME LOOP
            while (running)
            {
                Console.Clear(); // Clear the screen for a fresh frame

                // Grab the currently active map data
                string[] activeMap = mapList[currentMap];

                // DRAW THE MAP
                Console.SetCursorPosition(0, 0);
                foreach (string line in activeMap)
                {
                    Console.WriteLine(line);
                }
                Console.SetCursorPosition(0, activeMap.Length + 1);
                Console.WriteLine("Coins: " + coins);
            ;   

            // DRAW THE PLAYER
            Console.SetCursorPosition(playerX, playerY);
                Console.Write("@");

                // WAIT FOR INPUT
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                // Press Escape to quit
                if (key.Key == ConsoleKey.Escape) 
                {
                    running = false;
                    continue; // Skip the rest of the loop and exit
                }
                
                // 1. Calculate the DESIRED next position
                int nextX = playerX;
                int nextY = playerY;

                if (key.Key == ConsoleKey.W) nextY--;
                if (key.Key == ConsoleKey.S) nextY++;
                if (key.Key == ConsoleKey.A) nextX--;
                if (key.Key == ConsoleKey.D) nextX++;

                // 2. COLLISION CHECK: Is the next spot NOT a wall?
                if (nextY >= 0 && nextY < activeMap.Length &&
                    nextX >= 0 && nextX < activeMap[nextY].Length)
                {
                    char nextTile = activeMap[nextY][nextX];

                    if (nextTile == ']')
                    {
                        currentMap++;
                        if (currentMap >= mapList.Count)
                            currentMap = 0;

                        playerX = 2;
                        playerY = 2;
                    }
                    else if (nextTile != '#')
                    {
                        playerX = nextX;
                        playerY = nextY;
                    }
                    if (nextTile == '/')
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Congratulations! You've completed the game!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey(true);
                        running = false;
                    }
                    if (nextTile == '0')
                    {
                        coins++;

                        string row = activeMap[nextY];
                        char[] rowArray = row.ToCharArray();
                        rowArray[nextX] = '.';
                        activeMap[nextY] = new string(rowArray);
                    }
                    ;

                    }
            }
        }
    }
}