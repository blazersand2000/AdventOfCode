using System;
using AdventOfCode.Aoc2025.Day6;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day6Tests
{
   readonly string[] testData =
   [
      "123 328  51 64 ",
      " 45 64  387 23 ",
      "  6 98  215 314",
      "*   +   *   +  ",
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GetGrandTotal()
   {
      var result = Day6.GetGrandTotal(testData);

      Assert.AreEqual(4277556, result);
   }

   [Test]
   public void GetGrandTotalUsingCephalopodMath()
   {
      var result = Day6.GetGrandTotalUsingCephalopodMath(testData);

      Assert.AreEqual(3263827, result);
   }
}
