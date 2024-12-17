using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day14
{
   public class Day14 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSafetyFactorAfter100Seconds(lines, new Vector(101, 103)));

         Console.WriteLine("Part 2:");
         TickAndPrint(lines, new Vector(101, 103));
      }

      public void TickAndPrint(string[] lines, Vector areaDimensions)
      {
         var robots = ExtractRobots(lines);

         for (int i = 0; i < 20000; i++)
         {
            if ((i - 22) % 101 == 0 || (i - 98) % 103 == 0)
            {
               Console.WriteLine($"{i} second(s):");
               PrintRobots(robots, areaDimensions);
               Console.WriteLine();
            }
            robots = Tick(robots, areaDimensions);
         }
      }

      public static void PrintRobots(List<Robot> robots, Vector areaDimensions)
      {
         for (int i = 0; i < areaDimensions.Y; i++)
         {
            for (int j = 0; j < areaDimensions.X; j++)
            {
               var tile = new Vector(j, i);
               var numberOfRobotsInTile = robots.Count(robot => robot.Position == tile);
               Console.Write($"{(numberOfRobotsInTile > 0 ? numberOfRobotsInTile : ".")}");
            }
            Console.WriteLine();
         }
      }

      public static int GetSafetyFactorAfter100Seconds(string[] lines, Vector areaDimensions)
      {
         var robots = ExtractRobots(lines);

         for (int i = 0; i < 100; i++)
         {
            robots = Tick(robots, areaDimensions);
         }

         var safetyFactor = CalculateSafetyFactor(robots, areaDimensions);

         return safetyFactor;
      }

      private static int CalculateSafetyFactor(List<Robot> robots, Vector areaDimensions)
      {
         var midX = areaDimensions.X / 2;
         var midY = areaDimensions.Y / 2;

         var quadrant1 = robots.Where(robot => robot.Position.X < midX && robot.Position.Y < midY).ToList();
         var quadrant2 = robots.Where(robot => robot.Position.X < midX && robot.Position.Y > midY).ToList();
         var quadrant3 = robots.Where(robot => robot.Position.X > midX && robot.Position.Y < midY).ToList();
         var quadrant4 = robots.Where(robot => robot.Position.X > midX && robot.Position.Y > midY).ToList();

         return quadrant1.Count * quadrant2.Count * quadrant3.Count * quadrant4.Count;
      }

      private static List<Robot> Tick(List<Robot> robots, Vector areaDimensions)
      {
         return robots.Select(robot => Tick(robot, areaDimensions)).ToList();
      }

      private static Robot Tick(Robot robot, Vector areaDimensions)
      {
         var newPositionBeforeTeleporting = robot.Position + robot.Velocity;
         var newPosition = new Vector(getComponentAfterTeleporting(newPositionBeforeTeleporting.X, areaDimensions.X), getComponentAfterTeleporting(newPositionBeforeTeleporting.Y, areaDimensions.Y));

         int getComponentAfterTeleporting(int positionBeforeTeleporting, int dimension)
         {
            var clipped = positionBeforeTeleporting % dimension;
            return clipped < 0 ? clipped + dimension : clipped;
         }

         return new Robot(newPosition, robot.Velocity);
      }

      private static List<Robot> ExtractRobots(string[] lines)
      {
         return lines.Select(line =>
         {
            var vectors = line.Split().Select(part =>
            {
               var components = part[2..].Split(',');
               return new Vector(int.Parse(components[0]), int.Parse(components[1]));
            }).ToList();

            return new Robot(vectors[0], vectors[1]);
         }).ToList();
      }
   }

   public record struct Robot(Vector Position, Vector Velocity);

   public record struct Vector(int X, int Y)
   {
      public static Vector operator +(Vector a, Vector b)
      {
         return new Vector(a.X + b.X, a.Y + b.Y);
      }

      public static Vector operator -(Vector a, Vector b)
      {
         return new Vector(a.X - b.X, a.Y - b.Y);
      }
   }
}