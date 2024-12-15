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

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetTotalPriceOfFencingWithBulkDiscount(lines));
      }

      public static int GetTotalPriceOfFencing(string[] lines)
      {
         var regions = FindAllRegions(lines);
         return regions.Sum(r => r.Area * r.Perimeter);
      }

      public static int GetTotalPriceOfFencingWithBulkDiscount(string[] lines)
      {
         var regions = FindAllRegions(lines);
         var price = 0;
         foreach (var region in regions)
         {
            var edges = CountEdges(lines, region);
            price += region.Area * edges;
         }

         return price;
      }

      private static int CountEdges(string[] lines, Region region)
      {
         var edges = 0;
         var linePairs = GetLinePairs(lines);
         foreach (var linePair in linePairs)
         {
            var status = EdgeStatus.None;
            foreach (var pair in linePair)
            {
               var newStatus = (region.Plots.Contains(pair.A), region.Plots.Contains(pair.B)) switch
               {
                  (true, true) => EdgeStatus.None,
                  (false, false) => EdgeStatus.None,
                  (true, false) => EdgeStatus.Exiting,
                  (false, true) => EdgeStatus.Entering,
               };

               if (newStatus != status && newStatus != EdgeStatus.None)
               {
                  edges++;
               }
               status = newStatus;
            }
         }

         return edges;
      }

      private static IEnumerable<IEnumerable<(Coordinate A, Coordinate B)>> GetLinePairs(string[] lines)
      {
         foreach (var orientation in new List<string[]>() { lines, Rotate(lines) })
         {
            for (int i = -1; i < orientation.Length; i++)
            {
               yield return Enumerable.Range(0, orientation[0].Length).Select(j => (new Coordinate(i, j), new Coordinate(i + 1, j)));
            }
         }
      }

      private static string[] Rotate(string[] lines)
      {
         var rotated = new string[lines[0].Length];

         for (int i = 0; i < lines[0].Length; i++)
         {
            var row = new char[lines.Length];
            for (int j = 0; j < lines.Length; j++)
            {
               row[j] = lines[j][i];
            }
            rotated[i] = new string(row);
         }

         return rotated;
      }

      private static Region FindRegion(Coordinate plot, HashSet<Coordinate> visited, string[] map)
      {
         if (visited.Contains(plot))
         {
            return new Region(0, 0, []);
         }

         visited.Add(plot);
         var region = new Region(1, 4, [plot]);
         var neighboringPlots = new[] { plot.North(), plot.South(), plot.East(), plot.West() };

         foreach (var neighboringPlot in neighboringPlots)
         {
            if (IsInBounds(neighboringPlot, map) && map[neighboringPlot.Row][neighboringPlot.Col] == map[plot.Row][plot.Col])
            {
               region = new Region(region.Area, region.Perimeter - 1, region.Plots);

               if (!visited.Contains(neighboringPlot))
               {
                  var remainingRegion = FindRegion(neighboringPlot, visited, map);
                  region = new Region(region.Area + remainingRegion.Area, region.Perimeter + remainingRegion.Perimeter, region.Plots.Union(remainingRegion.Plots).ToHashSet());
               }
            }
         }

         return region;
      }

      private static IEnumerable<Region> FindAllRegions(string[] map)
      {
         HashSet<Coordinate> visited = [];
         for (int i = 0; i < map.Length; i++)
         {
            for (int j = 0; j < map[i].Length; j++)
            {
               var region = FindRegion(new Coordinate(i, j), visited, map);
               if (region.Area > 0)
               {
                  yield return region;
               }
            }
         }
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

   public record struct Region(int Area, int Perimeter, HashSet<Coordinate> Plots);

   public enum EdgeStatus
   {
      None,
      Entering,
      Exiting
   }

}