using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day11;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day11Tests
   {
      private readonly string[] testData =
      [
         "125 17",
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void SimpleGetStones()
      {
         var expected = new List<string>() { "2097446912", "14168", "4048", "2", "0", "2", "4", "40", "48", "2024", "40", "48", "80", "96", "2", "8", "6", "7", "6", "0", "3", "2" };

         var result = Day11.SimpleGetStones(6, testData);

         CollectionAssert.AreEqual(expected, result);
      }

      [TestCase(6, 22)]
      [TestCase(25, 55312)]
      public void GetNumberOfStonesWithMemoization(int numBlinks, long expected)
      {
         var result = Day11.GetNumberOfStonesWithMemoization(numBlinks, testData);

         Assert.AreEqual(expected, result);
      }
   }
}
