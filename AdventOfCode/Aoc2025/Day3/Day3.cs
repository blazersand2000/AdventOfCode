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
      Console.WriteLine(GetTotalOutputJoltage(lines, 2));

      Console.WriteLine("Part 2:");
      Console.WriteLine(GetTotalOutputJoltage(lines, 12));
   }

   public static long GetTotalOutputJoltage(string[] banks, int digits)
   {
      return banks.Sum(b => long.Parse(GetJoltage(b, digits)));
   }

   private static string GetJoltage(string bank, int digits)
   {
      if (digits > bank.Length || digits <= 0)
      {
         return string.Empty;
      }

      var firstDigitIndex = bank.IndexOf(bank[0..^(digits - 1)].Max());
      var firstDigit = bank[firstDigitIndex];
      var remainingDigits = GetJoltage(bank[(firstDigitIndex + 1)..], digits - 1);

      return $"{firstDigit}{remainingDigits}";
   }
}
