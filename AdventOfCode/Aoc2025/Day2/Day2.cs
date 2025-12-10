using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day2;

public class Day2 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(GetSumOfInvalidIds(lines[0]));

      Console.WriteLine("Part 2:");
      Console.WriteLine(GetSumOfInvalidIdsUsingNewRules(lines[0]));
   }

   public static long GetSumOfInvalidIds(string rawRanges)
   {
      var ranges = GetRanges(rawRanges);

      return ranges.SelectMany(GetInvalidIds).Sum();
   }

   public static long GetSumOfInvalidIdsUsingNewRules(string rawRanges)
   {
      var ranges = GetRanges(rawRanges);

      return ranges.SelectMany(GetInvalidIdsUsingNewRules).Sum();
   }

   private static IEnumerable<Range> GetRanges(string rawRanges)
   {
      return rawRanges.Split(',').Select(r => new Range(long.Parse(r.Split('-')[0]), long.Parse(r.Split('-')[1])));
   }

   private static IEnumerable<long> GetInvalidIdsUsingNewRules(Range range)
   {
      for (long i = range.First; i <= range.Last; i++)
      {
         if (IsInvalidUsingNewRules(i))
         {
            yield return i;
         }
      }
   }

   private static bool IsInvalidUsingNewRules(long current)
   {
      var numberOfDigits = (uint)current.ToString().Length;
      var sequenceSizes = Enumerable.Range(1, (int)numberOfDigits / 2).Where(s => numberOfDigits % s == 0);
      foreach (var size in sequenceSizes)
      {
         if (RepeatsSameSequenceOfSize(current, size))
         {
            return true;
         }
      }

      return false;
   }

   private static bool RepeatsSameSequenceOfSize(long current, int size)
   {
      var asString = current.ToString();
      var first = string.Concat(asString.Take(size));
      for (int i = size; i < asString.Length; i += size)
      {
         if (string.Concat(asString.Skip(i).Take(size)) != first)
         {
            return false;
         }
      }

      return true;
   }

   private static long[] GetInvalidIds(Range range)
   {
      long[] invalidIds = [];

      if (range.First > range.Last)
      {
         return invalidIds;
      }

      // Check for special case of range start being an invalid ID
      if (IsInvalid(range.First))
      {
         invalidIds = [.. invalidIds, range.First];
      }

      for (var next = GetNextInvalid(range.First); next <= range.Last; next = GetNextInvalid(next))
      {
         invalidIds = [.. invalidIds, next];
      }

      return invalidIds;
   }

   private static long GetNextInvalid(long current)
   {
      long left;

      var numberOfDigits = (uint)current.ToString().Length;
      if (numberOfDigits % 2 != 0)
      {
         left = GetLeft(Pow(10, numberOfDigits));
      }
      else if (GetRight(current) >= GetLeft(current))
      {
         left = GetLeft(current) + 1;
      }
      else
      {
         left = GetLeft(current);
      }

      return long.Parse($"{left}{left}");
   }

   private static long GetLeft(long current)
   {
      var asString = current.ToString();

      var left = asString[..(asString.Length / 2)];

      return long.Parse(left);
   }

   private static long GetRight(long current)
   {
      var asString = current.ToString();

      var right = asString[(asString.Length / 2)..];

      return long.Parse(right);
   }

   private static bool IsInvalid(long productId)
   {
      var asString = productId.ToString();

      if (!HasEvenNumberOfDigits(asString))
      {
         return false;
      }

      var numberOfDigits = asString.Length;
      if (asString[..(numberOfDigits / 2)] != asString[(numberOfDigits / 2)..])
      {
         return false;
      }

      return true;
   }

   private static bool HasEvenNumberOfDigits(string productId)
   {
      var numberOfDigits = (uint)productId.Length;
      if (numberOfDigits % 2 != 0)
      {
         return false;
      }

      return true;
   }

   private static long Pow(int @base, uint exponent)
   {
      long result = 1;

      for (int i = 0; i < exponent; i++)
      {
         result *= @base;
      }

      return result;
   }

   public record struct Range(long First, long Last);
}
