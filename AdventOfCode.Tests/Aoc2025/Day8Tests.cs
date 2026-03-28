using System;
using AdventOfCode.Aoc2025.Day8;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2025;

[TestFixture]
class Day8Tests
{
   readonly string[] testData =
   [
      "162,817,812",
      "57,618,57",
      "906,360,560",
      "592,479,940",
      "352,342,300",
      "466,668,158",
      "542,29,236",
      "431,825,988",
      "739,650,466",
      "52,470,668",
      "216,146,977",
      "819,987,18",
      "117,168,530",
      "805,96,715",
      "346,949,466",
      "970,615,88",
      "941,993,340",
      "862,61,35",
      "984,92,344",
      "425,690,689"
   ];

   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void SizeOf3LargestCircuitsMultipliedTogether()
   {
      var result = Day8.SizeOf3LargestCircuitsMultipliedTogether(testData, 10);

      Assert.AreEqual(40, result);
   }

   [Test]
   public void LastTwoJunctionBoxesXCoordinatesMultiplied()
   {
      var result = Day8.LastTwoJunctionBoxesXCoordinatesMultiplied(testData);

      Assert.AreEqual(25272, result);
   }
}
