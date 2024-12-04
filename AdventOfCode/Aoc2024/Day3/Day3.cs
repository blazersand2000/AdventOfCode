using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Aoc2024.Day3
{
   public class Day3 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSumOfMultiplicationResults(lines));

         // Console.WriteLine("Part 2:");
         // Console.WriteLine(GetNumberOfSafeReportsUsingDampener(lines));
      }

      public static int GetSumOfMultiplicationResults(string[] lines)
      {
         var corruptedMemory = string.Join(string.Empty, lines);
         var pattern = @"mul\(\d{1,3},\d{1,3}\)";
         var pairs = Regex.Matches(corruptedMemory, pattern).Select(x => x.Value[4..^1].Split([',']));
         return pairs.Sum(p => int.Parse(p[0]) * int.Parse(p[1]));
      }
   }
}
