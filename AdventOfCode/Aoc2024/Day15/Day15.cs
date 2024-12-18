using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdventOfCode.Aoc2024.Day15
{
   public class Day15 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSumOfAllBoxGpsCoordinates(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetSumOfAllBoxGpsCoordinatesWidened(lines));
      }

      public static int GetSumOfAllBoxGpsCoordinates(string[] lines)
      {
         var (warehouse, moves) = ExtractWarehouseAndMoves(lines);
         var robotLocation = FindRobot(warehouse);

         RunAmokSimple(warehouse, moves, robotLocation);

         var boxLocations = FindAllBoxes(warehouse).ToHashSet();

         return boxLocations.Sum(GetGPSCoordinateOfCoordinate);
      }

      public static int GetSumOfAllBoxGpsCoordinatesWidened(string[] lines)
      {
         var (warehouse, moves) = ExtractWarehouseAndMoves(lines);
         WidenWarehouse(warehouse);
         var robotLocation = FindRobot(warehouse);

         RunAmokWidened(warehouse, moves, robotLocation);

         var boxLocations = FindAllBoxes(warehouse).ToHashSet();

         return boxLocations.Sum(GetGPSCoordinateOfCoordinate);
      }

      private static void WidenWarehouse(string[] warehouse)
      {
         for (int i = 0; i < warehouse.Length; i++)
         {
            var original = warehouse[i].ToCharArray();
            var widened = original.SelectMany<char, char>(c =>
            {
               return c switch
               {
                  '#' => ['#', '#'],
                  'O' => ['[', ']'],
                  '.' => ['.', '.'],
                  '@' => ['@', '.'],
                  _ => throw new InvalidOperationException("Robot not found in the warehouse")
               };
            }).ToArray();
            warehouse[i] = new string(widened);
         }
      }

      private static IEnumerable<Coordinate> FindAllBoxes(string[] warehouse)
      {
         for (int i = 0; i < warehouse.Length; i++)
         {
            for (int j = 0; j < warehouse[i].Length; j++)
            {
               var currentObject = warehouse[i][j];
               if (currentObject == 'O' || currentObject == '[')
               {
                  yield return new Coordinate(i, j);
               }
            }
         }
      }

      private static void RunAmokSimple(string[] warehouse, string moves, Coordinate robotLocation)
      {
         foreach (var move in moves)
         {
            var didMove = TryMove(warehouse, move, robotLocation);

            if (didMove)
            {
               robotLocation = Next(robotLocation, move);
            }
         }
      }

      private static void RunAmokWidened(string[] warehouse, string moves, Coordinate robotLocation)
      {
         foreach (var move in moves)
         {
            var canMove = TryMove(warehouse, move, robotLocation, false);
            if (canMove)
            {
               TryMove(warehouse, move, robotLocation, true);
               robotLocation = Next(robotLocation, move);
            }
         }
      }

      private static int GetGPSCoordinateOfCoordinate(Coordinate coordinate)
      {
         return coordinate.Row * 100 + coordinate.Col;
      }

      private static bool TryMove(string[] warehouse, char move, Coordinate current, bool moveIfPossible = true)
      {
         var currentObject = warehouse[current.Row][current.Col];
         if (currentObject == '#')
         {
            return false;
         }
         if (currentObject == '.')
         {
            return true;
         }

         var inTheWay = GetLocationsInTheWay(warehouse, move, current);
         if (!inTheWay.All(next => TryMove(warehouse, move, next, moveIfPossible)))
         {
            return false;
         }

         if (moveIfPossible)
         {
            foreach (var next in inTheWay)
            {
               Move(warehouse, current, currentObject, next);
               // total hack to set the adjacent in the way value to '.'
               currentObject = '.';
            }
         }

         return true;
      }

      private static HashSet<Coordinate> GetLocationsInTheWay(string[] warehouse, char move, Coordinate current)
      {
         var next = Next(current, move);
         if (move == '<' || move == '>')
         {
            return [next];
         }

         var nextObject = warehouse[next.Row][next.Col];
         if (nextObject != '[' && nextObject != ']')
         {
            return [next];
         }

         if (nextObject == '[')
         {
            return [next, next.Right()];
         }
         else
         {
            return [next, next.Left()];
         }
      }

      private static void Move(string[] warehouse, Coordinate current, char currentObject, Coordinate next)
      {
         var nextRowChars = warehouse[next.Row].ToCharArray();
         nextRowChars[next.Col] = currentObject;
         warehouse[next.Row] = new string(nextRowChars);
         var currentRowChars = warehouse[current.Row].ToCharArray();
         currentRowChars[current.Col] = '.';
         warehouse[current.Row] = new string(currentRowChars);
      }

      private static (string[] Warehouse, string Moves) ExtractWarehouseAndMoves(string[] lines)
      {
         var indexOfBlankLine = lines.ToList().FindIndex(string.IsNullOrWhiteSpace);
         var warehouse = lines[..indexOfBlankLine];
         var moves = string.Join("", lines[(indexOfBlankLine + 1)..]);

         return (warehouse, moves);
      }

      private static Coordinate Next(Coordinate current, char move)
      {
         return move switch
         {
            '^' => current.Up(),
            'v' => current.Down(),
            '>' => current.Right(),
            '<' => current.Left(),
            _ => throw new ArgumentException("Invalid move character", nameof(move))
         };
      }

      private static Coordinate FindRobot(string[] warehouse)
      {
         for (int i = 0; i < warehouse.Length; i++)
         {
            for (int j = 0; j < warehouse[i].Length; j++)
            {
               if (warehouse[i][j] == '@')
               {
                  return new Coordinate(i, j);
               }
            }
         }
         throw new InvalidOperationException("Robot not found in the warehouse");
      }
   }

   public record struct Coordinate(int Row, int Col)
   {
      public Coordinate Up() => new(Row - 1, Col);
      public Coordinate Down() => new(Row + 1, Col);
      public Coordinate Right() => new(Row, Col + 1);
      public Coordinate Left() => new(Row, Col - 1);
   }

}