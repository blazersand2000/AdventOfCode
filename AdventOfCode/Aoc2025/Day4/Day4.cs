using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day4;

public class Day4 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(GetCountOfAccessibleRolls(lines));

      Console.WriteLine("Part 2:");
      Console.WriteLine(GetTotalRemovals(lines));
   }

   public static int GetCountOfAccessibleRolls(string[] lines)
   {
      return GetAccessibleRolls(lines).Count;
   }

   public static int GetTotalRemovals(string[] lines)
   {
      var accessibleRolls = GetAccessibleRolls(lines);

      if (accessibleRolls.Count == 0)
      {
         return 0;
      }

      var gridWithAccessibleRollsRemoved = GetGridWithAccessibleRollsRemoved(lines, accessibleRolls);

      return accessibleRolls.Count + GetTotalRemovals(gridWithAccessibleRollsRemoved);
   }

   public static string[] GetGridWithAccessibleRollsRemoved(string[] lines, HashSet<Coordinate> accessibleRolls)
   {
      var newGrid = new string[lines.Length];

      for (int i = 0; i < lines.Length; i++)
      {
         var charArray = new char[lines[i].Length];
         for (int j = 0; j < lines[0].Length; j++)
         {
            charArray[j] = accessibleRolls.Contains(new Coordinate(i, j)) ? '.' : lines[i][j];
         }
         newGrid[i] = new string(charArray);
      }

      return newGrid;
   }

   private static HashSet<Coordinate> GetAccessibleRolls(string[] lines)
   {
      HashSet<Coordinate> coordinates = [];
      for (int i = 0; i < lines.Length; i++)
      {
         for (int j = 0; j < lines[0].Length; j++)
         {
            if (lines[i][j] == '@' && GetAdjacents(i, j, lines).Count(a => a == '@') < 4)
            {
               coordinates.Add(new(i, j));
            }
         }
      }

      return coordinates;
   }

   private static char[] GetAdjacents(int row, int col, string[] lines)
   {
      List<char> adjacents = [];

      for (int i = row - 1; i < row + 2; i++)
      {
         for (int j = col - 1; j < col + 2; j++)
         {
            if (InBounds(i, j, lines) && !(i == row && j == col))
            {
               adjacents.Add(lines[i][j]);
            }
         }
      }

      return [.. adjacents];
   }

   private static bool InBounds(int row, int col, string[] lines)
   {
      return row >= 0 && row < lines.Length && col >= 0 && col < lines[0].Length;
   }

   public readonly record struct Coordinate(int X, int Y);
}
