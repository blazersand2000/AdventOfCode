using AdventOfCode.Aoc2023.Day10;
using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day10Tests
   {
      private readonly string[] testData = new[]
      {
         ".....",
         ".S-7.",
         ".|.|.",
         ".L-J.",
         "....."
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetDistanceToFarthestPoint_Tests()
      {
         var expected = 4;
         var result = Day10.GetDistanceToFarthestPoint(testData);

         Assert.AreEqual(expected, result);
      }

      [Test, TestCaseSource(nameof(Part2TestCases))]
      public void GetNumberOfEnclosedTiles_Tests((string[] lines, int expected) testCase)
      {
         var result = Day10.GetNumberOfEnclosedTiles(testCase.lines);

         Assert.AreEqual(testCase.expected, result);
      }

      private static IEnumerable<(string[] lines, int expected)> Part2TestCases()
      {
         yield return (new[]
         {
            "...........",
            ".S-------7.",
            ".|F-----7|.",
            ".||.....||.",
            ".||.....||.",
            ".|L-7.F-J|.",
            ".|..|.|..|.",
            ".L--J.L--J.",
            "..........."
         }, 4);
         yield return (new[]
         {
            "..........",
            ".S------7.",
            ".|F----7|.",
            ".||OOOO||.",
            ".||OOOO||.",
            ".|L-7F-J|.",
            ".|II||II|.",
            ".L--JL--J.",
            ".........."
         }, 4);
         yield return (new[]
         {
            ".F----7F7F7F7F-7....",
            ".|F--7||||||||FJ....",
            ".||.FJ||||||||L7....",
            "FJL7L7LJLJ||LJ.L-7..",
            "L--J.L7...LJS7F-7L7.",
            "....F-J..F7FJ|L7L7L7",
            "....L7.F7||L7|.L7L7|",
            ".....|FJLJ|FJ|F7|.LJ",
            "....FJL-7.||.||||...",
            "....L---J.LJ.LJLJ..."
         }, 8);
         yield return (new[]
         {
            "FF7FSF7F7F7F7F7F---7",
            "L|LJ||||||||||||F--J",
            "FL-7LJLJ||||||LJL-77",
            "F--JF--7||LJLJ7F7FJ-",
            "L---JF-JLJ.||-FJLJJ7",
            "|F|F-JF---7F7-L7L|7|",
            "|FFJF7L7F-JF7|JL---7",
            "7-L-JL7||F7|L7F-7F7|",
            "L.L7LFJ|||||FJL7||LJ",
            "L7JLJL-JLJLJL--JLJ.L"
         }, 10);
      }
   }
}