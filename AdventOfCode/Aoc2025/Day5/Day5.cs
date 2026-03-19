using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day5;

public class Day5 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(GetCountOfAvailableFreshIngredients(lines));

      Console.WriteLine("Part 2:");
      Console.WriteLine(GetCountOfIngredientsConsideredToBeFresh(lines));
   }

   public static long GetCountOfAvailableFreshIngredients(string[] lines)
   {
      var freshIngredientRanges = GetRanges(lines);
      var availableIngredients = GetIngredientIds(lines);

      return availableIngredients.Count(ingredient => freshIngredientRanges.Any(range => RangeContains(range, ingredient)));
   }

   public static long GetCountOfIngredientsConsideredToBeFresh(string[] lines)
   {
      var freshIngredientRanges = GetRanges(lines);
      var distinctRanges = new List<Range>();

      foreach (var range in freshIngredientRanges)
      {
         var existingOverlaps = distinctRanges.Where(dr => RangesOverlap(range, dr)).ToList();
         var newCombinedRange = MergeRanges([range, .. existingOverlaps]);
         distinctRanges = distinctRanges.Except(existingOverlaps).ToList();
         distinctRanges.Add(newCombinedRange);
      }

      return distinctRanges.Sum(dr => dr.End - dr.Start + 1);
   }

   private static List<Range> GetRanges(string[] lines)
   {
      return lines.TakeWhile(line => line != string.Empty).Select(ParseRange).ToList();
   }

   private static List<long> GetIngredientIds(string[] lines)
   {
      return lines.SkipWhile(line => line.Contains('-') || line == string.Empty).Select(long.Parse).ToList();
   }

   private static Range ParseRange(string range)
   {
      var parts = range.Split('-');
      var start = long.Parse(parts[0]);
      var end = long.Parse(parts[1]);
      return new Range(start, end);
   }

   private static bool RangeContains(Range range, long value)
   {
      return value >= range.Start && value <= range.End;
   }

   public static bool RangesOverlap(Range a, Range b)
   {
      return a.End >= b.Start && a.Start <= b.End;
   }

   private static Range MergeRanges(List<Range> ranges)
   {
      var start = ranges.Min(r => r.Start);
      var end = ranges.Max(r => r.End);

      return new Range(start, end);
   }

   public readonly record struct Range(long Start, long End);
}
