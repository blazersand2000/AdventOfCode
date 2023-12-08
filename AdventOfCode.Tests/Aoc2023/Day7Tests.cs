using AdventOfCode.Aoc2023.Day7;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day7Tests
   {
      private readonly string[] testData = new[]
      {
         "32T3K 765",
         "T55J5 684",
         "KK677 28",
         "KTJJT 220",
         "QQQJA 483"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetTotalWinnings_RegularHands_Tests()
      {
         var expected = 6440;
         var hands = Day7.ParseHands(testData);

         var result = Day7.GetTotalWinnings(hands);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void GetTotalWinnings_JacksAreJokers_Tests()
      {
         var expected = 5905;
         var hands = Day7.ParseHands(testData, true);

         var result = Day7.GetTotalWinnings(hands);

         Assert.AreEqual(expected, result);
      }
   }
}