using AdventOfCode.Aoc2023.Day9;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day9Tests
   {
      private readonly string[] testData = new[]
      {
         "0 3 6 9 12 15",
         "1 3 6 10 15 21",
         "10 13 16 21 30 45"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      [TestCase(new long[] { 0, 3, 6, 9, 12, 15 }, false, 18)]
      [TestCase(new long[] { 1, 3, 6, 10, 15, 21, }, false, 28)]
      [TestCase(new long[] { 10, 13, 16, 21, 30, 45, }, false, 68)]
      [TestCase(new long[] { 0, 3, 6, 9, 12, 15 }, true, -3)]
      [TestCase(new long[] { 1, 3, 6, 10, 15, 21, }, true, 0)]
      [TestCase(new long[] { 10, 13, 16, 21, 30, 45, }, true, 5)]
      public void ExtrapolateNextValue_Tests(long[] history, bool backwards, long expected)
      {
         var result = Day9.ExtrapolateNextValue(history, backwards);

         Assert.AreEqual(expected, result);
      }

      [Test]
      [TestCase(false, 114)]
      [TestCase(true, 2)]
      public void GetSumOfExtrapolatedValues_Tests(bool backwards, long expected)
      {
         var histories = Day9.ParseHistories(testData);

         var result = Day9.GetSumOfExtrapolatedValues(histories, backwards);

         Assert.AreEqual(expected, result);
      }
   }
}