using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace AdventOfCode.Problems.Day8
{
   public class Day8 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Problems/Day8/input.txt");

         Console.WriteLine("Part 1:");
         var treeHeightsGrid = ReadGrid(lines);
         var numVisibleTrees = GetNumVisibleTrees(treeHeightsGrid);
         Console.WriteLine(numVisibleTrees);

         Console.WriteLine("Part 2:");
         var highestScenicScore = GetHighestScenicScore(treeHeightsGrid);
         Console.Write(highestScenicScore);
      }

      public static int GetNumVisibleTrees(int[][] treeHeightsGrid)
      {
         var visibilityGrid = GetVisibilityGrid(treeHeightsGrid);

         var num = 0;
         for (int row = 0; row < visibilityGrid.Length; row++)
         {
            for (int col = 0; col < visibilityGrid[0].Length; col++)
            {
               if (treeHeightsGrid[row][col] > visibilityGrid[row][col])
               {
                  num++;
               }
            }
         }

         return num;
      }

      public static int GetHighestScenicScore(int[][] treeHeightsGrid)
      {
         var highestScore = 0;
         for (int row = 1; row < treeHeightsGrid.Length - 1; row++)
         {
            for (int col = 1; col < treeHeightsGrid[0].Length - 1; col++)
            {
               highestScore = Math.Max(highestScore, EvaluateScenicScore(treeHeightsGrid, row, col));
            }
         }

         return highestScore;
      }

      public static bool IsTreeVisible(int[][] treeHeightsGrid, int row, int col)
      {
         var visibilityGrid = GetVisibilityGrid(treeHeightsGrid);
         return treeHeightsGrid[row][col] > visibilityGrid[row][col];
      }

      private static int[][] GetVisibilityGrid(int[][] treeHeightsGrid)
      {
         var visibilityGrid = InitializeVisibilityGrid(treeHeightsGrid.Length, treeHeightsGrid[0].Length);

         EvaluateVisibilityGrid(visibilityGrid, treeHeightsGrid);

         return visibilityGrid;
      }

      private static int[][] InitializeVisibilityGrid(int rows, int cols)
      {
         var initializedVisibilityGrid = new int[rows][];
         for (int row = 0; row < rows; row++)
         {
            initializedVisibilityGrid[row] = new int[cols];
            for (int col = 0; col < cols; col++)
            {
               if (row == 0 || row == rows -1 || col == 0 || col == cols - 1)
               {
                  initializedVisibilityGrid[row][col] = -1;
               }
               else
               {
                  initializedVisibilityGrid[row][col] = int.MaxValue;
               }
            }
         }

         return initializedVisibilityGrid;
      }

      private static void EvaluateVisibilityGrid(int[][] visibilityGrid, int[][] treeHeightsGrid)
      {
         int treeLine;
         for (int row = 1; row < visibilityGrid.Length; row++)
         {
            // Left
            treeLine = treeHeightsGrid[row][0];
            for (int col = 1; col < visibilityGrid[0].Length; col++)
            {
               treeLine = EvaluateVisibility(visibilityGrid, treeHeightsGrid, treeLine, row, col);
            }
            // Right
            treeLine = treeHeightsGrid[row][visibilityGrid[0].Length - 1];
            for (int col = visibilityGrid[0].Length - 2; col >= 0; col--)
            {
               treeLine = EvaluateVisibility(visibilityGrid, treeHeightsGrid, treeLine, row, col);
            }
         }
         for (int col = 1; col < visibilityGrid[0].Length; col++)
         {
            // Top
            treeLine = treeHeightsGrid[0][col];
            for (int row = 1; row < visibilityGrid.Length; row++)
            {
               treeLine = EvaluateVisibility(visibilityGrid, treeHeightsGrid, treeLine, row, col);
            }
            // Bottom
            treeLine = treeHeightsGrid[visibilityGrid.Length - 1][col];
            for (int row = visibilityGrid.Length - 2; row >= 0; row--)
            {
               treeLine = EvaluateVisibility(visibilityGrid, treeHeightsGrid, treeLine, row, col);
            }
         }

         static int EvaluateVisibility(int[][] visibilityGrid, int[][] treeHeightsGrid, int treeLine, int row, int col)
         {
            var treeHeight = treeHeightsGrid[row][col];
            visibilityGrid[row][col] = Math.Min(visibilityGrid[row][col], treeLine);
            if (treeHeight > treeLine)
            {
               treeLine = treeHeight;
            }

            return treeLine;
         }
      }

      private static int EvaluateScenicScore(int[][] treeHeightsGrid, int row, int col)
      {
         int i = col;
         var left = 0;
         do
         {
            left++;
         } while (--i > 0 && treeHeightsGrid[row][col] > treeHeightsGrid[row][i]);

         i = col;
         var right = 0;
         do
         {
            right++;
         } while (++i < treeHeightsGrid[row].Length - 1 && treeHeightsGrid[row][col] > treeHeightsGrid[row][i]);

         i = row;
         var top = 0;
         do
         {
            top++;
         } while (--i > 0 && treeHeightsGrid[row][col] > treeHeightsGrid[i][col]);

         i = row;
         var bottom = 0;
         do
         {
            bottom++;
         } while (++i < treeHeightsGrid.Length - 1 && treeHeightsGrid[row][col] > treeHeightsGrid[i][col]);

         return left * right * top * bottom;
      }

      private static int[][] ReadGrid(string[] lines)
      {
         var grid = new int[lines.Length][];
         for (int row = 0; row < grid.Length; row++)
         {
            grid[row] = new int[lines[0].Length];
            for (int col = 0; col < lines[0].Length; col++)
            {
               grid[row][col] = int.Parse(lines[row][col].ToString());
            }
         }

         return grid;
      }
   }
}

