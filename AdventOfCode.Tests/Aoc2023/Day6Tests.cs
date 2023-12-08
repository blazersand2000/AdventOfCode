using AdventOfCode.Aoc2023.Day6;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day6Tests
   {
      private readonly string[] testData = new[]
      {
         "Time:      7  15   30",
         "Distance:  9  40  200"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetProductOfNumberOfWaysToBeatRecord_Tests()
      {
         var expected = 288;
         var races = Day6.ParseRaces(testData);

         var result = Day6.GetProductOfNumberOfWaysToBeatRecord(races);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void GetNumberOfWaysToBeatRecordSingleLongRace_Tests()
      {
         var expected = 71503;
         var races = Day6.ParseRaces(testData);

         var result = Day6.GetNumberOfWaysToBeatRecordSingleLongRace(races);

         Assert.AreEqual(expected, result);
      }
   }
}