using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Problems.Day2
{
   public class Day2 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Problems/Day2/input.txt");

         var myScore = 0;
         var opponentScore = 0;

         var scoreLookup = new Dictionary<string, int>
         {
            { "A X", 4 },
            { "A Y", 8 },
            { "A Z", 3 },
            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },
            { "C X", 7 },
            { "C Y", 2 },
            { "C Z", 6 },
         };

         foreach(var line in lines)
         {
            myScore += scoreLookup[line];
         }

         Console.WriteLine(myScore);
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
   }

   public enum Shape
   {
      Rock = 1,
      Paper = 2,
      Scissors = 3
   }
}

