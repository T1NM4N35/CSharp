using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO.MemoryMappedFiles;

namespace ConsoleApp1
{
    internal class Program
    {
        private static int coins = 0;

        static void Main(string[] args)
        {
            // Game setup
            bool running = true;
            int playerX = 2;
            int playerY = 2;
            int currentMap = 0;

            string[] map1 = {
                "##########",
                "#......0.#",
                "#0.......#",
                "#..####..#",
                "#..#.0#..#",
                "#........]",
                "##########"
            };
            
            string[] map2 = {
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
                "#.##.#.#.#.#.#.#####.#.###.##.#",
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

            int enemy_x = 5;
            int enemy_y = 5;

            // Main game loop
            while (running)
            {
                Console.Clear();

                string[] activeMap = mapList[currentMap];

                // Draw map
                Console.SetCursorPosition(0, 0);
                foreach (string line in activeMap)
                {
                    Console.WriteLine(line);
                }
                Console.SetCursorPosition(0, activeMap.Length + 1);
                Console.WriteLine("Coins: " + coins);

                // Draw entities
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("@");

                Console.SetCursorPosition(enemy_x, enemy_y);
                Console.Write("E");

                // Input handling
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                if (key.Key == ConsoleKey.Escape) 
                {
                    running = false;
                    continue;
                }

                // Enemy AI: Move toward player
                int enemyNextX = enemy_x;
                int enemyNextY = enemy_y;
               
                if (playerX == enemy_x && playerY == enemy_y)
                {
                    Console.Clear();
                    Console.WriteLine("You died!");
                    Console.ReadKey(true);
                    break;
                }

                if (enemy_x < playerX) enemyNextX++;
                else if (enemy_x > playerX) enemyNextX--;

                if (enemy_y < playerY) enemyNextY++;
                else if (enemy_y > playerY) enemyNextY--;

                // Enemy collision check
                if (enemyNextY >= 0 && enemyNextY < activeMap.Length &&
                    enemyNextX >= 0 && enemyNextX < activeMap[enemyNextY].Length)
                {
                    char enemyTile = activeMap[enemyNextY][enemyNextX];

                    if (enemyTile != '#')
                    {
                        enemy_x = enemyNextX;
                        enemy_y = enemyNextY;
                    }
                }
                
                // Calculate player next position
                int nextX = playerX;
                int nextY = playerY;

                if (key.Key == ConsoleKey.W) nextY--;
                if (key.Key == ConsoleKey.S) nextY++;
                if (key.Key == ConsoleKey.A) nextX--;
                if (key.Key == ConsoleKey.D) nextX++;

                // Player bounds and collision check
                if (nextY >= 0 && nextY < activeMap.Length &&
                    nextX >= 0 && nextX < activeMap[nextY].Length)
                {
                    char nextTile = activeMap[nextY][nextX];

                    // Transition to next map
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

                    // Win condition
                    if (nextTile == '/')
                    {
                        Console.Clear();
                        Console.WriteLine("Congratulations! You've completed the game!");
                        Console.ReadKey(true);
                        running = false;
                    }

                    // Collectibles
                    if (nextTile == '0')
                    {
                        coins++;

                        string row = activeMap[nextY];
                        char[] rowArray = row.ToCharArray();
                        rowArray[nextX] = '.';
                        activeMap[nextY] = new string(rowArray);
                    }
                }
            }
        }
    }
}