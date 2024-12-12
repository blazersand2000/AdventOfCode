using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day12
{
   public class Day12 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetTotalPriceOfFencing(lines));

         // Console.WriteLine("Part 2:");
         // Console.WriteLine(GetNumberOfStonesWithMemoization(75, lines));
      }

      public static int GetTotalPriceOfFencing(string[] lines)
      {
         int price = 0;
         HashSet<Coordinate> visited = [];
         for (int i = 0; i < lines.Length; i++)
         {
            for (int j = 0; j < lines[i].Length; j++)
            {
               var region = FindRegion(new Coordinate(i, j), visited, lines);
               price += region.Area * region.Perimeter;
            }
         }

         return price;
      }

      private static Region FindRegion(Coordinate plot, HashSet<Coordinate> visited, string[] map)
      {
         if (visited.Contains(plot))
         {
            return new Region(0, 0);
         }

         visited.Add(plot);
         var region = new Region(1, 4);
         var neighboringPlots = new[] { plot.North(), plot.South(), plot.East(), plot.West() };

         foreach (var neighboringPlot in neighboringPlots)
         {
            if (IsInBounds(neighboringPlot, map) && map[neighboringPlot.Row][neighboringPlot.Col] == map[plot.Row][plot.Col])
            {
               region = new Region(region.Area, region.Perimeter - 1);

               if (!visited.Contains(neighboringPlot))
               {
                  var remainingRegion = FindRegion(neighboringPlot, visited, map);
                  region = new Region(region.Area + remainingRegion.Area, region.Perimeter + remainingRegion.Perimeter);
               }
            }
         }

         return region;
      }

      private static bool IsInBounds(Coordinate coordinate, string[] map)
      {
         return coordinate.Row >= 0 && coordinate.Row < map.Length
         && coordinate.Col >= 0 && coordinate.Col < map[0].Length;
      }
   }


   public record struct Coordinate(int Row, int Col)
   {
      public Coordinate North() => new(Row - 1, Col);
      public Coordinate South() => new(Row + 1, Col);
      public Coordinate East() => new(Row, Col + 1);
      public Coordinate West() => new(Row, Col - 1);
   }

   public record struct Region(int Area, int Perimeter);

}