using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Problems.Day12
{
   public class Day12 : IProblem
   {
      public delegate void GridAction(int row, int col);

      public void Run()
      {
         var lines = File.ReadAllLines("Problems/Day12/input.txt");
         Console.WriteLine("Part 1:");
         var fewestSteps = Part1(lines);
         Console.WriteLine(fewestSteps);

         Console.WriteLine("Part 2:");
      }

      private int Part1(string[] lines)
      {
         var grid = ParseInput(lines);
         return GetFewestSteps(grid);
      }

      private int GetFewestSteps(Node[][] grid)
      {
         var start = FindAndReplace(grid, 'S', 'a');
         var end = FindAndReplace(grid, 'E', 'z');
         var neighbors = new HashSet<Coordinate>();
         Coordinate? current = start;
         grid[start.Row][start.Col].DistanceTo = 0;

         do
         {
            neighbors = ExploreNewNeighbors(grid, neighbors, (Coordinate)current);
            MarkCurrentAsVisited(grid, (Coordinate)current);
            current = GetNext(grid, neighbors);
         } while (neighbors.Count >= 0 && current != null);

         PrintGridDistances(grid);
         PrintGridVisited(grid);
         PrintPathTo(grid, end);

         return grid[end.Row][end.Col].DistanceTo;
      }

      private void PrintGridDistances(Node[][] grid)
      {
         Console.WriteLine();
         TraverseGrid(grid, (row, col) =>
         {
            Console.Write($"{grid[row][col].DistanceTo} | ");
            if (col == grid[0].Length - 1)
            {
               Console.WriteLine();
            }
         });
         Console.WriteLine();
      }

      private void PrintGridVisited(Node[][] grid)
      {
         Console.WriteLine();
         TraverseGrid(grid, (row, col) =>
         {
            Console.Write($"{(grid[row][col].Visited ? "1" : "0")} | ");
            if (col == grid[0].Length - 1)
            {
               Console.WriteLine();
            }
         });
         Console.WriteLine();
      }

      private void PrintPathTo(Node[][] grid, Coordinate coordinate)
      {
         char[][] printGrid = new char[grid.Length][];
         for (int row = 0; row < printGrid.Length; row++)
         {
            printGrid[row] = new char[grid[0].Length];
            for (int col = 0; col < printGrid[row].Length; col++)
            {
               printGrid[row][col] = '.';
            }
         }

         var current = coordinate;
         printGrid[current.Row][current.Col] = 'E';
         Coordinate? previous = grid[current.Row][current.Col].Previous;
         while (previous != null)
         {
            if (current.Row > previous.Value.Row)
            {
               printGrid[previous.Value.Row][previous.Value.Col] = 'v';
            }
            else if (current.Row < previous.Value.Row)
            {
               printGrid[previous.Value.Row][previous.Value.Col] = '^';
            }
            else if (current.Col > previous.Value.Col)
            {
               printGrid[previous.Value.Row][previous.Value.Col] = '>';
            }
            else if (current.Col < previous.Value.Col)
            {
               printGrid[previous.Value.Row][previous.Value.Col] = '<';
            }

            current = previous.Value;
            previous = grid[current.Row][current.Col].Previous;
         }

         Console.WriteLine();
         TraverseGrid(printGrid, (row, col) =>
         {
            Console.Write($"{printGrid[row][col]}");
            if (col == grid[0].Length - 1)
            {
               Console.WriteLine();
            }
         });
         Console.WriteLine();
      }

      private static Coordinate? GetNext(Node[][] grid, HashSet<Coordinate> neighbors)
      {
         var next = neighbors.OrderBy(n => grid[n.Row][n.Col].DistanceTo).Select(n => (Coordinate?)n).FirstOrDefault();
         if (next == null)
         {
            return null;
         }
         neighbors.Remove((Coordinate)next);
         return next;
      }

      private static void MarkCurrentAsVisited(Node[][] grid, Coordinate current)
      {
         grid[current.Row][current.Col].Visited = true;
      }

      private HashSet<Coordinate> ExploreNewNeighbors(Node[][] grid, HashSet<Coordinate> neighbors, Coordinate current)
      {
         var newNeighbors = GetNeighbors(grid, current);//.Except(neighbors);
         neighbors = neighbors.Concat(newNeighbors).ToHashSet();
         var newNeighborNodes = newNeighbors.Select(n => new { Coordinate = n, Node = grid[n.Row][n.Col] });
         foreach (var neighbor in newNeighborNodes)
         {
            if (neighbor.Node.DistanceTo > grid[current.Row][current.Col].DistanceTo + 1)
            {
               neighbor.Node.DistanceTo = grid[current.Row][current.Col].DistanceTo + 1;
               neighbor.Node.Previous = new Coordinate(current.Row, current.Col);
            }
         }

         return neighbors;
      }

      private HashSet<Coordinate> GetNeighbors(Node[][] grid, Coordinate current)
      {
         var possibleNeighbors = new HashSet<Coordinate>();
         if (current.Row != 0)
         {
            possibleNeighbors.Add(new Coordinate(current.Row - 1, current.Col));
         }
         if (current.Row != grid.Length - 1)
         {
            possibleNeighbors.Add(new Coordinate(current.Row + 1, current.Col));
         }
         if (current.Col != 0)
         {
            possibleNeighbors.Add(new Coordinate(current.Row, current.Col - 1));
         }
         if (current.Col != grid[0].Length - 1)
         {
            possibleNeighbors.Add(new Coordinate(current.Row, current.Col + 1));
         }

         return possibleNeighbors.Where(n => grid[n.Row][n.Col].Value <= grid[current.Row][current.Col].Value + 1 && grid[n.Row][n.Col].Visited == false).ToHashSet();
      }

      private Coordinate FindAndReplace(Node[][] grid, char toFind, char replaceWith)
      {
         var node = grid.SelectMany(row => row).First(node => node.Value == toFind);
         Coordinate? coordinate = null;

         TraverseGrid(grid, (row, col) =>
         {
            if (grid[row][col].Value == toFind)
            {
               grid[row][col].Value = replaceWith;
               coordinate = (row, col);
            }
         });

         if (coordinate == null)
         {
            throw new Exception($"{toFind} not found in grid.");
         }

         return (Coordinate)coordinate;
      }

      private void TraverseGrid<T>(T[][] grid, GridAction Action)
      {
         for (int row = 0; row < grid.Length; row++)
         {
            for (int col = 0; col < grid[0].Length; col++)
            {
               Action(row, col);
            }
         }
      }

      private Node[][] ParseInput(string[] lines)
      {
         var numRows = lines.Length;
         var numCols = lines[0].Length;
         var grid = new Node[numRows][];

         for (int row = 0; row < numRows; row++)
         {
            grid[row] = new Node[numCols];
            for (int col = 0; col < numCols; col++)
            {
               grid[row][col] = new Node { Value = lines[row][col], Visited = false, DistanceTo = int.MaxValue, Previous = null };
            }
         }

         return grid;
      }

      private class Node
      {
         public char Value { get; set; }
         public bool Visited { get; set; }
         public int DistanceTo { get; set; }
         public Coordinate? Previous { get; set; }
      }

   }

   internal struct Coordinate
   {
      public int Row;
      public int Col;

      public Coordinate(int row, int col)
      {
         Row = row;
         Col = col;
      }

      public override bool Equals(object obj)
      {
         return obj is Coordinate other &&
                Row == other.Row &&
                Col == other.Col;
      }

      public override int GetHashCode()
      {
         return HashCode.Combine(Row, Col);
      }

      public void Deconstruct(out int row, out int col)
      {
         row = Row;
         col = Col;
      }

      public static implicit operator (int Row, int Col)(Coordinate value)
      {
         return (value.Row, value.Col);
      }

      public static implicit operator Coordinate((int Row, int Col) value)
      {
         return new Coordinate(value.Row, value.Col);
      }
   }
}

