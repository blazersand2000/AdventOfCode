using AdventOfCode.Aoc2023.Day3;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day3Tests
   {
      private readonly string[] testData = new[]
      {
         "467..114..",
         "...*......",
         "..35..633.",
         "......#...",
         "617*......",
         ".....+.58.",
         "..592.....",
         "......755.",
         "...$.*....",
         ".664.598.."
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfPartNumbers_Tests()
      {
         var expected = 4361;
         var schematic = Day3.ParseSchematic(testData);

         var result = Day3.GetSumOfPartNumbers(schematic);

         Assert.AreEqual(expected, result);
      }

      [Test]
      public void GetSumOfGearRatios_Tests()
      {
         var expected = 467835;
         var schematic = Day3.ParseSchematic(testData);

         var result = Day3.GetSumOfGearRatios(schematic);

         Assert.AreEqual(expected, result);
      }
   }
}
