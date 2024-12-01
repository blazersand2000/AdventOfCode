using AdventOfCode.Aoc2024.Day1;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day1Tests
   {
      string[] testData = new[]
      {
         "3   4",
         "4   3",
         "2   5",
         "1   3",
         "3   9",
         "3   3"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetTotalDistance()
      {
         var result = Day1.GetTotalDistance(testData);

         Assert.AreEqual(11, result);
      }

      [Test]
      public void GetSimilarityScore()
      {
         var result = Day1.GetSimilarityScore(testData);

         Assert.AreEqual(31, result);
      }
   }
}
