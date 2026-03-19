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

      // Console.WriteLine("Part 2:");
      // Console.WriteLine(GetCountOfIngredientsConsideredToBeFresh(lines));
   }

   public static long GetGrandTotal(string[] lines)
   {
      var problems = ExtractProblems(lines);
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
