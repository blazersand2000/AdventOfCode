using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Aoc2024.Day8
{
   public class Day8 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetNumberOfUniqueLocationsContainingAntinode(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetNumberOfUniqueLocationsContainingAntinodeTakingIntoAccountEffectsOfHarmonics(lines));
      }

      public static int GetNumberOfUniqueLocationsContainingAntinode(string[] lines)
      {
         var antennas = ExtractAntennas(lines);
         var antinodes = FindAntinodes(antennas, lines, FindAntinodesSimple);
         var uniqueAntinodeLocations = antinodes.Where(a => LocationInBounds(a, lines)).ToHashSet();

         return uniqueAntinodeLocations.Count;
      }

      public static int GetNumberOfUniqueLocationsContainingAntinodeTakingIntoAccountEffectsOfHarmonics(string[] lines)
      {
         var antennas = ExtractAntennas(lines);
         var antinodes = FindAntinodes(antennas, lines, FindAntinodesTakingIntoAccountEffectsOfHarmonics);
         var uniqueAntinodeLocations = antinodes.Where(a => LocationInBounds(a, lines)).ToHashSet();

         return uniqueAntinodeLocations.Count;
      }

      private static bool LocationInBounds(Coordinate location, string[] grid)
      {
         return location.Row >= 0 && location.Row < grid.Length && location.Col >= 0 && location.Col < grid[0].Length;
      }

      private static HashSet<Coordinate> FindAntinodes(HashSet<Antenna> antennas, string[] grid, Func<Antenna, Antenna, string[], HashSet<Coordinate>> antinodeFinder)
      {
         return antennas.GroupBy(antenna => antenna.Frequency)
            .SelectMany(group =>
            {
               var locations = group.Select(a => a.Location).ToHashSet();
               List<Coordinate> antinodes = [];
               for (int i = 0; i < locations.Count - 1; i++)
               {
                  for (int j = i + 1; j < locations.Count; j++)
                  {
                     antinodes.AddRange(antinodeFinder(group.ElementAt(i), group.ElementAt(j), grid));
                  }
               }
               return antinodes;
            }).ToHashSet();
      }

      private static HashSet<Coordinate> FindAntinodesSimple(Antenna a, Antenna b, string[] _)
      {
         var antinodeNearA = a.Location - (b.Location - a.Location);
         var antinodeNearB = b.Location + (b.Location - a.Location);

         return [antinodeNearA, antinodeNearB];
      }

      private static HashSet<Coordinate> FindAntinodesTakingIntoAccountEffectsOfHarmonics(Antenna a, Antenna b, string[] grid)
      {
         var difference = b.Location - a.Location;
         var gcd = (int)BigInteger.GreatestCommonDivisor(difference.Row, difference.Col);
         var offset = new Coordinate(difference.Row / gcd, difference.Col / gcd);
         HashSet<Coordinate> antinodes = [];
         var current = a.Location;
         while (LocationInBounds(current, grid))
         {
            antinodes.Add(current);
            current -= offset;
         }
         current = a.Location;
         while (LocationInBounds(current, grid))
         {
            antinodes.Add(current);
            current += offset;
         }

         return antinodes;
      }

      private static HashSet<Antenna> ExtractAntennas(string[] lines)
      {
         HashSet<Antenna> antennas = [];

         for (int i = 0; i < lines.Length; i++)
         {
            for (int j = 0; j < lines[0].Length; j++)
            {
               var value = lines[i][j];
               if (value != '.')
               {
                  antennas.Add(new Antenna(new Coordinate(i, j), value));
               }
            }
         }

         return antennas;
      }
   }

   public record struct Coordinate(int Row, int Col)
   {
      public static Coordinate operator +(Coordinate a, Coordinate b)
      {
         return new Coordinate(a.Row + b.Row, a.Col + b.Col);
      }

      public static Coordinate operator -(Coordinate a, Coordinate b)
      {
         return new Coordinate(a.Row - b.Row, a.Col - b.Col);
      }
   }

   public record struct Antenna(Coordinate Location, char Frequency);
}
