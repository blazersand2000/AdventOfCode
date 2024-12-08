using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day7
{
   public class Day7 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetCalibrationResult(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetCalibrationResultIncludingConcatenation(lines));
      }

      public static long GetCalibrationResult(string[] lines)
      {
         var equations = ParseEquations(lines);
         return equations.Where(e => CanBeMadeTrue(e, [Add, Multiply])).Sum(e => e.TestValue);
      }

      public static long GetCalibrationResultIncludingConcatenation(string[] lines)
      {
         var equations = ParseEquations(lines);
         return equations.Where(e => CanBeMadeTrue(e, [Add, Multiply, Concatenate])).Sum(e => e.TestValue);
      }

      private static bool CanBeMadeTrue(Equation equation, List<Func<long, long, long>> operations)
      {
         return NumberOfWaysCanBeMadeTrue(equation, operations) > 0;
      }

      private static int NumberOfWaysCanBeMadeTrue(Equation equation, List<Func<long, long, long>> operations)
      {
         if (equation.Values[0] > equation.TestValue)
         {
            return 0;
         }
         if (equation.Values.Count == 1)
         {
            return equation.Values[0] == equation.TestValue ? 1 : 0;
         }

         return operations.Sum(operation =>
         {
            var result = operation(equation.Values[0], equation.Values[1]);
            var nextEquation = new Equation(equation.TestValue, [result, .. equation.Values[2..]]);

            return NumberOfWaysCanBeMadeTrue(nextEquation, operations);
         });
      }

      private static long Add(long a, long b) => a + b;

      private static long Multiply(long a, long b) => a * b;

      private static long Concatenate(long a, long b) => long.Parse($"{a}{b}");

      private static List<Equation> ParseEquations(string[] lines)
      {
         return lines.Select(line =>
         {
            var parts = line.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(part => long.Parse(part))
               .ToList();
            return new Equation(parts[0], parts[1..]);
         }).ToList();
      }

      public record Equation(long TestValue, List<long> Values);
   }
}
