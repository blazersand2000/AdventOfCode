using AdventOfCode.Aoc2023.Day2;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day2Tests
   {
      private readonly string[] testData = new[]
      {
         "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
         "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
         "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
         "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
         "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfPossibleGameIds_Tests()
      {
         var expected = 8;

         var result = Day2.GetSumOfPossibleGameIds(testData);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void GetSumOfMinimumGameCubesPowers_Tests()
      {
         var expected = 2286;

         var result = Day2.GetSumOfMinimumGameCubesPowers(testData);

         Assert.AreEqual(expected, result);
      }
   }
}
