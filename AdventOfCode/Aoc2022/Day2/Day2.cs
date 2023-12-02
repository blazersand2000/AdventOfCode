using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Aoc2022.Day2
{
   public class Day2 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Aoc2022/Day2/input.txt");

         Part1(lines);
         Part2(lines);
      }

      private void Part1(string[] lines)
      {
         var myScore = 0;

         foreach (var line in lines)
         {
            var lineChars = GetLineChars(line);
            var myShape = ConvertToShape(lineChars[1]);
            var opponentShape = ConvertToShape(lineChars[0]);

            myScore += GetRoundScore(myShape, opponentShape) + (int)myShape;
         }

         Console.WriteLine("Part 1:");
         Console.WriteLine(myScore);
      }

      private void Part2(string[] lines)
      {
         var myScore = 0;

         foreach (var line in lines)
         {
            var lineChars = GetLineChars(line);
            var opponentShape = ConvertToShape(lineChars[0]);
            var desiredOutcome = ConvertToOutcome(lineChars[1]);
            var myShape = GetShapeForOutcome(opponentShape, desiredOutcome);

            myScore += GetRoundScore(myShape, opponentShape) + (int)myShape;
         }

         Console.WriteLine("Part 2:");
         Console.WriteLine(myScore);
      }

      private char[] GetLineChars(string line)
      {
         var splitLine = line.Split(' ');
         return new char[] { splitLine[0][0], splitLine[1][0] };
      }

      private int GetRoundScore(Shape myShape, Shape opponentShape)
      {
         if (myShape == opponentShape)
         {
            return (int)Outcome.Draw;
         }
         if (myShape - opponentShape == 1 || myShape - opponentShape == -2)
         {
            return (int)Outcome.Win;
         }
         return (int)Outcome.Lose;
      }

      private Shape GetShapeForOutcome(Shape opponentShape, Outcome desiredOutcome)
      {
         switch (opponentShape)
         {
            case Shape.Rock:
               switch (desiredOutcome)
               {
                  case Outcome.Lose:
                     return Shape.Scissors;
                  case Outcome.Win:
                     return Shape.Paper;
               }
               break;
            case Shape.Paper:
               switch (desiredOutcome)
               {
                  case Outcome.Lose:
                     return Shape.Rock;
                  case Outcome.Win:
                     return Shape.Scissors;
               }
               break;
            case Shape.Scissors:
               switch (desiredOutcome)
               {
                  case Outcome.Lose:
                     return Shape.Paper;
                  case Outcome.Win:
                     return Shape.Rock;
               }
               break;
         }

         return opponentShape;
      }

      private Shape ConvertToShape(char letter)
      {
         switch (letter)
         {
            case 'A':
            case 'X':
               return Shape.Rock;
            case 'B':
            case 'Y':
               return Shape.Paper;
            case 'C':
            case 'Z':
               return Shape.Scissors;
            default:
               throw new ApplicationException("Unexpected input");
         }
      }

      private Outcome ConvertToOutcome(char letter)
      {
         switch (letter)
         {
            case 'X':
               return Outcome.Lose;
            case 'Y':
               return Outcome.Draw;
            case 'Z':
               return Outcome.Win;
            default:
               throw new ApplicationException("Unexpected input");
         }
      }
   }

   public enum Shape
   {
      Rock = 1,
      Paper = 2,
      Scissors = 3
   }

   public enum Outcome
   {
      Lose = 0,
      Draw = 3,
      Win = 6
   }
}

