using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day3;

public class Day3 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(GetTotalOutputJoltage(lines));

      // Console.WriteLine("Part 2:");
      // Console.WriteLine(GetSumOfInvalidIdsUsingNewRules(lines[0]));
   }

   public static long GetTotalOutputJoltage(string[] banks)
   {
      return banks.Sum(GetJoltage);
   }

   private static int GetJoltage(string bank)
   {
      var left = bank[0..^1].Max();
      var right = bank[(bank.IndexOf(left) + 1)..].Max();

      return int.Parse($"{left}{right}");
   }

}
