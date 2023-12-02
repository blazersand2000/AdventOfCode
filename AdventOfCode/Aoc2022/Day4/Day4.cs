using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Aoc2022.Day4
{
   public class Day4 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Aoc2022/Day4/input.txt");

         Part1(lines);
         Part2(lines);
      }

      private void Part1(string[] lines)
      {
         var numFullyContained = 0;

         foreach (var line in lines)
         {
            var ranges = GetRanges(line);
            if (AnyFullyContained(ranges))
            {
               numFullyContained++;
            }
         }

         Console.WriteLine("Part 1:");
         Console.WriteLine(numFullyContained);
      }

      private void Part2(string[] lines)
      {
         var numOverlap = 0;

         foreach (var line in lines)
         {
            var ranges = GetRanges(line);
            if (AnyOverlap(ranges) || AnyFullyContained(ranges))
            {
               numOverlap++;
            }
         }

         Console.WriteLine("Part 2:");
         Console.WriteLine(numOverlap);
      }

      private int[][] GetRanges(string line)
      {
         return line.Split(',').Select(r => r.Split('-').ToArray().Select(n => int.Parse(n)).ToArray()).ToArray();
      }

      private bool AnyFullyContained(int[][] ranges)
      {
         var rangeA = ranges[0];
         var rangeB = ranges[1];

         if (rangeA[0] <= rangeB[0] && rangeA[1] >= rangeB[1])
         {
            return true;
         }
         if (rangeB[0] <= rangeA[0] && rangeB[1] >= rangeA[1])
         {
            return true;
         }

         return false;
      }

      private bool AnyOverlap(int[][] ranges)
      {
         var rangeA = ranges[0];
         var rangeB = ranges[1];

         if (rangeA[0] <= rangeB[1] && rangeB[1] <= rangeA[1])
         {
            return true;
         }
         if (rangeA[0] <= rangeB[0] && rangeB[0] <= rangeA[1])
         {
            return true;
         }

         return false;
      }
   }
}

