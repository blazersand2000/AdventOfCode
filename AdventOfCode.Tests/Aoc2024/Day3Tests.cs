using AdventOfCode.Aoc2024.Day3;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day3Tests
   {
      string[] testData = ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfMultiplicationResults()
      {
         var result = Day3.GetSumOfMultiplicationResults(testData);

         Assert.AreEqual(161, result);
      }

      // [Test]
      // public void GetNumberOfSafeReportUsingDampener()
      // {
      //    var result = Day2.GetNumberOfSafeReportsUsingDampener(testData);

      //    Assert.AreEqual(4, result);
      // }
   }
}
