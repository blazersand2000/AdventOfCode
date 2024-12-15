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

      [Test]
      public void GetTotalPriceOfFencingWithBulkDiscount()
      {
         var result = Day12.GetTotalPriceOfFencingWithBulkDiscount(testData);

         Assert.AreEqual(1206, result);
      }
   }
}
