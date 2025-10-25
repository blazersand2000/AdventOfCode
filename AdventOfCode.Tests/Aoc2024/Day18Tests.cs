using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Aoc2024.Day18;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day18Tests
   {
      string[] _testData =
      [
         "5,4",
         "4,2",
         "4,5",
         "3,0",
         "2,1",
         "6,3",
         "2,4",
         "1,5",
         "0,6",
         "3,3",
         "2,6",
         "5,1",
         "1,2",
         "5,5",
         "2,5",
         "6,5",
         "1,4",
         "0,4",
         "6,4",
         "1,1",
         "6,1",
         "1,0",
         "0,5",
         "1,6",
         "2,0",
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetMinimumNumberOfSteps()
      {
         var result = Day18.GetMinimumNumberOfSteps(_testData, 6, 12);

         Assert.AreEqual(22, result);
      }

      [Test]
      public void GetCoordinateOfFirstByteBlockingExit()
      {
         var result = Day18.GetCoordinateOfFirstByteBlockingExit(_testData, 6, 0);

         Assert.AreEqual("6,1", result);
      }
   }
}
