using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2025.Day1;

public class Day1 : Problem
{
   private const int DIAL_LENGTH = 100;

   public override void Run()
   {
      string[] lines = ReadInputFile();

      Console.WriteLine("Part 1:");
      Console.WriteLine(GetPassword(lines));

      Console.WriteLine("Part 2:");
      Console.WriteLine(GetPasswordUsingMethod0x434C49434B(lines));
   }

   public static int GetPassword(string[] lines)
   {
      var rotations = GetRotations(lines);

      var numberOfTimesEndingAtZero = 0;
      uint current = 50;

      foreach (var rotation in rotations)
      {
         current = Rotate(current, rotation);
         if (current == 0)
         {
            numberOfTimesEndingAtZero++;
         }
      }

      return numberOfTimesEndingAtZero;
   }

   // There's gotta be a better way, but I gave up
   public static int GetPasswordUsingMethod0x434C49434B(string[] lines)
   {
      var rotations = GetRotations(lines);

      uint numberOfTimesPointingAtZero = 0;
      int current = 50;

      foreach (var rotation in rotations)
      {
         var clicks = Math.Abs(rotation);
         for (int i = 0; i < clicks; i++)
         {
            current += rotation < 0 ? -1 : 1;
            if (current == DIAL_LENGTH)
            {
               current = 0;
            }
            else if (current == -1)
            {
               current = DIAL_LENGTH - 1;
            }

            if (current == 0)
            {
               numberOfTimesPointingAtZero++;
            }
         }
      }

      return (int)numberOfTimesPointingAtZero;
   }

   private static IEnumerable<int> GetRotations(string[] lines)
   {
      return lines.Select(line => int.Parse(line[1..]) * (line[0] == 'L' ? -1 : 1));
   }

   private static uint Rotate(uint current, int rotation)
   {
      var rotatedPossiblyNegative = (current + rotation) % DIAL_LENGTH;
      return rotatedPossiblyNegative < 0 ? (uint)(rotatedPossiblyNegative + DIAL_LENGTH) : (uint)rotatedPossiblyNegative;
   }
}
