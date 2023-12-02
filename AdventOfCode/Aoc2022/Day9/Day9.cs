using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Aoc2022.Day9
{
   public class Day9 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Aoc2022/Day9/input.txt");

         Console.WriteLine("Part 1:");
         var count = GetUniquePositionsVisitedByTail(lines);
         Console.WriteLine(count);

         Console.WriteLine("Part 2:");
         count = GetUniquePositionsVisitedByLastTail(lines, 10);
         Console.WriteLine(count);
      }

      public static int GetUniquePositionsVisitedByTail(string[] lines)
      {
         var moves = Moves.ReadMoves(lines);
         var tailPosition = new Position(0, 0);
         var headPosition = new Position(tailPosition.X, tailPosition.Y);
         var positionsVisitedByTail = new HashSet<Position> { new Position(tailPosition.X, tailPosition.Y) };

         foreach (var move in moves.GetMoves())
         {
            headPosition = new Position(headPosition.X + move.X, headPosition.Y + move.Y);
            if (!AreTouching(headPosition, tailPosition))
            {
               tailPosition = FollowHead(headPosition, tailPosition);
            }
            positionsVisitedByTail.Add(new Position(tailPosition.X, tailPosition.Y));
         }

         return positionsVisitedByTail.Count;
      }

      public static int GetUniquePositionsVisitedByLastTail(string[] lines, int numKnots)
      {
         var moves = Moves.ReadMoves(lines);
         var knots = new List<Position>();
         for (int i = 0; i < numKnots; i++)
         {
            knots.Add(new Position(0, 0));
         }
         var positionsVisitedByTail = new HashSet<Position> { new Position(0, 0) };


         foreach (var move in moves.GetMoves())
         {
            knots[0] = new Position(knots[0].X + move.X, knots[0].Y + move.Y);
            for (int i = 0; i < numKnots - 1; i++)
            {
               if (!AreTouching(knots[i], knots[i + 1]))
               {
                  knots[i + 1] = FollowHead(knots[i], knots[i + 1]);
                  if (i == numKnots - 2)
                  {
                     positionsVisitedByTail.Add(new Position(knots[i + 1].X, knots[i + 1].Y));
                  }
               }
            }
         }

         return positionsVisitedByTail.Count;
      }

      private static Position FollowHead(Position head, Position tail)
      {
         var tries = new List<Position>();
         if (OnSameAxis(head, tail))
         {
            tries.Add(new Position(-1, 0));
            tries.Add(new Position(1, 0));
            tries.Add(new Position(0, -1));
            tries.Add(new Position(0, 1));
         }
         else
         {
            tries.Add(new Position(-1, -1));
            tries.Add(new Position(-1, 1));
            tries.Add(new Position(1, -1));
            tries.Add(new Position(1, 1));
         }

         foreach (var t in tries)
         {
            var newTail = new Position(tail.X + t.X, tail.Y + t.Y);
            if (AreTouching(head, newTail))
            {
               return newTail;
            }
         }
         throw new ApplicationException("Head has abandoned tail :(");
      }

      private static bool AreTouching(Position a, Position b)
      {
         return Math.Abs(a.X - b.X) <= 1 && Math.Abs(a.Y - b.Y) <= 1;
      }

      private static bool OnSameAxis(Position a, Position b)
      {
         return a.X == b.X || a.Y == b.Y;
      }
   }
}

