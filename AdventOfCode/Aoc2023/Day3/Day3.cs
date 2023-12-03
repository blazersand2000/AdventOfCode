using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Aoc2023.Day3
{
   public class Day3 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();
         var schematic = ParseSchematic(lines);
         var sum = GetSumOfPartNumbers(schematic);

         Console.WriteLine("Part 1:");
         Console.WriteLine(sum);
      }

      public static long GetSumOfPartNumbers(char[][] schematic)
      {
         var sum = 0;
         var currentNumber = string.Empty;
         for (int i = 0; i < schematic.Length; i++)
         {
            for (int j = 0; j < schematic[i].Length; j++)
            {
               if (char.IsDigit(schematic[i][j]))
               {
                  currentNumber += schematic[i][j];
               }
               if (WeAreOnTheLastDigitOfANumber(schematic, currentNumber, i, j))
               {
                  sum += GetPartNumber(schematic, i, j - currentNumber.Length + 1, currentNumber);
                  currentNumber = string.Empty;
               }
            }
         }

         return sum;

         static bool WeAreOnTheLastDigitOfANumber(char[][] schematic, string currentNumber, int i, int j)
         {
            // true if we are at the end of the line or the next character is not a digit
            return currentNumber.Length > 0 && (j == schematic[i].Length - 1 || !char.IsDigit(schematic[i][j + 1]));
         }
      }

      private static int GetPartNumber(char[][] schematic, int i, int j, string number)
      {
         IEnumerable<(int i, int j)> inBoundsAdjacentCells = GetAdjacentCells(schematic, i, j, number);

         var isPartNumber = inBoundsAdjacentCells.Any(c => IsSymbol(schematic[c.i][c.j]));

         return isPartNumber ? int.Parse(number) : 0;
      }

      private static IEnumerable<(int i, int j)> GetAdjacentCells(char[][] schematic, int i, int j, string number)
      {

         var adjacentCells = new List<(int i, int j)>();
         // add cells directly above and below the number
         for (int k = 0; k < number.Length; k++)
         {
            adjacentCells.Add((i - 1, j + k));
            adjacentCells.Add((i + 1, j + k));
         }
         // add cells directly to the left, right, and diagonal to the number
         for (int k = 0; k < 3; k++)
         {
            adjacentCells.Add((i - 1 + k, j - 1));
            adjacentCells.Add((i - 1 + k, j + number.Length));
         }

         var inBoundsAdjacentCells = adjacentCells.Where(c => c.i >= 0 && c.i < schematic.Length && c.j >= 0 && c.j < schematic[i].Length);
         return inBoundsAdjacentCells;
      }


      private static bool IsSymbol(char c)
      {
         return !Regex.IsMatch(c.ToString(), @"^[a-zA-Z0-9\.]$");
      }

      public static char[][] ParseSchematic(string[] lines)
      {
         char[][] schematic = new char[lines.Length][];
         for (int i = 0; i < lines.Length; i++)
         {
            schematic[i] = lines[i].ToCharArray();
         }

         return schematic;
      }

   }
}
