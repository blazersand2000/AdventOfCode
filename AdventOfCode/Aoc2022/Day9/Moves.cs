using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Aoc2022.Day9
{
   public partial class Moves
   {
      private List<(Position move, int count)> _moves = new List<(Position move, int count)>();

      public static Moves ReadMoves(string[] lines)
      {
         var moves = new Moves();
         foreach (var line in lines)
         {
            var parts = line.Split(" ");
            var direction = parts[0];
            var number = int.Parse(parts[1]);

            moves._moves.Add((GetMoveFromDirection(direction), number));
         }

         return moves;
      }

      public IEnumerable<Position> GetMoves()
      {
         foreach (var moveSet in _moves)
         {
            for (int i = 0; i < moveSet.count; i++)
            {
               yield return moveSet.move;
            }
         }
      }

      private static Position GetMoveFromDirection(string direction)
      {
         switch (direction)
         {
            case "L":
               return new Position(-1, 0);
            case "R":
               return new Position(1, 0);
            case "U":
               return new Position(0, 1);
            case "D":
               return new Position(0, -1);
            default:
               throw new ApplicationException("Unrecognized direction.");
         }
      }
   }
}
