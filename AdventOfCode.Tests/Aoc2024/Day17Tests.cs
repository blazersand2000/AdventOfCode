using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day17;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day17Tests
   {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetOutput()
      {
         string[] testData =
         [
            "Register A: 729",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,1,5,4,3,0"
         ];

         var result = Day17.GetOutput(testData);

         Assert.AreEqual("4,6,3,5,6,3,5,2,1,0", result);
      }

      [Test]
      public void GetLowestRegisterAThatProducesCopyOfItself()
      {
         string[] testData =
         [
            "Register A: 2024",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,3,5,4,3,0"
         ];

         var result = Day17.GetLowestRegisterAThatProducesCopyOfItself(testData);

         Assert.AreEqual(117440, result);
      }
   }
}
