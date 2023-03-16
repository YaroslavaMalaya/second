namespace Kse.Algorithms.Samples

{
    using System;
    using System.Collections.Generic;

    public class MapPrinter
    {
        public void Print(string[,] maze, List<Point> path)
        {
            PrintThePath();
            PrintTopLine();
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{row}\t");
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    if (path.Contains(new (column, row)) || maze[column, row] == "A" || maze[column, row] == "B" 
                        || maze[column, row] == "🧚")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(maze[column, row]);
                }

                Console.WriteLine();
            }

            void PrintTopLine()
            {
                Console.Write($" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0? i / 10 : " ");
                }
    
                Console.Write($"\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }
    
                Console.WriteLine("\n");
            } 
            
            void PrintThePath()
            {
                foreach (var point in path)
                {
                    if (!int.TryParse(maze[point.Column, point.Row], out _))
                    {
                        maze[point.Column, point.Row] = "🧚";
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}