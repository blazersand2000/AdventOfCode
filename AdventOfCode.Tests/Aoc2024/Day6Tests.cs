using AdventOfCode.Aoc2024.Day6;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day6Tests
   {
      private readonly string[] testData =
      {
         "....#.....",
         ".........#",
         "..........",
         "..#.......",
         ".......#..",
         "..........",
         ".#..^.....",
         "........#.",
         "#.........",
         "......#..."
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetCountOfDistinctGuardPositions()
      {
         var result = Day6.GetCountOfDistinctGuardPositions(testData);

         Assert.AreEqual(41, result);
      }

      [Test]
      public void GetCountOfObstructionPositions()
      {
         var result = Day6.GetCountOfObstructionPositions(testData);

         Assert.AreEqual(6, result);
      }
   }
}
