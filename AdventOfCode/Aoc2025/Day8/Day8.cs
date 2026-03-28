using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day8;

public class Day8 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(SizeOf3LargestCircuitsMultipliedTogether(lines, 1000));

      Console.WriteLine("Part 2:");
      Console.WriteLine(LastTwoJunctionBoxesXCoordinatesMultiplied(lines));
   }

   public static long SizeOf3LargestCircuitsMultipliedTogether(string[] lines, int numberOfConnections)
   {
      var points = ExtractPoints(lines);

      var pairs = GetPairsOrderedByDistance(points.ToList());

      var circuits = points.Select(p => new HashSet<Point> { p }).ToList();

      for (int i = 0; i < numberOfConnections; i++)
      {
         var pair = pairs[i];
         ConnectPair(circuits, pair);
      }

      var sizesOf3LargestCircuits = circuits.OrderByDescending(c => c.Count).Take(3).Select(c => c.Count).ToList();

      return sizesOf3LargestCircuits[0] * sizesOf3LargestCircuits[1] * sizesOf3LargestCircuits[2];
   }

   public static long LastTwoJunctionBoxesXCoordinatesMultiplied(string[] lines)
   {
      var points = ExtractPoints(lines);

      List<Pair> pairs = GetPairsOrderedByDistance(points.ToList());

      var circuits = points.Select(p => new HashSet<Point> { p }).ToList();

      foreach (var pair in pairs)
      {
         ConnectPair(circuits, pair);

         if (circuits.Count == 1)
         {
            return pair.A.X * pair.B.X;
         }
      }

      throw new InvalidOperationException("Somehow we connected all the junction boxes together and are not on one circuit. Impossible!");
   }

   private static HashSet<Point> ExtractPoints(string[] lines)
   {
      return lines.Select(l =>
      {
         var splits = l.Split(',').Select(s => long.Parse(s)).ToList();
         return new Point(splits[0], splits[1], splits[2]);
      }).ToHashSet();
   }

   private static List<Pair> GetPairsOrderedByDistance(List<Point> points)
   {
      var pairs = new List<Pair>();

      for (int i = 0; i < points.Count; i++)
      {
         for (int j = i + 1; j < points.Count; j++)
         {
            var a = points[i];
            var b = points[j];
            var pair = new Pair(a, b);
            pairs.Add(pair);
         }
      }

      pairs = pairs.OrderBy(p => p.Distance()).ToList();
      return pairs;
   }

   private static void ConnectPair(List<HashSet<Point>> circuits, Pair pair)
   {
      var circuitContainingA = circuits.Single(hs => hs.Contains(pair.A));
      var circuitContainingB = circuits.Single(hs => hs.Contains(pair.B));

      if (circuitContainingA == circuitContainingB)
      {
         return;
      }

      var mergedCircuit = circuitContainingA.Union(circuitContainingB).ToHashSet();
      circuits.Remove(circuitContainingA);
      circuits.Remove(circuitContainingB);
      circuits.Add(mergedCircuit);
   }

   public readonly record struct Point(long X, long Y, long Z);

   public readonly record struct Pair(Point A, Point B)
   {
      public Pair Reversed => new Pair(B, A);
      public double Distance()
      {
         var dx = B.X - A.X;
         var dy = B.Y - A.Y;
         var dz = B.Z - A.Z;

         return Math.Sqrt(dx * dx + dy * dy + dz * dz);
      }
   };
}
