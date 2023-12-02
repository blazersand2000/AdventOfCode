using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day1
{
   public class Day1 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine(GetCalibrationValues(lines).Sum());
      }

      private static IEnumerable<long> GetCalibrationValues(string[] lines)
      {
         return lines.Select(line =>
         {
            var firstDigit = line.First(c => char.IsDigit(c));

            char lastDigit = char.MinValue;
            for (int i = line.Length - 1; i >= 0; i--)
            {
               if (char.IsDigit(line[i]))
               {
                  lastDigit = line[i];
                  break;
               }
            }

            return long.Parse(string.Concat(firstDigit, lastDigit));
         });
      }
   }
}
