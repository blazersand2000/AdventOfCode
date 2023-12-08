using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AdventOfCode.Aoc2023.Day6
{
   public class Day6 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();
         var races = ParseRaces(lines);

         var part1 = GetProductOfNumberOfWaysToBeatRecord(races);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var part2 = GetNumberOfWaysToBeatRecordSingleLongRace(races);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long GetProductOfNumberOfWaysToBeatRecord(IEnumerable<Race> races)
      {
         return races.Select(NumberOfWaysToBeatRecord).Aggregate((a, b) => a * b);
      }

      public static long GetNumberOfWaysToBeatRecordSingleLongRace(IEnumerable<Race> races)
      {
         var singleTime = long.Parse(string.Concat(races.Select(r => r.Time.ToString())));
         var singleDistance = long.Parse(string.Concat(races.Select(r => r.Distance.ToString())));
         var race = new Race(singleTime, singleDistance);

         return NumberOfWaysToBeatRecord(race);
      }

      private static int NumberOfWaysToBeatRecord(Race race)
      {
         var a = -1;
         var b = race.Time;
         // this will cause the roots to be equal to the first and last hold values that will break the record.
         var c = -race.Distance - 1;

         var roots = GetRoots(a, b, c);
         if (!roots.Any())
         {
            return 0;
         }
         if (roots.Count() == 1)
         {
            return 1;
         }
         var firstRecordHoldTime = (int)(roots.Min() % 1 == 0 ? roots.Min() : Math.Ceiling(roots.Min()));
         var lastRecordHoldTime = (int)(roots.Max() % 1 == 0 ? roots.Max() : Math.Floor(roots.Max()));
         var numberOfWaysToBeatRecord = lastRecordHoldTime - firstRecordHoldTime + 1;
         Console.WriteLine($"Time {race.Time}, Distance {race.Distance}, first record time {firstRecordHoldTime}, last record time {lastRecordHoldTime}, number of ways to beat record {numberOfWaysToBeatRecord}");
         return numberOfWaysToBeatRecord;
      }

      private static IEnumerable<double> GetRoots(long a, long b, long c)
      {
         var discriminant = b * b - 4 * a * c;
         if (discriminant < 0)
         {
            return Enumerable.Empty<double>();
         }
         if (discriminant == 0)
         {
            return new[] { (double)-b / (2 * a) };
         }
         var root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
         var root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
         return new[] { root1, root2 };
      }

      public static IEnumerable<Race> ParseRaces(string[] lines)
      {
         var ParseLine = (string line) => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
         var times = ParseLine(lines[0]);
         var distances = ParseLine(lines[1]);

         return times.Zip(distances, (time, distance) => new Race(time, distance));
      }

      public readonly record struct Race(long Time, long Distance);
   }
}
