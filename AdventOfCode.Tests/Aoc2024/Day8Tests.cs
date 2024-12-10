using AdventOfCode.Aoc2024.Day8;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day8Tests
   {
      private readonly string[] testData =
      {
         "............",
         "........0...",
         ".....0......",
         ".......0....",
         "....0.......",
         "......A.....",
         "............",
         "............",
         "........A...",
         ".........A..",
         "............",
         "............"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetNumberOfUniqueLocationsContainingAntinode()
      {
         var result = Day8.GetNumberOfUniqueLocationsContainingAntinode(testData);

         Assert.AreEqual(14, result);
      }

      [Test]
      public void GetNumberOfUniqueLocationsContainingAntinodeTakingIntoAccountEffectsOfHarmonics()
      {
         var result = Day8.GetNumberOfUniqueLocationsContainingAntinodeTakingIntoAccountEffectsOfHarmonics(testData);

         Assert.AreEqual(34, result);
      }
   }
}
