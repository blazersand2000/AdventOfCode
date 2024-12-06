using AdventOfCode.Aoc2024.Day5;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day5Tests
   {
      private readonly string[] testData =
      {
         "47|53",
         "97|13",
         "97|61",
         "97|47",
         "75|29",
         "61|13",
         "75|53",
         "29|13",
         "97|29",
         "53|29",
         "61|53",
         "97|53",
         "61|29",
         "47|13",
         "75|47",
         "97|75",
         "47|61",
         "75|61",
         "47|29",
         "75|13",
         "53|13",
         "",
         "75,47,61,53,29",
         "97,61,53,29,13",
         "75,29,13",
         "75,97,47,61,53",
         "61,13,29",
         "97,13,75,29,47"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfMiddlePagesOfCorrectlyOrderedUpdates()
      {
         var result = Day5.GetSumOfMiddlePagesOfCorrectlyOrderedUpdates(testData);

         Assert.AreEqual(143, result);
      }

      [Test]
      public void GetSumOfMiddlePagesOfInCorrectlyOrderedUpdatesAfterOrdering()
      {
         var result = Day5.GetSumOfMiddlePagesOfInCorrectlyOrderedUpdatesAfterOrdering(testData);

         Assert.AreEqual(123, result);
      }
   }
}
