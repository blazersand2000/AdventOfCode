using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day13;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day13Tests
   {
      private readonly string[] testData =
      [
         "Button A: X+94, Y+34",
         "Button B: X+22, Y+67",
         "Prize: X=8400, Y=5400",
         "",
         "Button A: X+26, Y+66",
         "Button B: X+67, Y+21",
         "Prize: X=12748, Y=12176",
         "",
         "Button A: X+17, Y+86",
         "Button B: X+84, Y+37",
         "Prize: X=7870, Y=6450",
         "",
         "Button A: X+69, Y+23",
         "Button B: X+27, Y+71",
         "Prize: X=18641, Y=10279"
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      [TestCase(94, 22, -8400, 34, 67, -5400, 80, 40)]
      //[TestCase(26, 67, -12748, 66, 21, -12176, 80, 40)]
      public void SolveSystem(int a1, int b1, int c1, int a2, int b2, int c2, double expectedX, double expectedY)
      {
         var first = new LinearEquation(a1, b1, c1);
         var second = new LinearEquation(a2, b2, c2);

         var result = Day13.SolveSystem(first, second);

         Assert.AreEqual(expectedX, result.X);
         Assert.AreEqual(expectedY, result.Y);
      }

      [Test]
      public void GetFewestTokensSpentToWinAllPrizes()
      {
         var result = Day13.GetFewestTokensSpentToWinAllPrizes(testData);

         Assert.AreEqual(480, result);
      }

      // [Test]
      // public void GetFewestTokensSpentToWinAllPrizes_PrizeOffsetByTenTrillion()
      // {
      //    var result = Day13.GetFewestTokensSpentToWinAllPrizes(testData, 10_000_000_000_000);

      //    Assert.AreEqual(480, result);
      // }
   }
}
