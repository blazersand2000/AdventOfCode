using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day1
{
   public class Day1 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetTotalDistance(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetSimilarityScore(lines));
      }

      public static int GetTotalDistance(string[] lines)
      {
         var (listA, listB) = ExtractLists(lines);
         var sortedListA = listA.OrderBy(i => i).ToList();
         var sortedListB = listB.OrderBy(i => i).ToList();

         return sortedListA.Zip(sortedListB, (a, b) => Math.Abs(a - b)).Sum();
      }

      public static int GetSimilarityScore(string[] lines)
      {
         var (listA, listB) = ExtractLists(lines);
         var listBFrequencies = listB.GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());

         return listA.Sum(i => listBFrequencies.TryGetValue(i, out var count) ? i * count : 0);
      }

      private static (List<int> ListA, List<int> ListB) ExtractLists(string[] lines)
      {
         var listA = new List<int>();
         var listB = new List<int>();
         foreach (var line in lines)
         {
            var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            listA.Add(int.Parse(split[0]));
            listB.Add(int.Parse(split[1]));
         }

         return (listA, listB);
      }
   }
}
