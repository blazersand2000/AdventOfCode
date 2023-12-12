using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace AdventOfCode.Aoc2023.Day10
{
   public class Day10 : Problem
   {
      public delegate void LoopAction(Point previous, Point current, Move move);

      public override void Run()
      {
         var lines = ReadInputFile();

         var part1 = GetDistanceToFarthestPoint(lines);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var part2 = GetNumberOfEnclosedTiles(lines);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long GetDistanceToFarthestPoint(string[] lines)
      {
         var grid = lines.Select(l => l.ToCharArray()).ToArray();
         var start = FindStart(grid);
         var neighbors = GetConnections(grid, start).Select(n => (n, start));
         var distances = new Dictionary<Point, int> { { start, 0 } };
         var queue = new Queue<(Point point, Point previous)>(neighbors);

         while (queue.Any())
         {
            var current = queue.Dequeue();
            if (distances.ContainsKey(current.point))
            {
               return distances[current.point];
            }
            distances[current.point] = distances[current.previous] + 1;
            var next = GetNext(grid, current.previous, current.point).Next;
            queue.Enqueue((next, current.point));
         }
         return -1;
      }

      public static long GetNumberOfEnclosedTiles(string[] lines)
      {
         var grid = lines.Select(l => l.ToCharArray()).ToArray();
         var start = FindStart(grid);
         var neighbors = GetConnections(grid, start);
         var next = neighbors.First();
         var loop = new HashSet<Point> { start };
         var rights = 0;
         var lefts = 0;

         // Determine the loop tiles and whether it's a right or left hand loop
         TraverseLoop(grid, start, next, (_, current, move) =>
         {
            loop.Add(current);
            if (move.Turn == Turn.Left)
            {
               lefts++;
            }
            else if (move.Turn == Turn.Right)
            {
               rights++;
            }
         });

         var side = lefts > rights ? Side.Left : Side.Right;
         var enclosedTiles = new HashSet<Point>();

         // Find the enclosed tiles
         TraverseLoop(grid, start, next, (previous, current, move) =>
         {
            var adJacentTile = GetAdjacentTile(previous, current, side);
            var adjacentTiles = new HashSet<Point> { adJacentTile };
            if ((move.Turn == Turn.Left && side == Side.Right) || (move.Turn == Turn.Right && side == Side.Left))
            {
               var adjacentTilesMissed = GetAdjacentTilesMissedAfterTurn(adJacentTile, move.Heading);
               adjacentTiles.UnionWith(adjacentTilesMissed);
            }
            var newEnclosedTiles = new Queue<Point>(adjacentTiles.Where(IsAvailableTile));

            while (newEnclosedTiles.Any())
            {
               var tile = newEnclosedTiles.Dequeue();
               enclosedTiles.Add(tile);
               var enclosedNeighbors = tile.GetNonDiagonalNeighbors().Where(IsAvailableTile);
               foreach (var neighbor in enclosedNeighbors.Where(n => !newEnclosedTiles.Contains(n)))
               {
                  newEnclosedTiles.Enqueue(neighbor);
               }
            }

            bool IsAvailableTile(Point p) => InBounds(grid, p) && !loop.Contains(p) && !enclosedTiles.Contains(p);
         });

         return enclosedTiles.Count;
      }

      private static Point GetAdjacentTile(Point previous, Point current, Side side)
      {
         var direction = GetDirection(previous, current);
         return direction switch
         {
            Direction.EB => side == Side.Left ? current.Above() : current.Below(),
            Direction.WB => side == Side.Left ? current.Below() : current.Above(),
            Direction.NB => side == Side.Left ? current.Left() : current.Right(),
            Direction.SB => side == Side.Left ? current.Right() : current.Left(),
            _ => throw new InvalidOperationException("Invalid direction")
         };
      }

      private static HashSet<Point> GetAdjacentTilesMissedAfterTurn(Point existingAdjacent, Direction direction)
      {
         return direction switch
         {
            Direction.EB => new HashSet<Point> { existingAdjacent.Left(), existingAdjacent.Left().Left() },
            Direction.WB => new HashSet<Point> { existingAdjacent.Right(), existingAdjacent.Right().Right() },
            Direction.NB => new HashSet<Point> { existingAdjacent.Below(), existingAdjacent.Below().Below() },
            Direction.SB => new HashSet<Point> { existingAdjacent.Above(), existingAdjacent.Above().Above() },
            _ => throw new InvalidOperationException("Invalid direction")
         };
      }

      private static void TraverseLoop(char[][] grid, Point start, Point next, LoopAction doSomething)
      {
         var previous = start;
         var current = next;
         var direction = GetDirection(previous, current);
         var move = new Move(direction, Turn.Straight);

         // Because we need to check S once we return to it
         do
         {
            doSomething(previous, current, move);
            (next, move) = GetNext(grid, previous, current);
            previous = current;
            current = next;
         } while (current != start);
      }

      private static Direction GetDirection(Point previous, Point current)
      {
         return current switch
         {
            _ when previous.I < current.I => Direction.SB,
            _ when previous.I > current.I => Direction.NB,
            _ when previous.J < current.J => Direction.EB,
            _ when previous.J > current.J => Direction.WB,
            _ => throw new InvalidOperationException("Invalid start")
         };
      }


      private static Point FindStart(char[][] grid)
      {
         for (int i = 0; i < grid.Length; i++)
         {
            for (int j = 0; j < grid[i].Length; j++)
            {
               if (grid[i][j] == 'S')
               {
                  return (i, j);
               }
            }
         }
         return (-1, -1);
      }

      private static (Point Next, Move Move) GetNext(char[][] grid, Point previous, Point current)
      {
         var connections = GetConnections(grid, current);
         try
         {
            var next = connections.Where(c => c != previous).Single();
            var direction = GetMove(previous, current, next);

            return (next, direction);
         }
         catch (System.Exception ex)
         {

            throw;
         }
      }

      private static Move GetMove(Point previous, Point current, Point next)
      {
         if (next.I == previous.I)
         {
            return (next.J < previous.J ? Direction.WB : Direction.EB, Turn.Straight);
         }
         if (next.J == previous.J)
         {
            return (next.I < previous.I ? Direction.NB : Direction.SB, Turn.Straight);
         }
         var (previousDirection, newDirection) = (GetDirection(previous, current), GetDirection(current, next));
         return (previousDirection, newDirection) switch
         {
            (Direction.NB, Direction.EB) => (newDirection, Turn.Right),
            (Direction.NB, Direction.WB) => (newDirection, Turn.Left),
            (Direction.SB, Direction.EB) => (newDirection, Turn.Left),
            (Direction.SB, Direction.WB) => (newDirection, Turn.Right),
            (Direction.EB, Direction.NB) => (newDirection, Turn.Left),
            (Direction.EB, Direction.SB) => (newDirection, Turn.Right),
            (Direction.WB, Direction.NB) => (newDirection, Turn.Right),
            (Direction.WB, Direction.SB) => (newDirection, Turn.Left),
            _ => throw new InvalidOperationException("Invalid move")
         };
      }


      private static IEnumerable<Point> GetConnections(char[][] grid, Point point)
      {
         var value = grid[point.I][point.J];
         Point above = point.Above();
         Point below = point.Below();
         Point left = point.Left();
         Point right = point.Right();

         if (value == 'S')
         {
            var connections = new List<Point>();

            if (InBounds(grid, above) && "7|F".Contains(grid[above.I][above.J]))
            {
               connections.Add(above);
            }
            if (InBounds(grid, below) && "J|L".Contains(grid[below.I][below.J]))
            {
               connections.Add(below);
            }
            if (InBounds(grid, left) && "L-F".Contains(grid[left.I][left.J]))
            {
               connections.Add(left);
            }
            if (InBounds(grid, right) && "7-J".Contains(grid[right.I][right.J]))
            {
               connections.Add(right);
            }
            return connections;
         }

         return value switch
         {
            'F' => new List<Point> { below, right },
            '-' => new List<Point> { left, right },
            '7' => new List<Point> { left, below },
            '|' => new List<Point> { above, below },
            'J' => new List<Point> { above, left },
            'L' => new List<Point> { above, right },
            _ => Array.Empty<Point>(),
         };
      }

      private static bool InBounds(char[][] grid, Point point)
      {
         return point.I >= 0 && point.I < grid.Length && point.J >= 0 && point.J < grid[point.I].Length;
      }
   }

   public record struct Point(int I, int J)
   {
      public static implicit operator (int I, int J)(Point value)
      {
         return (value.I, value.J);
      }

      public static implicit operator Point((int I, int J) value)
      {
         return new Point(value.I, value.J);
      }

      public readonly Point Above()
      {
         return (I - 1, J);
      }

      public readonly Point Below()
      {
         return (I + 1, J);
      }

      public readonly Point Left()
      {
         return (I, J - 1);
      }

      public readonly Point Right()
      {
         return (I, J + 1);
      }

      public readonly HashSet<Point> GetNonDiagonalNeighbors()
      {
         return new HashSet<Point> { Above(), Below(), Left(), Right() };
      }

   }

   public record struct Move(Direction Heading, Turn Turn)
   {
      public static implicit operator (Direction Heading, Turn Turn)(Move value)
      {
         return (value.Heading, value.Turn);
      }

      public static implicit operator Move((Direction Heading, Turn Turn) value)
      {
         return new Move(value.Heading, value.Turn);
      }
   }

   public enum Direction
   {
      EB,
      WB,
      NB,
      SB
   }

   public enum Turn
   {
      Straight,
      Left,
      Right
   }

   public enum Side
   {
      Left,
      Right
   }

}
