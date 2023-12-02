using AdventOfCode.Aoc2023.Day1;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day1Tests
   {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetNumericOrSpelledOutCalibrationValues_Tests()
      {
         var testData = new[]
         {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
         };

         var expected = new[]
         {
            29,
            83,
            13,
            24,
            42,
            14,
            76
         };

         var result = Day1.GetNumericOrSpelledOutCalibrationValues(testData);

         CollectionAssert.AreEqual(expected, result);
      }
   }
}
