using AdventOfCode.Aoc2023.Day13;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day13Tests
   {
      private readonly string[] testData = new[]
      {
         "#.##..##.",
         "..#.##.#.",
         "##......#",
         "##......#",
         "..#.##.#.",
         "..##..##.",
         "#.#.##.#.",
         "",
         "#...##..#",
         "#....#..#",
         "..##..###",
         "#####.##.",
         "#####.##.",
         "..##..###",
         "#....#..#"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void Part1_Tests()
      {
         var expected = 405;
         var result = Day13.Part1(testData);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void Part2_Tests()
      {
         var expected = 400;
         var result = Day13.Part2(testData);

         Assert.AreEqual(expected, result);
      }

      private readonly string[] testData2 = new[]
      {
         ".#.###.....",
         "#..#.##...#",
         ".#.....#.#.",
         ".#.....#.#.",
         "#..#.##...#",
         ".#.###.....",
         "##.#..##.##",
         "#....##..#.",
         ".##.##.#.##",
         "...#..###.#",
         "...#..###.#",
         ".##.##.#.##",
         "#....##..#.",
         "####..##.##",
         ".#.###.....",
         "#..#.##...#",
         ".#.....#.#."
      };

      [Test]
      public void MorePart2_Tests()
      {
         var part1 = Day13.Part1(testData2);
         var part2 = Day13.Part2(testData2);

         Assert.AreEqual(300, part1);
         //Assert.AreEqual(1000, part2);
      }

      // [Test]
      // [TestCase(10, 1030)]
      // [TestCase(100, 8410)]
      // public void Part2_Tests(long expansionFactor, long expected)
      // {
      //    var result = Day11.Part2(testData, expansionFactor);

      //    Assert.AreEqual(expected, result);
      // }
   }
}