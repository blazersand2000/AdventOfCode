using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day10
{
   public class Day10 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSumOfTrailheadScores(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetSumOfTrailheadRatings(lines));

      }

      public static int GetSumOfTrailheadScores(string[] lines)
      {
         int score = 0;
         for (int i = 0; i < lines.Length; i++)
         {
            for (int j = 0; j < lines[i].Length; j++)
            {
               var currentLocation = new Coordinate(i, j);
               score += HikeAllTrails(currentLocation, 0, lines).ToHashSet().Count;
            }
         }

         return score;
      }

      public static int GetSumOfTrailheadRatings(string[] lines)
      {
         int score = 0;
         for (int i = 0; i < lines.Length; i++)
         {
            for (int j = 0; j < lines[i].Length; j++)
            {
               var currentLocation = new Coordinate(i, j);
               score += HikeAllTrails(currentLocation, 0, lines).Count;
            }
         }

         return score;
      }

      private static List<Coordinate> HikeAllTrails(Coordinate currentLocation, int expectedHeight, string[] map)
      {
         if (!LocationWithinMap(currentLocation, map))
         {
            return [];
         }
         var currentHeight = int.Parse(map[currentLocation.Row][currentLocation.Col].ToString());
         if (currentHeight != expectedHeight)
         {
            return [];
         }
         if (currentHeight == 9)
         {
            return [currentLocation];
         }

         var nextHeight = expectedHeight + 1;
         var north = HikeAllTrails(new Coordinate(currentLocation.Row - 1, currentLocation.Col), nextHeight, map);
         var south = HikeAllTrails(new Coordinate(currentLocation.Row + 1, currentLocation.Col), nextHeight, map);
         var east = HikeAllTrails(new Coordinate(currentLocation.Row, currentLocation.Col + 1), nextHeight, map);
         var west = HikeAllTrails(new Coordinate(currentLocation.Row, currentLocation.Col - 1), nextHeight, map);

         return north.Concat(south).Concat(east).Concat(west).ToList();
      }

      private static bool LocationWithinMap(Coordinate currentLocation, string[] map)
      {
         return currentLocation.Row >= 0 && currentLocation.Row < map.Length && currentLocation.Col >= 0 && currentLocation.Col < map[0].Length;
      }
   }

   public record struct Coordinate(int Row, int Col);
}