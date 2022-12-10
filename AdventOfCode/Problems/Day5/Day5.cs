using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Problems.Day5
{
   public class Day5 : IProblem
   {
      private const int NumStacks = 9;

      public void Run()
      {
         var lines = File.ReadAllLines("Problems/Day5/input.txt");

         var splitIdx = lines.ToList().FindIndex(l => string.IsNullOrWhiteSpace(l));
         var initialStacksStateInput = lines.Take(splitIdx);
         var movesInput = lines.Skip(splitIdx + 1);

         var stacks = ParseInitialStacksState(initialStacksStateInput);
         var moves = ParseMoves(movesInput);

         Part1(stacks, moves);
         Part2(stacks, moves);
      }

      private void Part1(Stack<char>[] stacks, IEnumerable<Move> moves)
      {
         var finalStacksState = ProcessCrateMover9000Moves(stacks, moves);

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetTopOfStacks(finalStacksState));
      }

      private void Part2(Stack<char>[] stacks, IEnumerable<Move> moves)
      {
         var finalStacksState = ProcessCrateMover9001Moves(stacks, moves);

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetTopOfStacks(finalStacksState));
      }

      private Stack<char>[] ParseInitialStacksState(IEnumerable<string> lines)
      {
         var stacks = new Stack<char>[NumStacks];
         for (int i = 0; i < NumStacks; i++)
         {
            stacks[i] = new Stack<char>();
         }

         var parsableLines = lines.Reverse().Skip(1);

         foreach (var line in parsableLines)
         {
            if (line.Length != NumStacks * 3 + (NumStacks - 1))
            {
               throw new ApplicationException("Unexpected format when attemtping to parse initial stacks state.");
            }

            for (int i = 1; i < line.Length; i += 4)
            {
               if (!char.IsWhiteSpace(line[i]))
               {
                  stacks[i / 4].Push(line[i]);
               }
            }
         }

         return stacks;
      }

      private IEnumerable<Move> ParseMoves(IEnumerable<string> lines)
      {
         List<Move> moves = new List<Move>();

         foreach (var line in lines)
         {
            var words = line.Split(' ');
            var move = new Move
            {
               Quantity = int.Parse(words[1]),
               From = int.Parse(words[3]) - 1,
               To = int.Parse(words[5]) - 1
            };
            moves.Add(move);
         }

         return moves;
      }

      private Stack<char>[] Copy(Stack<char>[] source)
      {
         return source.Select(stack => new Stack<char>(stack.Reverse())).ToArray();
      }

      private Stack<char>[] ProcessCrateMover9000Moves(Stack<char>[] initialStacks, IEnumerable<Move> moves)
      {
         var stacks = Copy(initialStacks);

         foreach (var move in moves)
         {
            for (int i = 0; i < move.Quantity; i++)
            {
               var removedCrate = stacks[move.From].Pop();
               stacks[move.To].Push(removedCrate);
            }
         }

         return stacks;
      }

      private Stack<char>[] ProcessCrateMover9001Moves(Stack<char>[] initialStacks, IEnumerable<Move> moves)
      {
         var stacks = Copy(initialStacks);
         var tempStack = new Stack<char>();

         foreach (var move in moves)
         {
            for (int i = 0; i < move.Quantity; i++)
            {
               tempStack.Push(stacks[move.From].Pop());
            }
            for (int i = 0; i < move.Quantity; i++)
            {
               stacks[move.To].Push(tempStack.Pop());
            }
         }

         return stacks;
      }

      private string GetTopOfStacks(Stack<char>[] stacks)
      {
         return string.Concat(stacks.Select(s => s.Peek()));
      }

      private struct Move
      {
         public int Quantity { get; set; }
         public int From { get; set; }
         public int To { get; set; }
      }
   }
}

