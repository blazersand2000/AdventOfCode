using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2023.Day13
{
   public partial class Day13 : Problem
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
         return ParsePatterns(lines).Select(p => Summarize(p).Value).Sum();
      }

      public static long Part2(string[] lines)
      {
         return ParsePatterns(lines).Select(FixSmudgeThenSummarize).Sum();
      }

      public static int FixSmudgeThenSummarize(Pattern pattern)
      {
         var original = Summarize(pattern);
         for (var i = 0; i < pattern.Rows.Length; i++)
         {
            for (var j = 0; j < pattern.Rows[i].Length; j++)
            {
               var candidate = pattern.Clone();
               candidate.Set(i, j, pattern.Rows[i][j] == '#' ? '.' : '#');

               var horizontalSummary = Summarize(candidate, Orientation.Horizontal);
               if (horizontalSummary.Value > 0 && horizontalSummary != original)
               {
                  return horizontalSummary.Value;
               }
               var verticalSummary = Summarize(candidate, Orientation.Vertical);
               if (verticalSummary.Value > 0 && verticalSummary != original)
               {
                  return verticalSummary.Value;
               }
            }
         }
         return original.Value;
      }

      public static LineOfReflection Summarize(Pattern pattern)
      {
         var horizontal = Summarize(pattern, Orientation.Horizontal);
         if (horizontal.Value > 0)
         {
            return horizontal;
         }
         return Summarize(pattern, Orientation.Vertical);
      }

      public static LineOfReflection Summarize(Pattern pattern, Orientation orientation)
      {
         if (orientation == Orientation.Horizontal)
         {
            var rowReflectionIndex = FindIndexBeforeReflection(pattern.Rows);
            return new LineOfReflection(Orientation.Horizontal, 100 * (rowReflectionIndex + 1));
         }
         var columnReflectionIndex = FindIndexBeforeReflection(pattern.Columns);
         return new LineOfReflection(Orientation.Vertical, columnReflectionIndex + 1);
      }

      private static int FindIndexBeforeReflection(char[][] pattern)
      {
         for (var i = 0; i < pattern.Length - 1; i++)
         {
            if (HasReflectionBetween(i, i + 1, pattern))
            {
               return i;
            }
         }
         return -1;
      }

      private static bool HasReflectionBetween(int indexA, int indexB, char[][] pattern)
      {
         if (indexA >= indexB)
         {
            return false;
         }

         while (indexA >= 0 && indexB < pattern.Length)
         {
            if (!pattern[indexA].SequenceEqual(pattern[indexB]))
            {
               return false;
            }
            indexA--;
            indexB++;
         }
         return true;
      }

      private static IEnumerable<Pattern> ParsePatterns(string[] lines)
      {
         var patterns = new List<char[][]>();
         var pattern = new List<char[]>();
         foreach (var line in lines)
         {
            if (string.IsNullOrWhiteSpace(line))
            {
               patterns.Add(pattern.ToArray());
               pattern.Clear();
            }
            else
            {
               pattern.Add(line.ToCharArray());
            }
         }

         patterns.Add(pattern.ToArray());
         return patterns.Select(p => new Pattern(p));
      }

      public struct Pattern
      {
         private char[][] _pattern;

         public Pattern(char[][] pattern)
         {
            _pattern = pattern;
            Columns = Enumerable.Range(0, pattern.Max(row => row.Length))
                              .Select(i => pattern.Select(row => row.ElementAtOrDefault(i)).ToArray())
                              .ToArray();
         }

         public readonly char[][] Rows => _pattern;
         public readonly char[][] Columns { get; }

         public void Set(int row, int column, char value)
         {
            _pattern[row][column] = value;
         }

         public Pattern Clone()
         {
            char[][] copy = new char[_pattern.Length][];
            for (int i = 0; i < _pattern.Length; i++)
            {
               copy[i] = (char[])_pattern[i].Clone();
            }
            return new Pattern(copy);
         }
      }

      public record struct LineOfReflection(Orientation Orientation, int Value);

      public enum Orientation
      {
         Horizontal,
         Vertical
      }
   }
}
