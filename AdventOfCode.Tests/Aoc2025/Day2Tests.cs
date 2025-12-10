using AdventOfCode.Aoc2025.Day2;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day2Tests
{
   readonly string testData = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GetSumOfInvalidIds()
   {
      var result = Day2.GetSumOfInvalidIds(testData);

      Assert.AreEqual(1227775554, result);
   }

   [Test]
   public void GetSumOfInvalidIdsUsingNewRules()
   {
      var result = Day2.GetSumOfInvalidIdsUsingNewRules(testData);

      Assert.AreEqual(4174379265, result);
   }
}
