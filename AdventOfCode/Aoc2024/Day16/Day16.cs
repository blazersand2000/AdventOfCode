using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AdventOfCode.Aoc2024.Day16
{
   public class Day16 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetLowestScore(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetNumberOfTilesInAnyBestPath(lines));

         // These take a long time to run, around 1 minute!
      }

      public static int GetNumberOfTilesInAnyBestPath(string[] lines)
      {
         var startLocation = FindLocation(lines, 'S');
         var endLocation = FindLocation(lines, 'E');
         var start = new Vertex(startLocation, Orientation.East);
         var toVisit = new Queue<Vertex>();
         toVisit.Enqueue(start);
         var graph = BuildGraph(lines, toVisit);

         var paths = GetPathsFrom(start, graph);
         var pathsToEnd = paths.Where(d => d.Key.Coordinate == endLocation);
         var shortestDistanceToEnd = pathsToEnd.Min(d => d.Value.Distance);
         var shortestPathsToEnd = pathsToEnd.Where(p => p.Value.Distance == shortestDistanceToEnd);

         var coordinatesAlongShortestPathsToEnd = GetCoordinatesAlongShortestPaths(shortestPathsToEnd.Select(p => p.Key).ToHashSet(), paths);

         return coordinatesAlongShortestPathsToEnd.Count;
      }

      public static int GetLowestScore(string[] lines)
      {
         var startLocation = FindLocation(lines, 'S');
         var endLocation = FindLocation(lines, 'E');
         var start = new Vertex(startLocation, Orientation.East);
         var toVisit = new Queue<Vertex>();
         toVisit.Enqueue(start);
         var graph = BuildGraph(lines, toVisit);

         var paths = GetPathsFrom(start, graph);
         var distanceToDestination = paths.Where(d => d.Key.Coordinate == endLocation).Min(d => d.Value.Distance);

         return distanceToDestination;
      }

      private static HashSet<Coordinate> GetCoordinatesAlongShortestPaths(HashSet<Vertex> shortestPathDestinations, Dictionary<Vertex, DijkstraState> allPaths)
      {
         return shortestPathDestinations.SelectMany(p => GetCoordinatesAlongPath(p, allPaths)).ToHashSet();
      }

      private static HashSet<Coordinate> GetCoordinatesAlongPath(Vertex vertex, Dictionary<Vertex, DijkstraState> allPaths)
      {
         var predecessors = allPaths[vertex].Predecessors;
         var current = new HashSet<Coordinate> { vertex.Coordinate };
         if (!predecessors.Any())
         {
            return current;
         }

         foreach (var predecessor in predecessors)
         {
            current.UnionWith(GetCoordinatesAlongPath(predecessor, allPaths));
         }

         return current;
      }

      private static Dictionary<Vertex, DijkstraState> GetPathsFrom(Vertex from, Dictionary<Vertex, Dictionary<Vertex, int>> graph)
      {
         var paths = RunDijkstra(from, graph);

         return paths;
      }

      private static Dictionary<Vertex, DijkstraState> RunDijkstra(Vertex from, Dictionary<Vertex, Dictionary<Vertex, int>> graph)
      {
         var state = graph.Keys.ToDictionary(v => v, v => new DijkstraState(false, int.MaxValue, []));
         state[from] = new DijkstraState(false, 0, []);

         while (true)
         {
            var unvisiteds = state.Where(s => !s.Value.Visited);
            if (!unvisiteds.Any())
            {
               return state;
            }

            var current = unvisiteds.OrderBy(s => s.Value.Distance).First().Key;

            foreach (var neighbor in graph[current].Where(n => !state[n.Key].Visited))
            {
               var newDistance = state[current].Distance + neighbor.Value;
               if (newDistance < state[neighbor.Key].Distance)
               {
                  state[neighbor.Key] = new DijkstraState(false, newDistance, [current]);
               }
               else if (newDistance == state[neighbor.Key].Distance)
               {
                  var newPredecessors = state[neighbor.Key].Predecessors.Concat([current]).ToHashSet();
                  state[neighbor.Key] = new DijkstraState(false, newDistance, newPredecessors);
               }
            }

            state[current] = new DijkstraState(true, state[current].Distance, state[current].Predecessors);
         }
      }

      private static void RunDijkstraRecursive(Dictionary<Vertex, DijkstraState> state, Dictionary<Vertex, Dictionary<Vertex, int>> graph)
      {
         var unvisiteds = state.Where(s => !s.Value.Visited);
         if (!unvisiteds.Any())
         {
            return;
         }

         var current = unvisiteds.OrderBy(s => s.Value.Distance).First().Key;

         foreach (var neighbor in graph[current].Where(n => !state[n.Key].Visited))
         {
            var newDistance = state[current].Distance + neighbor.Value;
            if (newDistance < state[neighbor.Key].Distance)
            {
               // I added predecessors after abandoning the recursive approach, so it's just set to empty here and below
               state[neighbor.Key] = new DijkstraState(false, newDistance, []);
            }
         }

         state[current] = new DijkstraState(true, state[current].Distance, []);

         RunDijkstraRecursive(state, graph);
      }

      private static Dictionary<Vertex, Dictionary<Vertex, int>> BuildGraph(string[] maze, Queue<Vertex> toVisit)
      {
         var graph = new Dictionary<Vertex, Dictionary<Vertex, int>>();

         while (toVisit.TryDequeue(out var current))
         {
            var rotatedLeft = new Vertex(current.Coordinate, current.Orientation.Left());
            var aheadCoordinate = current.Orientation switch
            {
               Orientation.North => current.Coordinate.North(),
               Orientation.South => current.Coordinate.South(),
               Orientation.East => current.Coordinate.East(),
               Orientation.West => current.Coordinate.West(),
               _ => throw new ArgumentOutOfRangeException(nameof(current.Orientation), current.Orientation, null)
            };
            var ahead = new Vertex(aheadCoordinate, current.Orientation);
            var rotatedRight = new Vertex(current.Coordinate, current.Orientation.Right());

            graph[current] = new()
            {
               { rotatedLeft, 1000 },
               { rotatedRight, 1000 }
            };

            if (maze[aheadCoordinate.Row][aheadCoordinate.Col] != '#')
            {
               graph[current][ahead] = 1;
            }

            foreach (var graphNeighbor in graph[current].Keys.Where(v => !graph.ContainsKey(v)))
            {
               toVisit.Enqueue(graphNeighbor);
            }
         }

         return graph;
      }

      private static void BuildGraphRecursive(string[] maze, Vertex current, Dictionary<Vertex, Dictionary<Vertex, int>> graph)
      {
         if (maze[current.Coordinate.Row][current.Coordinate.Col] == '#')
         {
            return;
         }
         if (graph.ContainsKey(current))
         {
            return;
         }

         var rotatedLeft = new Vertex(current.Coordinate, current.Orientation.Left());
         var aheadCoordinate = current.Orientation switch
         {
            Orientation.North => current.Coordinate.North(),
            Orientation.South => current.Coordinate.South(),
            Orientation.East => current.Coordinate.East(),
            Orientation.West => current.Coordinate.West(),
            _ => throw new ArgumentOutOfRangeException(nameof(current.Orientation), current.Orientation, null)
         };
         var ahead = new Vertex(aheadCoordinate, current.Orientation);
         var rotatedRight = new Vertex(current.Coordinate, current.Orientation.Right());

         graph[current] = new()
         {
            { rotatedLeft, 1000 },
            { rotatedRight, 1000 }
         };

         if (maze[aheadCoordinate.Row][aheadCoordinate.Col] != '#')
         {
            graph[current][ahead] = 1;
         }

         foreach (var graphNeighbor in graph[current].Keys)
         {
            BuildGraphRecursive(maze, graphNeighbor, graph);
         }
      }

      private static Coordinate FindLocation(string[] maze, char location)
      {
         for (int i = 0; i < maze.Length; i++)
         {
            for (int j = 0; j < maze[i].Length; j++)
            {
               if (maze[i][j] == location)
               {
                  return new Coordinate(i, j);
               }
            }
         }
         throw new InvalidOperationException("Not found");
      }
   }

   public record struct DijkstraState(bool Visited, int Distance, HashSet<Vertex> Predecessors);

   public record struct Vertex(Coordinate Coordinate, Orientation Orientation);

   public enum Orientation { North, South, East, West }

   public record struct Coordinate(int Row, int Col)
   {
      public Coordinate North() => new(Row - 1, Col);
      public Coordinate South() => new(Row + 1, Col);
      public Coordinate East() => new(Row, Col + 1);
      public Coordinate West() => new(Row, Col - 1);
   }

   public static class OrientationExtensions
   {
      public static Orientation Left(this Orientation orientation)
      {
         return orientation switch
         {
            Orientation.North => Orientation.West,
            Orientation.West => Orientation.South,
            Orientation.South => Orientation.East,
            Orientation.East => Orientation.North,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
         };
      }

      public static Orientation Right(this Orientation orientation)
      {
         return orientation switch
         {
            Orientation.North => Orientation.East,
            Orientation.East => Orientation.South,
            Orientation.South => Orientation.West,
            Orientation.West => Orientation.North,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
         };
      }
   }
}