using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day18
{
   public class Day18 : Problem
   {
      public override void Run()
      {
         const int lastMemorySpace = 70;
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetMinimumNumberOfSteps(lines, lastMemorySpace));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetCoordinateOfFirstByteBlockingExit(lines, lastMemorySpace));
      }

      public static int GetMinimumNumberOfSteps(string[] lines, int lastMemorySpace, int numBytesToFall = 1024)
      {
         var fallingBytes = lines.Take(numBytesToFall).Select(l =>
         {
            var parts = l.Split(',').Select(s => int.Parse(s)).ToImmutableArray();
            return new Coordinate(parts[0], parts[1]);
         }).ToImmutableHashSet();

         var path = GetPathBFS(fallingBytes, lastMemorySpace);

         return (path?.Count ?? 0) - 1;
      }

      public static string GetCoordinateOfFirstByteBlockingExit(string[] lines, int lastMemorySpace, int numBytesToSkip = 1024)
      {
         var fallingBytes = lines.Select(l =>
         {
            var parts = l.Split(',').Select(s => int.Parse(s)).ToImmutableArray();
            return new Coordinate(parts[0], parts[1]);
         });

         // TODO: Skipping the number of bytes (1024) we know had a path in part 1 is not sufficient; try binary searching this bad boy
         for (int i = numBytesToSkip; i < fallingBytes.Count(); i++)
         {
            var bytes = fallingBytes.Take(i);
            if (!FindDestinationDFS(bytes.ToImmutableHashSet(), new HashSet<Coordinate>(), new Coordinate(0, 0), lastMemorySpace))
            {
               var blockingByte = bytes.Last();
               return $"{blockingByte.Row},{blockingByte.Col}";
            }
         }

         throw new InvalidOperationException("All falling bytes fell without blocking path");
      }

      private static List<Coordinate>? GetPathBFS(ImmutableHashSet<Coordinate> fallingBytes, int lastMemorySpace)
      {
         var start = new Coordinate(0, 0);
         var visitedToPreviousMapping = new Dictionary<Coordinate, Coordinate?> { { start, null } };
         var next = new Queue<Coordinate>([start]);
         var destination = new Coordinate(lastMemorySpace, lastMemorySpace);

         while (next.Count > 0)
         {
            var current = next.Dequeue();

            if (current == destination)
            {
               var path = new List<Coordinate>();
               for (Coordinate? coordinate = current; coordinate != null; coordinate = visitedToPreviousMapping[coordinate])
               {
                  path.Add(coordinate);
               }

               path.Reverse();
               return path;
            }


            foreach (var neighbor in current.Neighbors().Where(c => InBounds(c, lastMemorySpace)).Except(fallingBytes).Except(visitedToPreviousMapping.Keys))
            {
               next.Enqueue(neighbor);
               // marking as visited now instead of when we actually visit since this is when we know its previous coordinate
               visitedToPreviousMapping[neighbor] = current;
            }
         }

         return null;
      }

      private static bool FindDestinationDFS(ImmutableHashSet<Coordinate> fallingBytes, HashSet<Coordinate> visited, Coordinate current, int lastMemorySpace)
      {
         if (current == new Coordinate(lastMemorySpace, lastMemorySpace))
         {
            return true;
         }

         visited.Add(current);

         foreach (var neighbor in current.Neighbors().Where(c => InBounds(c, lastMemorySpace)).Except(fallingBytes).Except(visited))
         {
            if (FindDestinationDFS(fallingBytes, visited, neighbor, lastMemorySpace))
            {
               return true;
            }
         }

         return false;
      }

      private static bool InBounds(Coordinate coordinate, int lastMemorySpace)
      {
         return coordinate.Row >= 0 && coordinate.Row <= lastMemorySpace && coordinate.Col >= 0 && coordinate.Col <= lastMemorySpace;
      }

      public record Coordinate(int Row, int Col)
      {
         public Coordinate Up() => new(Row - 1, Col);
         public Coordinate Down() => new(Row + 1, Col);
         public Coordinate Right() => new(Row, Col + 1);
         public Coordinate Left() => new(Row, Col - 1);
         public ImmutableHashSet<Coordinate> Neighbors() => [Up(), Down(), Left(), Right()];
      }
   }
}