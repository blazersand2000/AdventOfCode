using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day12;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day12Tests
   {
      private readonly string[] testData =
      [
         "RRRRIICCFF",
         "RRRRIICCCF",
         "VVRRRCCFFF",
         "VVRCCCJFFF",
         "VVVVCJJCFE",
         "VVIVCCJJEE",
         "VVIIICJJEE",
         "MIIIIIJJEE",
         "MIIISIJEEE",
         "MMMISSJEEE"
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetTotalPriceOfFencing()
      {
         var result = Day12.GetTotalPriceOfFencing(testData);

         Assert.AreEqual(1930, result);
      }

      // [TestCase(6, 22)]
      // [TestCase(25, 55312)]
      // public void GetNumberOfStonesWithMemoization(int numBlinks, long expected)
      // {
      //    var result = Day11.GetNumberOfStonesWithMemoization(numBlinks, testData);

      //    Assert.AreEqual(expected, result);
      // }
   }
}
