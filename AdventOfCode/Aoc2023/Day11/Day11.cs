using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2023.Day11
{
   public class Day11 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();

         var part1 = Part1(lines);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var part2 = Part2(lines);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long Part1(string[] lines)
      {
         var universe = ExpandUniverse(lines);
         var galaxies = ParseGalaxies(universe);
         return GetSumOfDistancesBetweenAllPairsOfGalaxies(galaxies);
      }

      public static long Part2(string[] lines, long expansionFactor = 1_000_000)
      {
         var galaxies = ExpandUniversePlusPlus(lines, expansionFactor);
         return GetSumOfDistancesBetweenAllPairsOfGalaxies(galaxies);
      }

      private static long GetSumOfDistancesBetweenAllPairsOfGalaxies(List<Galaxy> galaxies)
      {
         long sum = 0;
         for (var i = 0; i < galaxies.Count - 1; i++)
         {
            for (var j = i + 1; j < galaxies.Count; j++)
            {
               sum += Galaxy.DistanceBetween(galaxies[i], galaxies[j]);
            }
         }

         return sum;
      }

      private static List<Galaxy> ParseGalaxies(IEnumerable<IEnumerable<char>> universe)
      {
         var galaxies = new List<Galaxy>();
         for (var i = 0; i < universe.Count(); i++)
         {
            for (var j = 0; j < universe.ElementAt(i).Count(); j++)
            {
               if (universe.ElementAt(i).ElementAt(j) != '.')
               {
                  galaxies.Add((i, j));
               }
            }
         }

         return galaxies;
      }

      public static IEnumerable<IEnumerable<char>> ExpandUniverse(string[] lines)
      {
         var universe = new List<List<char>>();
         for (var i = 0; i < lines.Length; i++)
         {
            var currentRow = new List<char>();
            for (var j = 0; j < lines[i].Length; j++)
            {
               currentRow.Add(lines[i][j]);
               if (!AnyGalaxiesInColumn(j, lines))
               {
                  currentRow.Add('.');
               }
            }
            universe.Add(currentRow);
            if (!AnyGalaxiesInRow(i, lines))
            {
               universe.Add(currentRow);
            }
         }

         return universe;
      }

      private static bool AnyGalaxiesInColumn(int column, string[] lines)
      {
         return lines.Any(row => row[column] != '.');
      }

      private static bool AnyGalaxiesInRow(int row, string[] lines)
      {
         return lines[row].Any(c => c != '.');
      }

      public static List<Galaxy> ExpandUniversePlusPlus(string[] lines, long expansionFactor)
      {
         var expansionNumber = expansionFactor - 1;
         List<Galaxy> galaxies = new();
         long rowOffset = 0;
         for (var i = 0; i < lines.Length; i++)
         {
            long columnOffset = 0;
            for (var j = 0; j < lines[i].Length; j++)
            {
               if (lines[i][j] != '.')
               {
                  galaxies.Add((i + rowOffset, j + columnOffset));
               }
               else if (!AnyGalaxiesInColumn(j, lines))
               {
                  columnOffset += expansionNumber;
               }
            }
            if (!AnyGalaxiesInRow(i, lines))
            {
               rowOffset += expansionNumber;
            }
         }

         return galaxies;
      }

      public record Galaxy(long X, long Y)
      {
         public static implicit operator Galaxy((long X, long Y) tuple) => new(tuple.X, tuple.Y);
         public static implicit operator (long X, long Y)(Galaxy galaxy) => (galaxy.X, galaxy.Y);
         public static long DistanceBetween(Galaxy a, Galaxy b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
      }
   }
}
