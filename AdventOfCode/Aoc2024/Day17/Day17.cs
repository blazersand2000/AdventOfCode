using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day17
{
   public class Day17 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetOutput(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetLowestRegisterAThatProducesCopyOfItself(lines));
      }

      public static long GetLowestRegisterAThatProducesCopyOfItself(string[] lines)
      {
         var computer = ExtractComputer(lines);

         for (long i = 0; true; i++)
         {
            computer = computer.SetARegister(i);
            if (ComputerProgramOutputsItself(computer))
            {
               return i;
            }
         }
      }

      public static string GetOutput(string[] lines)
      {
         var computer = ExtractComputer(lines);

         computer = RunProgram(computer);

         return string.Join(',', computer.OutputBuffer);
      }

      private static bool ComputerProgramOutputsItself(Computer computer)
      {
         while (computer.InstructionPointer < computer.Program.Length)
         {
            var opcode = computer.Program[computer.InstructionPointer];
            var operand = computer.Program[computer.InstructionPointer + 1];
            var instruction = GetInstruction(opcode);
            computer = instruction(computer, operand);

            if (computer.OutputBuffer.Length > computer.Program.Length)
            {
               return false;
            }
            if (computer.OutputBuffer.Zip(computer.Program).Any(pair => pair.First != pair.Second))
            {
               return false;
            }
         }

         return computer.OutputBuffer.SequenceEqual(computer.Program.Select(i => (long)i));
      }

      private static Computer RunProgram(Computer computer)
      {
         while (computer.InstructionPointer < computer.Program.Length)
         {
            var opcode = computer.Program[computer.InstructionPointer];
            var operand = computer.Program[computer.InstructionPointer + 1];
            var instruction = GetInstruction(opcode);
            computer = instruction(computer, operand);
         }

         return computer;
      }

      private static Func<Computer, int, Computer> GetInstruction(int opcode)
      {
         return opcode switch
         {
            0 => Adv,
            1 => Bxl,
            2 => Bst,
            3 => Jnz,
            4 => Bxc,
            5 => Out,
            6 => Bdv,
            7 => Cdv,
            _ => throw new InvalidOperationException("Invalid opcode")
         };
      }

      private static Computer Adv(Computer computer, int operand)
      {
         var result = computer.Registers.A / Math.Pow(2, Combo(operand, computer.Registers));
         return computer.SetARegister((long)result).AdvancePointer();
      }

      private static Computer Bxl(Computer computer, int operand)
      {
         var result = computer.Registers.B ^ operand;
         return computer.SetBRegister(result).AdvancePointer();
      }

      private static Computer Bst(Computer computer, int operand)
      {
         var result = Combo(operand, computer.Registers) % 8;
         return computer.SetBRegister(result).AdvancePointer();
      }

      private static Computer Jnz(Computer computer, int operand)
      {
         if (computer.Registers.A == 0)
         {
            return computer.AdvancePointer();
         }
         return computer.Jump(operand);
      }

      private static Computer Bxc(Computer computer, int _)
      {
         var result = computer.Registers.B ^ computer.Registers.C;
         return computer.SetBRegister(result).AdvancePointer();
      }

      private static Computer Out(Computer computer, int operand)
      {
         var result = Combo(operand, computer.Registers) % 8;
         return computer.Output(result).AdvancePointer();
      }

      private static Computer Bdv(Computer computer, int operand)
      {
         var result = computer.Registers.A / Math.Pow(2, Combo(operand, computer.Registers));
         return computer.SetBRegister((long)result).AdvancePointer();
      }

      private static Computer Cdv(Computer computer, int operand)
      {
         var result = computer.Registers.A / Math.Pow(2, Combo(operand, computer.Registers));
         return computer.SetCRegister((long)result).AdvancePointer();
      }


      private static long Combo(int operand, Registers registers)
      {
         return operand switch
         {
            >= 0 and <= 3 => operand,
            4 => registers.A,
            5 => registers.B,
            6 => registers.C,
            _ => throw new InvalidOperationException("Invalid combo operand")
         };
      }

      private static Computer ExtractComputer(string[] lines)
      {
         var registers = lines[..3].Select(line => long.Parse(line[12..])).ToList();
         var a = registers[0];
         var b = registers[1];
         var c = registers[2];

         var program = lines[^1][9..].Split(',').Select(int.Parse).ToArray();

         return new(new(a, b, c), 0, program, []);
      }
   }

   public record struct Computer(Registers Registers, int InstructionPointer, int[] Program, long[] OutputBuffer)
   {
      public Computer SetARegister(long newValue)
      {
         var newRegisters = Registers with { A = newValue };
         return this with { Registers = newRegisters };
      }

      public Computer SetBRegister(long newValue)
      {
         var newRegisters = Registers with { B = newValue };
         return this with { Registers = newRegisters };
      }

      public Computer SetCRegister(long newValue)
      {
         var newRegisters = Registers with { C = newValue };
         return this with { Registers = newRegisters };
      }

      public Computer Jump(int to)
      {
         return this with { InstructionPointer = to };
      }

      public Computer AdvancePointer()
      {
         return Jump(InstructionPointer + 2);
      }

      public Computer Output(long output)
      {
         return this with { OutputBuffer = [.. OutputBuffer, output] };
      }
   }

   public record struct Registers(long A, long B, long C);
}