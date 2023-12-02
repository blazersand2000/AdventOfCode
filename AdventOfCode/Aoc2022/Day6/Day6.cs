using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Aoc2022.Day6
{
   public class Day6 : IProblem
   {
      public void Run()
      {
         var input = File.ReadAllText("Problems/Day6/input.txt");

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetFirstMarkerCharacter(input, 4));
         Console.WriteLine("Part 2:");
         Console.WriteLine(GetFirstMarkerCharacter(input, 14));
      }

      public int GetFirstMarkerCharacter(string buffer, int numDistinctChars)
      {
         var counts = new Dictionary<char, int>();

         for (int i = 0; i < numDistinctChars; i++)
         {
            counts[buffer[i]] = counts.GetValueOrDefault(buffer[i]) + 1;
         }

         for (int i = numDistinctChars; i < buffer.Length; i++)
         {
            if (!AnyRepeats())
            {
               return i;
            }

            counts[buffer[i - numDistinctChars]]--;
            counts[buffer[i]] = counts.GetValueOrDefault(buffer[i]) + 1;
         }

         bool AnyRepeats()
         {
            return counts.Values.Any(v => v > 1);
         }

         throw new ApplicationException("Marker not found");
      }
   }
}

