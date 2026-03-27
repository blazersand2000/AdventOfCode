using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day6;

public class Day6 : Problem
{
   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(GetGrandTotal(lines));

      Console.WriteLine("Part 2:");
      Console.WriteLine(GetGrandTotalUsingCephalopodMath(lines));
   }

   public static long GetGrandTotal(string[] lines)
   {
      var problems = ExtractProblems(lines);
      return problems.Sum(p => SolveProblem(p.numbers, p.operation));
   }

   public static long GetGrandTotalUsingCephalopodMath(string[] lines)
   {
      var problems = ExtractProblemsUsingCephalopodMath(lines);
      return problems.Sum(p => SolveProblem(p.numbers, p.operation));
   }

   private static List<(List<long> numbers, string operation)> ExtractProblems(string[] lines)
   {
      var converted = lines.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();
      var problems = new List<List<string>>();

      for (int j = 0; j < converted[0].Length; j++)
      {
         List<string> problem = [];
         for (int i = 0; i < lines.Length; i++)
         {
            problem.Add(converted[i][j]);
         }
         problems.Add(problem);
      }

      return problems.Select(p => (p[..^1].Select(long.Parse).ToList(), p[^1])).ToList();
   }

   private static List<(List<long> numbers, string operation)> ExtractProblemsUsingCephalopodMath(string[] lines)
   {
      var rightToLeftColumns = new List<string>();

      for (int j = lines[0].Length - 1; j >= 0; j--)
      {
         char[] column = new char[lines.Length];
         for (int i = 0; i < lines.Length; i++)
         {
            column[i] = lines[i][j];
         }
         rightToLeftColumns.Add(new string(column));
      }

      var problems = new List<(List<long> numbers, string operation)>();
      var numbers = new List<long>();

      foreach (var column in rightToLeftColumns)
      {
         var trimmed = new string(column.Where(ch => ch != ' ').ToArray());
         if (trimmed.Length == 0)
         {
            continue;
         }

         if (trimmed[^1] == '+' || trimmed[^1] == '*')
         {
            numbers.Add(long.Parse(new string(trimmed[..^1].ToArray())));
            problems.Add((numbers, trimmed[^1].ToString()));
            numbers = new List<long>();
         }
         else
         {
            numbers.Add(long.Parse(trimmed));
         }
      }

      return problems;
   }

   private static long SolveProblem(List<long> numbers, string operation)
   {
      return operation switch
      {
         "+" => numbers.Sum(),
         "*" => numbers.Aggregate((acc, cur) => acc * cur),
         _ => throw new InvalidOperationException()
      };
   }

}
