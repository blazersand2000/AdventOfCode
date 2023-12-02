using AdventOfCode.Aoc2022.Day8;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tests.Aoc2022
{
   [TestFixture]
   class Day8Tests
   {
      [SetUp]
      public void Setup()
      {

      }

      [Test]
      [TestCase(0, 0, true)]
      [TestCase(0, 1, true)]
      [TestCase(0, 2, true)]
      [TestCase(0, 3, true)]
      [TestCase(0, 4, true)]
      [TestCase(1, 0, true)]
      [TestCase(1, 1, true)]
      [TestCase(1, 2, true)]
      [TestCase(1, 3, false)]
      [TestCase(1, 4, true)]
      [TestCase(2, 0, true)]
      [TestCase(2, 1, true)]
      [TestCase(2, 2, false)]
      [TestCase(2, 3, true)]
      [TestCase(2, 4, true)]
      [TestCase(3, 0, true)]
      [TestCase(3, 1, false)]
      [TestCase(3, 2, true)]
      [TestCase(3, 3, false)]
      [TestCase(3, 4, true)]
      [TestCase(4, 0, true)]
      [TestCase(4, 1, true)]
      [TestCase(4, 2, true)]
      [TestCase(4, 3, true)]
      [TestCase(4, 4, true)]
      public void IsTreeVisible_Tests(int row, int col, bool expected)
      {
         var testGrid = GetTestGrid();

         var result = Day8.IsTreeVisible(testGrid, row, col);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void GetNumVisibleTrees_CorrectNumber()
      {
         var result = Day8.GetNumVisibleTrees(GetTestGrid());

         Assert.AreEqual(21, result);
      }

      [Test]
      public void GetHighestScenicScore_CorrectScore()
      {
         var result = Day8.GetHighestScenicScore(GetTestGrid());

         Assert.AreEqual(8, result);
      }

      private int[][] GetTestGrid()
      {
         return new int[][]
         {
            new int[]{ 3, 0, 3, 7, 3 },
            new int[]{ 2, 5, 5, 1, 2 },
            new int[]{ 6, 5, 3, 3, 2 },
            new int[]{ 3, 3, 5, 4, 9 },
            new int[]{ 3, 5, 3, 9, 0 },
         };
      }
   }
}
