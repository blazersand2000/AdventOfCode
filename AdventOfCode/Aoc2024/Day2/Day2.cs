using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day2
{
   public class Day2 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetNumberOfSafeReports(lines));

         //Console.WriteLine("Part 2:");
         //Console.WriteLine(GetSimilarityScore(lines));
      }

      public static int GetNumberOfSafeReports(string[] lines)
      {
         var reports = lines.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)));

         return reports.Count(report =>
         {
            var direction = 0;
            foreach (var (a, b) in GetPairs(report.ToList()))
            {
               var difference = b - a;
               if (Math.Abs(difference) < 1 || Math.Abs(difference) > 3)
               {
                  return false;
               }
               if (direction == 0)
               {
                  direction = b - a;
                  continue;
               }
               if (direction < 0 && difference > 0)
               {
                  return false;
               }
               if (direction > 0 && difference < 0)
               {
                  return false;
               }
            }
            return true;
         });
      }

      public static IEnumerable<(int, int)> GetPairs(List<int> list)
      {
         for (int i = 0; i < list.Count() - 1; i++)
         {
            yield return (list[i], list[i + 1]);
         }
      }
   }
}
