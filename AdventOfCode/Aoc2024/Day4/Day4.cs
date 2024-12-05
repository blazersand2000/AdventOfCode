using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Aoc2024.Day4
{
   public class Day4 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetNumberOfWordMatches(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetNumberOfX_MAS(lines));
      }

      public static int GetNumberOfWordMatches(string[] lines)
      {
         var pattern = "XMAS";

         var searchStrings = GetPassesForEdges(lines).Select(pass => string.Join(string.Empty, pass.Select(coordinate => lines[coordinate.Row][coordinate.Col])));

         return searchStrings.Sum(s => Regex.Matches(s, pattern).Count);
      }

      public static int GetNumberOfX_MAS(string[] lines)
      {
         var count = 0;

         for (int i = 0; i < lines.Length - 2; i++)
         {
            for (int j = 0; j < lines[0].Length - 2; j++)
            {
               if (IsX_MAS(lines, new Coordinate(i, j)))
               {
                  count++;
               }
            }
         }

         return count;
      }

      private static IEnumerable<IEnumerable<Coordinate>> GetPassesForEdges(string[] lines)
      {
         var rows = lines.Length;
         var cols = lines[0].Length;

         // Storing in a dictionary solely to conveniently remove duplicate passes generated starting at the corners
         var passes = new Dictionary<(Coordinate Start, int RowStep, int ColStep), IEnumerable<Coordinate>>();

         var top = Enumerable.Range(0, cols).Select(col => new Coordinate(0, col));
         foreach (var topCoordinate in top)
         {
            passes[(topCoordinate, 1, -1)] = GetCoordinatesInPass(rows, cols, topCoordinate, 1, -1);
            passes[(topCoordinate, 1, 0)] = GetCoordinatesInPass(rows, cols, topCoordinate, 1, 0);
            passes[(topCoordinate, 1, 1)] = GetCoordinatesInPass(rows, cols, topCoordinate, 1, 1);
         }

         var right = Enumerable.Range(0, rows).Select(row => new Coordinate(row, cols - 1));
         foreach (var rightCoordinate in right)
         {
            passes[(rightCoordinate, -1, -1)] = GetCoordinatesInPass(rows, cols, rightCoordinate, -1, -1);
            passes[(rightCoordinate, 0, -1)] = GetCoordinatesInPass(rows, cols, rightCoordinate, 0, -1);
            passes[(rightCoordinate, 1, -1)] = GetCoordinatesInPass(rows, cols, rightCoordinate, 1, -1);
         }

         var bottom = Enumerable.Range(0, cols).Select(col => new Coordinate(rows - 1, col));
         foreach (var bottomCoordinate in bottom)
         {
            passes[(bottomCoordinate, -1, 1)] = GetCoordinatesInPass(rows, cols, bottomCoordinate, -1, 1);
            passes[(bottomCoordinate, -1, 0)] = GetCoordinatesInPass(rows, cols, bottomCoordinate, -1, 0);
            passes[(bottomCoordinate, -1, -1)] = GetCoordinatesInPass(rows, cols, bottomCoordinate, -1, -1);
         }

         var left = Enumerable.Range(0, rows).Select(row => new Coordinate(row, 0));
         foreach (var leftCoordinate in left)
         {
            passes[(leftCoordinate, 1, 1)] = GetCoordinatesInPass(rows, cols, leftCoordinate, 1, 1);
            passes[(leftCoordinate, 0, 1)] = GetCoordinatesInPass(rows, cols, leftCoordinate, 0, 1);
            passes[(leftCoordinate, -1, 1)] = GetCoordinatesInPass(rows, cols, leftCoordinate, -1, 1);
         }

         return passes.Values;
      }

      private static IEnumerable<Coordinate> GetCoordinatesInPass(int rows, int cols, Coordinate start, int rowStep, int colStep)
      {
         var current = start;
         while (CoordinateInBounds(current, rows, cols))
         {
            yield return current;
            current = new(current.Row + rowStep, current.Col + colStep);
         }
      }

      public static bool CoordinateInBounds(Coordinate coordinate, int rows, int cols)
      {
         return coordinate.Col >= 0 && coordinate.Col < cols && coordinate.Row >= 0 && coordinate.Row < rows;
      }

      private static bool IsX_MAS(string[] lines, Coordinate upperLeft)
      {
         var ul = lines[upperLeft.Row][upperLeft.Col];
         var ur = lines[upperLeft.Row][upperLeft.Col + 2];
         var m = lines[upperLeft.Row + 1][upperLeft.Col + 1];
         var ll = lines[upperLeft.Row + 2][upperLeft.Col];
         var lr = lines[upperLeft.Row + 2][upperLeft.Col + 2];

         if (m != 'A')
         {
            return false;
         }
         if (!((ul == 'M' && lr == 'S') || (ul == 'S' && lr == 'M')))
         {
            return false;
         }
         if (!((ur == 'M' && ll == 'S') || (ur == 'S' && ll == 'M')))
         {
            return false;
         }

         return true;
      }
   }

   public record struct Coordinate(int Row, int Col);
}
