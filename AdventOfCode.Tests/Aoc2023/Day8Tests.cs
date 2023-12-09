using AdventOfCode.Aoc2023.Day8;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day8Tests
   {
      private readonly string[] testData = new[]
      {
         "LLR",
         "",
         "AAA = (BBB, BBB)",
         "BBB = (AAA, ZZZ)",
         "ZZZ = (ZZZ, ZZZ)",
      };

      private readonly string[] simultaneousTestData = new[]
      {
         "LR",
         "",
         "11A = (11B, XXX)",
         "11B = (XXX, 11Z)",
         "11Z = (11B, XXX)",
         "22A = (22B, XXX)",
         "22B = (22C, 22C)",
         "22C = (22Z, 22Z)",
         "22Z = (22B, 22B)",
         "XXX = (XXX, XXX)",
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetTotalSteps_Tests()
      {
         var expected = 6;
         var input = Day8.ParseInput(testData);

         var result = Day8.GetTotalSteps(input);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void GetTotalStepsSimultaneous_Tests()
      {
         var expected = 6;
         var input = Day8.ParseInput(simultaneousTestData);

         var result = Day8.GetTotalStepsSimultaneous(input);

         Assert.AreEqual(expected, result);
      }
   }
}