using AdventOfCode.Aoc2024.Day2;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day2Tests
   {
      string[] testData = new[]
      {
         "7 6 4 2 1",
         "1 2 7 8 9",
         "9 7 6 2 1",
         "1 3 2 4 5",
         "8 6 4 4 1",
         "1 3 6 7 9"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetNumberOfSafeReports()
      {
         var result = Day2.GetNumberOfSafeReports(testData);

         Assert.AreEqual(2, result);
      }
   }
}
