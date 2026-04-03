using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO.MemoryMappedFiles;

namespace TerminalGame
{
    internal class Program
    {
        private static int coins = 0;

        static void Main(string[] args)
        {
            
            bool running = true; //maps with mulitple maps 
            int player_X = 2;
            int player_Y = 2;
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

                Console.SetCursorPosition(0, 0);
                foreach (string line in activeMap)
                {
                    Console.WriteLine(line);
                }
                Console.SetCursorPosition(0, activeMap.Length + 1);
                Console.WriteLine("Coins: " + coins);

                Console.SetCursorPosition(player_X, player_Y); //player, enemy, along with the stats bar for the coins
                Console.Write("@");

                Console.SetCursorPosition(enemy_x, enemy_y);
                Console.Write("E");

                ConsoleKeyInfo key = Console.ReadKey(true); //player movement, enemy movement, along with the win and lose conditions, and the coin collection system

                if (key.Key == ConsoleKey.Escape)
                {
                    running = false;
                    continue;
                }

                int enemyNext_X = enemy_x;
                int enemyNext_Y = enemy_y;

                if (player_X == enemy_x && player_Y == enemy_y)
                {
                    Console.Clear();
                    Console.WriteLine("You died!");
                    Console.ReadKey(true);
                    break;
                }

                if (enemy_x < player_X) enemyNext_X++;
                else if (enemy_x > player_X) enemyNext_X--;

                if (enemy_y < player_Y) enemyNext_Y++;
                else if (enemy_y > player_Y) enemyNext_Y--;
                if (enemyNext_Y >= 0 && enemyNext_Y < activeMap.Length &&
                    enemyNext_X >= 0 && enemyNext_X < activeMap[enemyNext_Y].Length)
                {
                    char enemyTile = activeMap[enemyNext_Y][enemyNext_X];

                    if (enemyTile != '#')
                    {
                        enemy_x = enemyNext_X;
                        enemy_y = enemyNext_Y;
                    }
                }
                int next_X = player_X;
                int next_Y = player_Y;

                if (key.Key == ConsoleKey.W) next_Y--;
                if (key.Key == ConsoleKey.S) next_Y++;
                if (key.Key == ConsoleKey.A) next_X--;
                if (key.Key == ConsoleKey.D) next_X++;

                // Player bounds and collision check
                if (next_Y >= 0 && next_Y < activeMap.Length &&
                    next_X >= 0 && next_X < activeMap[next_Y].Length)
                {
                    char nextTile = activeMap[next_Y][next_X];

                    // Transition to next map
                    if (nextTile == ']')
                    {
                        currentMap++;
                        if (currentMap >= mapList.Count)
                            currentMap = 0;

                        player_X = 2;
                        player_Y = 2;
                    }
                    else if (nextTile != '#')
                    {
                        player_X = next_X;
                        player_Y = next_Y;
                    }

                    // Win condition
                    if (nextTile == '/')
                    {
                        Console.Clear();
                        Console.WriteLine("Congratulations! You've completed the game!");
                        Console.ReadKey(true);
                        running = false;
                    }

                    
                    if (nextTile == '0') //coin system and replacement so the game doesn't infinitely give coins
                    {
                        coins++;

                        string row = activeMap[next_Y];
                        char[] rowArray = row.ToCharArray();
                        rowArray[next_X] = '.';
                        activeMap[next_Y] = new string(rowArray);
                    }
                }
            }
        }
    }
}