using AdventOfCode.Aoc2024.Day4;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day4Tests
   {
      private readonly string[] testData =
      {
         "MMMSXXMASM",
         "MSAMXMSMSA",
         "AMXSXMAAMM",
         "MSAMASMSMX",
         "XMASAMXAMM",
         "XXAMMXXAMA",
         "SMSMSASXSS",
         "SAXAMASAAA",
         "MAMMMXMMMM",
         "MXMXAXMASX"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetNumberOfWordMatches()
      {
         var result = Day4.GetNumberOfWordMatches(testData);

         Assert.AreEqual(18, result);
      }
   }
}
