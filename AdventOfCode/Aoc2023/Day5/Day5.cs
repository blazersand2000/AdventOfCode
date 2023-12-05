using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day5
{
   public class Day5 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();
         var almanac = ParseAlmanac(lines);

         Console.WriteLine("Part 1:");

         //Console.WriteLine("Part 2:");
      }

      private static Almanac ParseAlmanac(string[] lines)
      {
         IEnumerable<long> seeds = lines[0].Split(':', StringSplitOptions.TrimEntries)[1]
            .Split(' ').Select(long.Parse);
         List<List<MapRange>> maps = new();
         foreach (var line in lines.Skip(2))
         {
            if (line.EndsWith(':'))
            {
               maps.Add(new());
            }
            else if (!string.IsNullOrEmpty(line))
            {
               var mapRanges = maps[^1];
               var parts = line.Split(' ');
               mapRanges.Add(new(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }
         }

         return new(seeds, maps);
      }

      private readonly record struct Almanac(IEnumerable<long> Seeds, IEnumerable<IEnumerable<MapRange>> Maps);

      private readonly record struct MapRange(long DestinationRangeStart, long SourceRangeStart, long RangeLength);
   }
}
