using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day16;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day16Tests
   {
      private readonly string[] testData =
      [
         "#################",
         "#...#...#...#..E#",
         "#.#.#.#.#.#.#.#.#",
         "#.#.#.#...#...#.#",
         "#.#.#.#.###.#.#.#",
         "#...#.#.#.....#.#",
         "#.#.#.#.#.#####.#",
         "#.#...#.#.#.....#",
         "#.#.#####.#.###.#",
         "#.#.#.......#...#",
         "#.#.###.#####.###",
         "#.#.#...#.....#.#",
         "#.#.#.#####.###.#",
         "#.#.#.........#.#",
         "#.#.#.#########.#",
         "#S#.............#",
         "#################"
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetLowestScore()
      {
         var result = Day16.GetLowestScore(testData);

         Assert.AreEqual(11048, result);
      }

      [Test]
      public void GetNumberOfTilesInAnyBestPath()
      {
         var result = Day16.GetNumberOfTilesInAnyBestPath(testData);

         Assert.AreEqual(64, result);
      }
   }
}
