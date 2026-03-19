using System;
using AdventOfCode.Aoc2025.Day5;
using NUnit.Framework;
using Range = AdventOfCode.Aoc2025.Day5.Day5.Range;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day5Tests
{
   readonly string[] testData =
   [
      "3-5",
      "10-14",
      "16-20",
      "12-18",
      "",
      "1",
      "5",
      "8",
      "11",
      "17",
      "32"
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GetCountOfFreshIngredients()
   {
      var result = Day5.GetCountOfAvailableFreshIngredients(testData);

      Assert.AreEqual(3, result);
   }

   [Test]
   public void GetCountOfIngredientsConsideredToBeFresh()
   {
      var result = Day5.GetCountOfIngredientsConsideredToBeFresh(testData);

      Assert.AreEqual(14, result);
   }

   [Test]
   [TestCase(10, 20, 30, 40, ExpectedResult = false)]
   [TestCase(10, 20, 20, 30, ExpectedResult = true)]
   [TestCase(10, 30, 20, 40, ExpectedResult = true)]
   [TestCase(20, 40, 10, 30, ExpectedResult = true)]
   [TestCase(20, 30, 10, 20, ExpectedResult = true)]
   [TestCase(30, 40, 10, 20, ExpectedResult = false)]
   [TestCase(10, 40, 20, 30, ExpectedResult = true)]
   [TestCase(20, 30, 10, 40, ExpectedResult = true)]
   public bool RangesOverlap(long aStart, long aEnd, long bStart, long bEnd)
   {
      return Day5.RangesOverlap(new Range(aStart, aEnd), new Range(bStart, bEnd));
   }
}
