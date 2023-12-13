using AdventOfCode.Aoc2023.Day11;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day11Tests
   {
      private readonly string[] testData = new[]
      {
         "...#......",
         ".......#..",
         "#.........",
         "..........",
         "......#...",
         ".#........",
         ".........#",
         "..........",
         ".......#..",
         "#...#....."
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void ExpandUniverse_Tests()
      {
         string[] expected = new[]
         {
            "....#........",
            ".........#...",
            "#............",
            ".............",
            ".............",
            "........#....",
            ".#...........",
            "............#",
            ".............",
            ".............",
            ".........#...",
            "#....#......."
         };

         var result = Day11.ExpandUniverse(testData);
         var transformedResult = result.Select(r => string.Join("", r)).ToArray();

         CollectionAssert.AreEqual(expected, transformedResult);
      }

      [Test]
      public void Part1_Tests()
      {
         var expected = 374;
         var result = Day11.Part1(testData);

         Assert.AreEqual(expected, result);
      }

      [Test]
      [TestCase(10, 1030)]
      [TestCase(100, 8410)]
      public void Part2_Tests(long expansionFactor, long expected)
      {
         var result = Day11.Part2(testData, expansionFactor);

         Assert.AreEqual(expected, result);
      }
   }
}