using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day10;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day10Tests
   {
      private readonly string[] testData =
      [
         "89010123",
         "78121874",
         "87430965",
         "96549874",
         "45678903",
         "32019012",
         "01329801",
         "10456732"
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfTrailheadScores()
      {
         var result = Day10.GetSumOfTrailheadScores(testData);

         Assert.AreEqual(36, result);
      }

      [Test]
      public void GetSumOfTrailheadRatings()
      {
         var result = Day10.GetSumOfTrailheadRatings(testData);

         Assert.AreEqual(81, result);
      }
   }
}
