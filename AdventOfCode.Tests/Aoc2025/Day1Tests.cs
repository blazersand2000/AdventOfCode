using AdventOfCode.Aoc2025.Day1;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day1Tests
{
   readonly string[] testData =
   [
      "L68",
      "L30",
      "R48",
      "L5",
      "R60",
      "L55",
      "L1",
      "L99",
      "R14",
      "L82"
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GetPassword()
   {
      var result = Day1.GetPassword(testData);

      Assert.AreEqual(3, result);
   }

   [Test]
   public void GetPasswordUsingMethod0x434C49434B()
   {
      var result = Day1.GetPasswordUsingMethod0x434C49434B(testData);

      Assert.AreEqual(6, result);
   }

   [Test]
   public void GetPasswordUsingMethod0x434C49434B_LandingOnZeroRotatingNegative()
   {
      string[] data = ["L50", "L5"];

      var result = Day1.GetPasswordUsingMethod0x434C49434B(data);

      Assert.AreEqual(1, result);
   }

}
