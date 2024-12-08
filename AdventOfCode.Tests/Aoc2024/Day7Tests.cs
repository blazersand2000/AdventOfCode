using AdventOfCode.Aoc2024.Day7;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day7Tests
   {
      private readonly string[] testData =
      {
         "190: 10 19",
         "3267: 81 40 27",
         "83: 17 5",
         "156: 15 6",
         "7290: 6 8 6 15",
         "161011: 16 10 13",
         "192: 17 8 14",
         "21037: 9 7 18 13",
         "292: 11 6 16 20"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetCalibrationResult()
      {
         var result = Day7.GetCalibrationResult(testData);

         Assert.AreEqual(3749, result);
      }

      [Test]
      public void GetCalibrationResultIncludingConcatenation()
      {
         var result = Day7.GetCalibrationResultIncludingConcatenation(testData);

         Assert.AreEqual(11387, result);
      }
   }
}
