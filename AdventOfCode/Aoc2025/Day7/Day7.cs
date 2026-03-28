using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day7;

public class Day7 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(Part1(lines));

      Console.WriteLine("Part 2:");
      Console.WriteLine(Part2(lines));
   }

   public static int Part1(string[] lines)
   {
      var grid = lines.Select(l => l.ToCharArray()).ToArray();
      return GetNumberOfSplits(grid, 1);
   }

   public static long Part2(string[] lines)
   {
      var grid = lines.Select(l => l.ToCharArray()).ToArray();
      var start = new Coordinate(1, lines[0].IndexOf('S'));
      return GetNumberOfTimelines(grid, start, new Dictionary<Coordinate, long>());
   }

   public static int GetNumberOfSplits(char[][] grid, int currentRow)
   {
      if (currentRow >= grid.Length)
      {
         return 0;
      }

      List<int> splitIndices = [];

      for (int j = 0; j < grid[currentRow].Length; j++)
      {
         if (grid[currentRow - 1][j] is 'S' or '|')
         {
            if (grid[currentRow][j] == '^')
            {
               splitIndices.Add(j);
               grid[currentRow][j - 1] = '|';
               grid[currentRow][j + 1] = '|';
            }
            else
            {
               grid[currentRow][j] = '|';
            }
         }
      }

      return splitIndices.Count + GetNumberOfSplits(grid, currentRow + 1);
   }

   public static long GetNumberOfTimelines(char[][] grid, Coordinate current, Dictionary<Coordinate, long> memo)
   {
      if (memo.TryGetValue(current, out var numberOfTimelines))
      {
         return numberOfTimelines;
      }

      if (current.Row >= grid.Length)
      {
         memo[current] = 1;
         return 1;
      }

      if (grid[current.Row][current.Col] == '^')
      {
         var left = GetNumberOfTimelines(grid, current with { Row = current.Row + 1, Col = current.Col - 1 }, memo);
         var right = GetNumberOfTimelines(grid, current with { Row = current.Row + 1, Col = current.Col + 1 }, memo);
         memo[current] = left + right;

         return left + right;
      }

      numberOfTimelines = GetNumberOfTimelines(grid, current with { Row = current.Row + 1 }, memo);
      memo[current] = numberOfTimelines;

      return numberOfTimelines;
   }

   public readonly record struct Coordinate(int Row, int Col);
}
