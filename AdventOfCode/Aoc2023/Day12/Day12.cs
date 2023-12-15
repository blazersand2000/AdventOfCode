using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2023.Day12
{
   public partial class Day12 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();

         var part1 = Part1(lines);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         // var part2 = Part2(lines);
         // Console.WriteLine("Part 2:");
         // Console.WriteLine(part2);
      }

      public static int Part1(string[] lines)
      {
         var records = ParseRecords(lines);
         var possibleArrangements = records.Select(GetPossibleArrangements).ToArray();
         return possibleArrangements.Sum(a => a.Count);
      }

      public static IEnumerable<Record> ParseRecords(string[] lines)
      {
         return lines.Select(l =>
         {
            var parts = l.Split(" ");
            var springs = parts[0];
            var damagedGroups = parts[1].Split(",").Select(int.Parse).ToArray();
            return new Record(springs, damagedGroups);
         });
      }

      public static bool CombinationValidForDamagedRecord(string combination, string damagedRecord)
      {
         return combination.Zip(damagedRecord).All(x => x.First != '?' && (x.Second == '?' || x.First == x.Second));
      }

      public static bool UndamagedRecordIsValid(Record record)
      {
         var contiguousGroupsOfDamagedSprings = ContiguousGroupsOfDamagedSpringsRegex().Matches(record.Springs);
         var groupCounts = contiguousGroupsOfDamagedSprings.Select(m => m.Length).ToArray();
         return record.DamagedGroups.SequenceEqual(groupCounts);
      }

      public static List<Record> GetPossibleArrangements(Record record)
      {
         var possibleArrangements = new List<Record>();
         var damagedLocations = record.Springs.Select((s, i) => (s, i)).Where(x => x.s == '?').Select(t => t.i).ToArray();
         var numCominations = 1 << damagedLocations.Length;
         for (int i = 0; i < numCominations; i++)
         {
            var combination = i;
            var springs = record.Springs.ToCharArray();
            for (int j = 0; j < damagedLocations.Length; j++)
            {
               var damagedLocation = damagedLocations[j];
               var damaged = (combination & 1) == 1;
               if (damaged)
               {
                  springs[damagedLocation] = '#';
               }
               else
               {
                  springs[damagedLocation] = '.';
               }
               combination >>= 1;
            }
            var candidate = new Record(new string(springs), record.DamagedGroups);
            if (UndamagedRecordIsValid(candidate))
            {
               possibleArrangements.Add(candidate);
            }
         }

         return possibleArrangements;
      }

      private static Record UnfoldRecord(Record record)
      {
         var springs = string.Join('?', Enumerable.Repeat(record.Springs, 5));
         var damagedGroups = record.DamagedGroups.SelectMany(g => Enumerable.Repeat(g, 5));
         return new Record(springs, damagedGroups.ToArray());
      }

      public static long GetNumberOfPossibleArrangements(Record record)
      {
         if (record.DamagedGroups.Length == 0)
         {
            return 1;
         }

         var numPossibleArrangements = 0L;
         int groupSize = record.DamagedGroups.First();
         var remainingGroupSizes = record.DamagedGroups.Skip(1).ToArray();
         var endBuffer = remainingGroupSizes.Length == 0 ? 0
            : remainingGroupSizes.Sum() + remainingGroupSizes.Length;
         for (int i = 0; i < record.Springs.Length - endBuffer - (groupSize - 1); i++)
         {
            var candidate = record.Springs.Substring(i, groupSize);
            if (candidate.Any(c => c != '?' && c != '#'))
            {
               continue;
            }
            var firstIndexAfterCandidate = i + groupSize;
            if (firstIndexAfterCandidate < record.Springs.Length && !(new[] { '.', '?' }.Contains(record.Springs[firstIndexAfterCandidate])))
            {
               continue;
            }
            var lastIndexBeforeCandidate = i - 1;
            if (lastIndexBeforeCandidate >= 0 && !(new[] { '.', '?' }.Contains(record.Springs[lastIndexBeforeCandidate])))
            {
               continue;
            }
            var nextSprings = remainingGroupSizes.Length == 0 ? ""
               : record.Springs.Substring(firstIndexAfterCandidate + 1);
            var remaining = GetNumberOfPossibleArrangements(new Record(nextSprings, remainingGroupSizes));
            numPossibleArrangements += remaining;
         }
         return numPossibleArrangements;
      }

      public sealed record Record
      {
         public string Springs { get; init; }
         public int[] DamagedGroups { get; init; }

         public Record(string springs, int[] damagedGroups)
         {
            Springs = springs;
            DamagedGroups = damagedGroups;
         }

         public bool Equals(Record other)
         {
            return other != null &&
                   Springs == other.Springs &&
                   DamagedGroups.SequenceEqual(other.DamagedGroups);
         }

         public override int GetHashCode()
         {
            return HashCode.Combine(Springs, DamagedGroups);
         }
      }

      [GeneratedRegex("#+")]
      private static partial Regex ContiguousGroupsOfDamagedSpringsRegex();

   }
}
