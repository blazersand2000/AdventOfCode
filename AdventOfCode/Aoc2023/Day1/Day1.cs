using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day1
{
   public class Day1 : Problem
   {
      private static Dictionary<string, char> spelledOutValues = new()
      {
         { "one", '1' },
         { "two", '2' },
         { "three", '3' },
         { "four", '4' },
         { "five", '5' },
         { "six", '6' },
         { "seven", '7' },
         { "eight", '8' },
         { "nine", '9' }
      };

      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetNumericCalibrationValues(lines).Sum());

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetNumericOrSpelledOutCalibrationValues(lines).Sum());
      }

      private static IEnumerable<long> GetNumericCalibrationValues(string[] lines)
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

      public static IEnumerable<long> GetNumericOrSpelledOutCalibrationValues(string[] lines)
      {

         return lines.Select(line =>
         {
            var firstDigit = char.MinValue;
            for (int i = 0; i < line.Length; i++)
            {
               if (char.IsDigit(line[i]))
               {
                  firstDigit = line[i];
                  break;
               }
               else if (DetectSpelledOutDigit(line, i, out char digit))
               {
                  firstDigit = digit;
                  break;
               }
            }

            char lastDigit = char.MinValue;
            for (int i = line.Length - 1; i >= 0; i--)
            {
               if (char.IsDigit(line[i]))
               {
                  lastDigit = line[i];
                  break;
               }
               else if (DetectSpelledOutDigit(line, i, out char digit, true))
               {
                  lastDigit = digit;
                  break;
               }
            }

            return long.Parse(string.Concat(firstDigit, lastDigit));
         });
      }

      // This might be improved by using something like the KMP string searching algorithm.
      // But for a small number of short strings, this brute force approach is probably fine for the purpose of Advent of Code.
      private static bool DetectSpelledOutDigit(string line, int startIndex, out char digit, bool reverse = false)
      {
         digit = char.MinValue;
         var i = startIndex;
         if (startIndex < 0 || startIndex >= line.Length)
         {
            return false;
         }

         // values that wouldn't go out of bounds given our starting index
         var candidateValues = spelledOutValues.Where(v => !reverse ? startIndex + v.Key.Length <= line.Length : startIndex + 1 - v.Key.Length >= 0);
         foreach (var spelledOutValue in candidateValues)
         {
            if ((!reverse && IsLeftToRightMatch(line, startIndex, spelledOutValue)) || (reverse && IsRightToLeftMatch(line, startIndex, spelledOutValue)))
            {
               digit = spelledOutValue.Value;
               return true;
            }
         }

         return false;

         static bool IsLeftToRightMatch(string line, int startIndex, KeyValuePair<string, char> spelledOutValue)
         {
            return line.Substring(startIndex, spelledOutValue.Key.Length) == spelledOutValue.Key;
         }

         static bool IsRightToLeftMatch(string line, int startIndex, KeyValuePair<string, char> spelledOutValue)
         {
            return line.Substring(startIndex + 1 - spelledOutValue.Key.Length, spelledOutValue.Key.Length) == spelledOutValue.Key;
         }
      }
   }
}
