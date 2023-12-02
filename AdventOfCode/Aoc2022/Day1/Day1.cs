using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Aoc2022.Day1
{
   public class Day1 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Aoc2022/Day1/input.txt");

         Console.WriteLine("Calories carried by elf carrying most calories:");
         Console.WriteLine(GetTopNCalories(lines, 1).Sum());

         Console.WriteLine("Calories carried by the 3 elves carrying most calories:");
         Console.WriteLine(GetTopNCalories(lines, 3).Sum());
      }

      private long[] GetTopNCalories(string[] lines, int n)
      {
         long currentSum = 0;
         long[] topCalories = new long[n];
         for (int i = 0; i < topCalories.Length; i++)
         {
            topCalories[i] = long.MinValue;
         }

         for (int i = 0; i < lines.Length; i++)
         {
            if (string.IsNullOrWhiteSpace(lines[i]) || i + 1 == lines.Length)
            {
               var topCaloriesMinValueIdx = Array.IndexOf(topCalories, topCalories.Min());
               topCalories[topCaloriesMinValueIdx] = Math.Max(topCalories[topCaloriesMinValueIdx], currentSum);
               currentSum = 0;
            }
            else
            {
               currentSum += long.Parse(lines[i]);
            }
         }

         return topCalories;
      }
   }
}
