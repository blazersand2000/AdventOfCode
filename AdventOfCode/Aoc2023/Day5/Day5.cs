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

         var part1 = GetLowestLocationForIndividualSeeds(almanac);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var part2 = GetLowestLocationForSeedRanges(almanac);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long GetLowestLocationForIndividualSeeds(Almanac almanac)
      {
         return GetLowestLocation(almanac.Maps, almanac.Seeds);
      }

      public static long GetLowestLocationForSeedRanges(Almanac almanac)
      {
         var pairs = almanac.Seeds
            .Select((value, index) => (value, index))
            .GroupBy(x => x.index / 2)
            .Select(g => g.Select(x => x.value));
         var seeds = pairs.SelectMany(pair => LongRange(pair.First(), pair.Last()));
         return GetLowestLocation(almanac.Maps, seeds);
      }

      public static IEnumerable<long> LongRange(long start, long count)
      {
         for (long i = start; i < start + count; i++)
         {
            yield return i;
         }
      }

      private static long GetLowestLocation(IEnumerable<IEnumerable<MapRange>> maps, IEnumerable<long> seeds)
      {
         return seeds.Min(seed => GetDestination(maps, seed));
      }

      private static long GetDestination(IEnumerable<IEnumerable<MapRange>> maps, long source)
      {
         if (!maps.Any())
         {
            return source;
         }

         var mapRanges = maps.First();
         var destination = GetMapping(source, mapRanges);
         return GetDestination(maps.Skip(1), destination);
      }

      private static long GetMapping(long source, IEnumerable<MapRange> mapRanges)
      {
         var mapRange = mapRanges.FirstOrDefault(mr => source >= mr.SourceRangeStart && source < mr.SourceRangeStart + mr.RangeLength);
         if (mapRange == default)
         {
            return source;
         }
         return mapRange.DestinationRangeStart + (source - mapRange.SourceRangeStart);
      }

      public static Almanac ParseAlmanac(string[] lines)
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

      public readonly record struct Almanac(IEnumerable<long> Seeds, IEnumerable<IEnumerable<MapRange>> Maps);

      public readonly record struct MapRange(long DestinationRangeStart, long SourceRangeStart, long RangeLength);
   }
}
