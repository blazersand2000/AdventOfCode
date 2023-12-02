using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Aoc2022.Day10
{
   public class Day10 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Aoc2022/Day10/input.txt");

         Console.WriteLine("Part 1:");
         var sum = SumInterestingSignalStrengths(lines);
         Console.WriteLine(sum);

         Console.WriteLine("Part 2:");
         var rows = GetCrt(lines);
         PrintCrt(rows);
      }

      public static int SumInterestingSignalStrengths(string[] lines)
      {
         var sumInterestingSignalStrengths = 0;

         var interestingCycles = new List<int>
         {
            20, 60, 100, 140, 180, 220
         };

         foreach (var output in RunProgram(lines))
         {
            if (interestingCycles.Contains(output.Cycle))
            {
               sumInterestingSignalStrengths += output.RegisterX * output.Cycle;
            }

         }

         return sumInterestingSignalStrengths;
      }

      public static void PrintCrt(string[] rows)
      {
         foreach (var row in rows)
         {
            Console.WriteLine(row);
         }
      }

      public static string[] GetCrt(string[] lines)
      {
         var rowLength = 40;
         var rows = new List<string>();
         var row = new StringBuilder(string.Empty);

         foreach (var output in RunProgram(lines))
         {
            var crtDrawPosition = (output.Cycle - 1) % rowLength;
            var spritePositions = new int[] { output.RegisterX - 1, output.RegisterX, output.RegisterX + 1 };

            row.Append(spritePositions.Contains(crtDrawPosition) ? '#' : '.');

            if (crtDrawPosition == rowLength - 1)
            {
               rows.Add(row.ToString());
               row.Clear();
            }
         }

         return rows.ToArray();
      }

      private static IEnumerable<ProgramOutput> RunProgram(string[] lines)
      {
         var instructions = new Queue<(Command Command, int? Argument)>(lines.Select(line => ParseInstruction(line)));
         var cycle = 0;
         var x = 1;
         (Command Command, int? Argument) currentInstruction = (Command.noop, null);
         var instructionCyclesRemaining = 0;
         while (instructions.Any())
         {
            cycle++;

            yield return new ProgramOutput(x, cycle);

            if (instructionCyclesRemaining == 0)
            {
               // Execute instruction
               currentInstruction = instructions.Dequeue();
               switch (currentInstruction.Command)
               {
                  case Command.addx:
                     instructionCyclesRemaining = 2;
                     break;
                  case Command.noop:
                     instructionCyclesRemaining = 1;
                     break;
                  default:
                     throw new ApplicationException("Unrecognized instruction.");
               }
            }

            instructionCyclesRemaining = Math.Max(instructionCyclesRemaining - 1, 0);

            // Process completed instruction
            if (instructionCyclesRemaining == 0 && currentInstruction.Command == Command.addx)
            {
               x += currentInstruction.Argument ?? 0;
            }
         }
      }

      private static (Command Command, int? Argument) ParseInstruction(string line)
      {
         var parts = line.Split(' ');
         var instruction = Enum.Parse<Command>(parts[0]);
         int? argument = parts.Length > 1 ? int.Parse(parts[1]) : default;

         return (instruction, argument);
      }

      private struct ProgramOutput
      {
         public int RegisterX { get; }
         public int Cycle { get; }

         public ProgramOutput(int registerX, int cycle)
         {
            RegisterX = registerX;
            Cycle = cycle;
         }
      }

      enum Command
      {
         addx,
         noop
      }
   }
}

