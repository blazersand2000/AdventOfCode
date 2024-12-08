using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace AdventOfCode.Aoc2024.Day6
{
   public class Day6 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetCountOfDistinctGuardPositions(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetCountOfObstructionPositions(lines));
      }

      public static int GetCountOfDistinctGuardPositions(string[] lines)
      {
         var distinctPositions = new HashSet<Coordinate>();
         var startPosition = FindStartPosition(lines);
         var startDirection = new Coordinate(-1, 0);

         Patrol(lines, startPosition, startDirection, (position, direction) =>
         {
            distinctPositions.Add(position);
            return true;
         });

         return distinctPositions.Count;
      }

      // This code sucks
      public static int GetCountOfObstructionPositions(string[] lines)
      {
         var pathHistory = new HashSet<(Coordinate Position, Coordinate Direction)>();
         var obstructionPositions = new HashSet<Coordinate>();

         var startPosition = FindStartPosition(lines);
         var startDirection = new Coordinate(-1, 0);

         Patrol(lines, startPosition, startDirection, (position, direction) =>
         {
            pathHistory.Add((position, direction));
            var inFront = AdjacentPosition(position, direction);
            if (InBounds(inFront, lines) && lines[inFront.Row][inFront.Col] == '.' && !pathHistory.Any(p => p.Position == inFront))
            {
               var original = lines[inFront.Row];
               var changed = new StringBuilder(original[..inFront.Col]);
               changed.Append('#');
               changed.Append(original[(inFront.Col + 1)..]);
               lines[inFront.Row] = changed.ToString();
               var directionToRight = GetNextDirection(direction);
               var currentCycleCheckPath = new HashSet<(Coordinate Position, Coordinate Direction)>();
               Patrol(lines, position, directionToRight, (cycleCheckPosition, cycleCheckDirection) =>
               {
                  if (pathHistory.Contains((cycleCheckPosition, cycleCheckDirection)) || currentCycleCheckPath.Contains((cycleCheckPosition, cycleCheckDirection)))
                  {
                     obstructionPositions.Add(inFront);
                     Console.WriteLine($"({inFront.Row},{inFront.Col})");
                     return false;
                  }
                  currentCycleCheckPath.Add((cycleCheckPosition, cycleCheckDirection));
                  return true;
               });
               lines[inFront.Row] = original;
            }

            return true;
         });

         return obstructionPositions.Count;
      }

      public delegate bool PatrolAction(Coordinate position, Coordinate direction);

      private static void Patrol(string[] lines, Coordinate startPosition, Coordinate startDirection, PatrolAction patrolAction)
      {
         var position = startPosition;
         var direction = startDirection;

         while (InBounds(position, lines))
         {
            var shouldContinue = patrolAction(position, direction);
            if (!shouldContinue)
            {
               return;
            }

            if (TheresAnObstacleInFront(position, direction, lines))
            {
               direction = GetNextDirection(direction);
            }
            else
            {
               position = AdjacentPosition(position, direction);
            }
         }
      }

      private static bool TheresAnObstacleInFront(Coordinate position, Coordinate direction, string[] lines)
      {
         Coordinate inFront = AdjacentPosition(position, direction);
         if (!InBounds(inFront, lines))
         {
            return false;
         }
         return lines[inFront.Row][inFront.Col] == '#';
      }

      private static Coordinate AdjacentPosition(Coordinate position, Coordinate direction)
      {
         return new Coordinate(position.Row + direction.Row, position.Col + direction.Col);
      }

      private static bool InBounds(Coordinate coordinate, string[] lines)
      {
         var (row, col) = coordinate;
         return row >= 0 && row < lines.Length && col >= 0 && col < lines[0].Length;
      }

      private static Coordinate GetNextDirection(Coordinate currentDirection)
      {
         return currentDirection switch
         {
            (-1, 0) => new Coordinate(0, 1),
            (0, 1) => new Coordinate(1, 0),
            (1, 0) => new Coordinate(0, -1),
            (0, -1) => new Coordinate(-1, 0),
            _ => throw new InvalidOperationException("Invalid direction")
         };
      }

      private static Coordinate FindStartPosition(string[] lines)
      {
         for (int i = 0; i < lines.Length; i++)
         {
            for (int j = 0; j < lines[0].Length; j++)
            {
               if (lines[i][j] == '^')
               {
                  return new Coordinate(i, j);
               }
            }
         }
         throw new InvalidOperationException("Starting guard position not found!");
      }
   }

   public record struct Coordinate(int Row, int Col);
}
