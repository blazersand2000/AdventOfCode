using AdventOfCode.Aoc2025.Day3;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day3Tests
{
   readonly string[] testData =
   [
      "987654321111111",
      "811111111111119",
      "234234234234278",
      "818181911112111"
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GetTotalOutputJoltage_TwoDigits()
   {
      var result = Day3.GetTotalOutputJoltage(testData, 2);

      Assert.AreEqual(357, result);
   }

   [Test]
   public void GetTotalOutputJoltage_TwelveDigits()
   {
      var result = Day3.GetTotalOutputJoltage(testData, 12);

      Assert.AreEqual(3121910778619, result);
   }
}
