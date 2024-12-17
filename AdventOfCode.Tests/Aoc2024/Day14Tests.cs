using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day14;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day14Tests
   {
      private readonly string[] testData =
      [
         "p=0,4 v=3,-3",
         "p=6,3 v=-1,-3",
         "p=10,3 v=-1,2",
         "p=2,0 v=2,-1",
         "p=0,0 v=1,3",
         "p=3,0 v=-2,-2",
         "p=7,6 v=-1,-3",
         "p=3,0 v=-1,-2",
         "p=9,3 v=2,3",
         "p=7,3 v=-1,2",
         "p=2,4 v=2,-3",
         "p=9,5 v=-3,-3"
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSafetyFactorAfter100Seconds()
      {
         var result = Day14.GetSafetyFactorAfter100Seconds(testData, new Vector(11, 7));

         Assert.AreEqual(12, result);
      }

   }
}
