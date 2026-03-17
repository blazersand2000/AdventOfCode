using AdventOfCode.Aoc2025.Day4;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day4Tests
{
   readonly string[] testData =
   [
      "..@@.@@@@.",
      "@@@.@.@.@@",
      "@@@@@.@.@@",
      "@.@@@@..@.",
      "@@.@@@@.@@",
      ".@@@@@@@.@",
      ".@.@.@.@@@",
      "@.@@@.@@@@",
      ".@@@@@@@@.",
      "@.@.@@@.@."
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GetCountOfAccessibleRolls()
   {
      var result = Day4.GetCountOfAccessibleRolls(testData);

      Assert.AreEqual(13, result);
   }

   [Test]
   public void GetTotalRemovals()
   {
      var result = Day4.GetTotalRemovals(testData);

      Assert.AreEqual(43, result);
   }
}
