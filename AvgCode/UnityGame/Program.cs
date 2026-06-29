using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        private static int coins = 0;

        static void Main(string[] args)
        {
            bool running = true;
            int playerX = 2;
            int playerY = 2;
            int currentMap = 0;

            string[] map1 =
            {
                "##########",
                "#......0.#",
                "#0.#####.#",
                "#..#..#..#",
                "#.##..#..#",
                "#......0.]",
                "##########"
            };

            string[] map2 =
            {
                "##########",
                "]........#",
                "#..#.###.#",
                "#...0.#.0#",
                "#.##.##..#",
                "#....0...]",
                "##########"
            };

            string[] map3 =
            {
                "###############################",
                "].........0....#......#......##",
                "###.#######.#######.#.#.####.##",
                "#......#....#.,.....#.0....#.0#",
                "#.####.###.###.####.######.##.#",
                "#.#.0...#.......#..........#..#",
                "#.#######.#######.#######.##.##",
                "#.#0#...0...#..#..#0#.....#.0.#",
                "#...#.#.###.#..#.##.#.###.#.#.#",
                "#####.#...#.#.#..#..#.#...#.#.#",
                "#.....###.#.#.......#.#.###.#.#",
                "#.#######.#.#..0.#..#.#.#...#.#",
                "#.#.....#.#.#########.#.#.###.#",
                "#...###.0.#......0....#...0.../",
                "###############################"
            };

            List<string[]> mapList = new List<string[]>();
            mapList.Add(map1);
            mapList.Add(map2);
            mapList.Add(map3);

            int enemyX = 5;
            int enemyY = 5;

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

                Console.SetCursorPosition(playerX, playerY);
                Console.Write("@");

                Console.SetCursorPosition(enemyX, enemyY);
                Console.Write("E");

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    running = false;
                    continue;
                }

                int nextX = playerX;
                int nextY = playerY;

                if (key.Key == ConsoleKey.W) nextY--;
                if (key.Key == ConsoleKey.S) nextY++;
                if (key.Key == ConsoleKey.A) nextX--;
                if (key.Key == ConsoleKey.D) nextX++;

                if (IsInsideMap(activeMap, nextX, nextY))
                {
                    char nextTile = activeMap[nextY][nextX];

                    if (nextTile == '#')
                    {
                        // blocked by wall, do nothing
                    }
                    else if (nextTile == ']')
                    {
                        currentMap++;
                        if (currentMap >= mapList.Count)
                        {
                            currentMap = 0;
                        }

                        activeMap = mapList[currentMap];

                        if (!TryFindTile(activeMap, ']', out playerX, out playerY))
                        {
                            playerX = 1;
                            playerY = 1;
                        }

                        if (!TryFindEnemySpawn(activeMap, playerX, playerY, out enemyX, out enemyY))
                        {
                            enemyX = 1;
                            enemyY = 1;
                        }

                        if (enemyX == playerX && enemyY == playerY)
                        {
                            Console.Clear();
                            Console.WriteLine("You died!");
                            Console.ReadKey(true);
                            break;
                        }

                        continue;
                    }
                    else if (nextTile == '/')
                    {
                        playerX = nextX;
                        playerY = nextY;

                        Console.Clear();
                        Console.WriteLine("Congratulations! You've completed the game!");
                        Console.ReadKey(true);
                        running = false;
                        continue;
                    }
                    else
                    {
                        playerX = nextX;
                        playerY = nextY;

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

                if (enemyX == playerX && enemyY == playerY)
                {
                    Console.Clear();
                    Console.WriteLine("You died!");
                    Console.ReadKey(true);
                    break;
                }

                int enemyNextX = enemyX;
                int enemyNextY = enemyY;

                if (enemyX < playerX) enemyNextX++;
                else if (enemyX > playerX) enemyNextX--;

                if (enemyY < playerY) enemyNextY++;
                else if (enemyY > playerY) enemyNextY--;

                if (IsInsideMap(activeMap, enemyNextX, enemyNextY))
                {
                    char enemyTile = activeMap[enemyNextY][enemyNextX];

                    if (enemyTile != '#')
                    {
                        enemyX = enemyNextX;
                        enemyY = enemyNextY;
                    }
                }

                if (enemyX == playerX && enemyY == playerY)
                {
                    Console.Clear();
                    Console.WriteLine("You died!");
                    Console.ReadKey(true);
                    break;
                }
            }
        }

        private static bool IsInsideMap(string[] map, int x, int y)
        {
            return y >= 0 && y < map.Length && x >= 0 && x < map[y].Length;
        }

        private static bool TryFindTile(string[] map, char tile, out int x, out int y)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == tile)
                    {
                        x = col;
                        y = row;
                        return true;
                    }
                }
            }

            x = -1;
            y = -1;
            return false;
        }

        private static bool TryFindEnemySpawn(string[] map, int playerX, int playerY, out int spawnX, out int spawnY)
        {
            spawnX = -1;
            spawnY = -1;

            int bestDistance = -1;

            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] != '.')
                    {
                        continue;
                    }

                    int dx = col - playerX;
                    int dy = row - playerY;
                    int distance = dx * dx + dy * dy;

                    if (distance > bestDistance)
                    {
                        bestDistance = distance;
                        spawnX = col;
                        spawnY = row;
                    }
                }
            }

            return bestDistance >= 0;
        }
    }
}
