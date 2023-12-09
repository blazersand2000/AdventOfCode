using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day9
{
   public class Day9 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();
         var histories = ParseHistories(lines);

         var part1 = GetSumOfExtrapolatedValues(histories);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var part2 = GetSumOfExtrapolatedValues(histories, true);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long GetSumOfExtrapolatedValues(IEnumerable<IEnumerable<long>> histories, bool backwards = false)
      {
         return histories.Select(h => ExtrapolateNextValue(h, backwards)).Sum();
      }

      public static long ExtrapolateNextValue(IEnumerable<long> history, bool backwards)
      {
         if (history.All(v => v == 0))
         {
            return 0;
         }
         var diffs = history.Skip(1).Zip(history, (a, b) => a - b);
         return backwards
            ? history.First() - ExtrapolateNextValue(diffs, backwards)
            : ExtrapolateNextValue(diffs, backwards) + history.Last();
      }

      public static IEnumerable<IEnumerable<long>> ParseHistories(string[] lines)
      {
         return lines.Select(line => line.Split(' ').Select(long.Parse));
      }
   }
}
