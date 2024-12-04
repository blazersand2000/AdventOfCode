using AdventOfCode.Aoc2024.Day3;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day3Tests
   {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfMultiplicationResults()
      {
         string[] testData = ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];

         var result = Day3.GetSumOfMultiplicationResults(testData);

         Assert.AreEqual(161, result);
      }

      [Test]
      public void GetSumOfEnabledMultiplicationResults()
      {
         string[] testData = ["xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"];

         var result = Day3.GetSumOfEnabledMultiplicationResults(testData);

         Assert.AreEqual(48, result);
      }
   }
}
