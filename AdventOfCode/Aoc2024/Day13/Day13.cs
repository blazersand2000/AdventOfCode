using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day13
{
   public class Day13 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetFewestTokensSpentToWinAllPrizes(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetFewestTokensSpentToWinAllPrizes(lines, 10_000_000_000_000));
      }

      public static long GetFewestTokensSpentToWinAllPrizes(string[] lines, long prizeOffset = 0L)
      {
         var clawMachines = ExtractClawMachines(lines);

         var tokens = clawMachines.Sum(clawMachine =>
         {
            var firstEquation = new LinearEquation(clawMachine.A.X, clawMachine.B.X, -(clawMachine.Prize.X + prizeOffset));
            var secondEquation = new LinearEquation(clawMachine.A.Y, clawMachine.B.Y, -(clawMachine.Prize.Y + prizeOffset));
            var (buttonAPushes, buttonBPushes) = SolveSystem(firstEquation, secondEquation);

            if (!IsInteger(buttonAPushes) || !IsInteger(buttonBPushes))
            {
               return 0;
            }

            return 3 * (long)buttonAPushes + 1 * (long)buttonBPushes;
         });

         return tokens;
      }

      public static (double X, double Y) SolveSystem(LinearEquation first, LinearEquation second)
      {
         var x = (double)(first.B * -second.C - second.B * -first.C) / (second.A * first.B - second.B * first.A);
         var y = (double)(first.A * -second.C - second.A * -first.C) / (second.B * first.A - second.A * first.B);

         return (x, y);
      }

      private static bool IsInteger(double value)
      {
         return value == Math.Floor(value);
      }

      private static List<ClawMachine> ExtractClawMachines(string[] lines)
      {
         return lines.Chunk(4).Select(chunk =>
         {
            var buttonA = extractButton(chunk[0]);
            var buttonB = extractButton(chunk[1]);
            var prize = extractPrize(chunk[2]);
            return new ClawMachine(buttonA, buttonB, prize);
         }).ToList();

         Button extractButton(string line)
         {
            var parts = line.Split();
            var x = long.Parse(parts[2][2..^1]);
            var y = long.Parse(parts[3][2..]);

            return new Button(x, y);
         }

         Prize extractPrize(string line)
         {
            var parts = line.Split();
            var x = long.Parse(parts[1][2..^1]);
            var y = long.Parse(parts[2][2..]);

            return new Prize(x, y);
         }
      }

   }

   /// <summary>
   /// ax + by + c = 0
   /// </summary>
   public record struct LinearEquation(long A, long B, long C);

   public record struct Button(long X, long Y);

   public record struct Prize(long X, long Y);

   public record struct ClawMachine(Button A, Button B, Prize Prize);
}