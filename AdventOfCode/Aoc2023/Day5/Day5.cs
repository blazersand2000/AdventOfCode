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
         var seedsRanges = pairs.Select(p => new Range(p.First(), p.Last()));
         var seedNumberOfLowestLocation = GetPrevious(new Range(0, long.MaxValue), almanac.Maps, seedsRanges);
         return GetDestination(almanac.Maps, seedNumberOfLowestLocation);
      }

      public static long GetPrevious(Range range, IEnumerable<Map> maps, IEnumerable<Range> seedRanges)
      {
         if (!maps.Any())
         {
            var overlappingSeedRanges = seedRanges.Where(sr => sr.Overlaps(range));
            if (!overlappingSeedRanges.Any())
            {
               return long.MaxValue;
            }
            var seedRange = overlappingSeedRanges.MinBy(sr => sr.First);
            return Math.Max(seedRange.First, range.First);
         }
         var map = maps.Last();
         var untrimmedRanges = map.GetMapRangesOrderedByDestination(maps.Count() > 1).Where(mr => mr.Destination.Overlaps(range));
         if (untrimmedRanges.Any())
         {
            var shrinkFirstBy = range.First - untrimmedRanges.First().Destination.First;
            var shrinkLastBy = untrimmedRanges.Last().Destination.Last - range.Last;
            if (shrinkFirstBy > 0)
            {
               var first = untrimmedRanges.First();
               untrimmedRanges = new List<MapRange> { new(first.Destination.First + shrinkFirstBy, first.Source.First + shrinkFirstBy, first.Destination.RangeLength - shrinkFirstBy) }.Concat(untrimmedRanges.Skip(1));
            }
            if (shrinkLastBy > 0)
            {
               var last = untrimmedRanges.Last();
               untrimmedRanges = untrimmedRanges.Take(untrimmedRanges.Count() - 1).Append(new MapRange(last.Destination.First, last.Source.First, last.Destination.RangeLength - shrinkLastBy));
            }

         }
         var trimmedRanges = untrimmedRanges;

         var previousRanges = trimmedRanges.Select(mr => mr.Source);
         foreach (var previousRange in previousRanges)
         {
            var value = GetPrevious(previousRange, maps.SkipLast(1), seedRanges);
            if (value != long.MaxValue)
            {
               return value;
            }
         }
         return long.MaxValue;
      }

      // public static long GetLowestLocationImproved(IEnumerable<Map> maps, IEnumerable<Range> seedRanges)
      // {
      //    if (maps.Count() == 1)
      //    {
      //       var map = maps.First();
      //       foreach (var mapRange in map.MapRangesOrderedByDestination)
      //       {
      //          var seedRange = seedRanges.First(sr => sr.RangeStart >= mapRange.DestinationRangeStart && sr.RangeStart < mapRange.DestinationRangeStart + mapRange.RangeLength);
      //          if (seedRange.RangeStart + seedRange.RangeLength > mapRange.DestinationRangeStart + mapRange.RangeLength)
      //          {
      //             return mapRange.DestinationRangeStart + mapRange.RangeLength;
      //          }
      //       }
      //       return seedRanges.Min(sr => GetDestination(maps, sr.RangeStart));
      //    }
      //    else
      //    {
      //       var mapRanges = maps.First().MapRangesOrderedByDestination;
      //       var newSeedRanges = seedRanges.Select(sr => new SeedRange(GetDestination(maps, sr.RangeStart), sr.RangeLength));
      //       return GetLowestLocationImproved(maps.Skip(1), newSeedRanges);
      //    }
      //    {
      //       return seedRanges.Min(sr => sr.RangeStart);
      //    }
      // }

      // public static long FindMinDestinationInSourceRanges(MapRange mapRange, IEnumerable<Range> seedRanges)
      // {
      //    var min = long.MaxValue;
      //    var overlappingSeedRanges = seedRanges.Where(sr => sr.Overlaps(mapRange.Source));
      //    if (!overlappingSeedRanges.Any())
      //    {
      //       return min;
      //    }
      //    var seedRange = overlappingSeedRanges.MinBy(sr => sr.First);
      //    return Math.Max(seedRange.First, mapRange.Source.First);
      // }

      // public static IEnumerable<long> LongRange(long start, long count)
      // {
      //    for (long i = start; i < start + count; i++)
      //    {
      //       yield return i;
      //    }
      // }

      private static long GetLowestLocation(IEnumerable<Map> maps, IEnumerable<long> seeds)
      {
         return seeds.Min(seed => GetDestination(maps, seed));
      }

      private static long GetDestination(IEnumerable<Map> maps, long source)
      {
         if (!maps.Any())
         {
            return source;
         }

         var mapRanges = maps.First();
         var destination = GetMapping(source, mapRanges.GetMapRangesOrderedByDestination());
         return GetDestination(maps.Skip(1), destination);
      }

      private static long GetMapping(long source, IEnumerable<MapRange> mapRanges)
      {
         var mapRange = mapRanges.First(mr => source >= mr.Source.First && source < mr.Source.First + mr.Source.RangeLength);
         // if (mapRange == default)
         // {
         //    return source;
         // }
         return mapRange.Destination.First + (source - mapRange.Source.First);
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

         return new(seeds, maps.Select(m => new Map(m)));
      }

      public readonly record struct Almanac(IEnumerable<long> Seeds, IEnumerable<Map> Maps);

      public readonly struct Map
      {
         private readonly IEnumerable<MapRange> _mapRanges;

         public Map(IEnumerable<MapRange> mapRanges)
         {
            _mapRanges = mapRanges.OrderBy(mr => mr.Destination.First);
         }

         //Generates map ranges from destination to source for all positive longs, ordered by destination. All gaps between map ranges provided in the constructor are filled in.
         public IEnumerable<MapRange> GetMapRangesOrderedByDestination(bool fillInGaps = true) => fillInGaps ? GetAllMapRanges() : _mapRanges;

         // public IEnumerable<MapRange> GenerateMappingsForRange(Range range)
         // {
         //    foreach (var mapRange in _mapRanges.Where(mr => mr.Destination.Overlaps(range)))
         //    {

         //       if (mapRange.Destination.First > nextRangeStart)
         //       {
         //          yield return new(nextRangeStart, nextRangeStart, mapRange.Destination.First - nextRangeStart);
         //       }
         //       else if (mapRange.Destination.First < nextRangeStart)
         //       {
         //          yield return new(nextRangeStart, nextRangeStart, nextRangeStart - mapRange.Destination.First);
         //       }
         //       yield return mapRange;
         //       nextRangeStart = mapRange.Destination.Last + 1;
         //    }
         //    yield return new(nextRangeStart, nextRangeStart, long.MaxValue - nextRangeStart);
         // }

         private IEnumerable<MapRange> GetAllMapRanges()
         {
            var nextRangeStart = 0L;
            foreach (var mapRange in _mapRanges)
            {
               if (mapRange.Destination.First > nextRangeStart)
               {
                  yield return new(nextRangeStart, nextRangeStart, mapRange.Destination.First - nextRangeStart);
               }
               yield return mapRange;
               nextRangeStart = mapRange.Destination.Last + 1;
            }
            yield return new(nextRangeStart, nextRangeStart, long.MaxValue - nextRangeStart);
         }
      }

      public readonly struct MapRange
      {
         public Range Source { get; }
         public Range Destination { get; }

         public MapRange(long destinationRangeStart, long sourceRangeStart, long rangeLength)
         {
            Source = new(sourceRangeStart, rangeLength);
            Destination = new(destinationRangeStart, rangeLength);
         }
      }

      public readonly struct Range
      {
         public long First { get; }
         public long Last { get; }
         public long RangeLength { get; }

         public Range(long rangeStart, long rangeLength)
         {
            First = rangeStart;
            Last = rangeStart + rangeLength - 1;
            RangeLength = rangeLength;
         }

         public bool Overlaps(Range other)
         {
            return First >= other.First && First <= other.Last
               || Last >= other.First && Last <= other.Last
               || First < other.First && Last > other.Last;
         }
      }
   }
}
