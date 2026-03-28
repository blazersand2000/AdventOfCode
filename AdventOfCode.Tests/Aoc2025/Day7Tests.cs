using System;
using AdventOfCode.Aoc2025.Day7;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day7Tests
{
   readonly string[] testData =
   [
      ".......S.......",
      "...............",
      ".......^.......",
      "...............",
      "......^.^......",
      "...............",
      ".....^.^.^.....",
      "...............",
      "....^.^...^....",
      "...............",
      "...^.^...^.^...",
      "...............",
      "..^...^.....^..",
      "...............",
      ".^.^.^.^.^...^.",
      "..............."
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void Part1()
   {
      var result = Day7.Part1(testData);

      Assert.AreEqual(21, result);
   }

   [Test]
   public void Part2()
   {
      var result = Day7.Part2(testData);

      Assert.AreEqual(40, result);
   }
}
