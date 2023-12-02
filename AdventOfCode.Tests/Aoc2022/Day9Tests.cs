using AdventOfCode.Aoc2022.Day9;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tests.Aoc2022
{
   [TestFixture]
   class Day9Tests
   {
      [SetUp]
      public void Setup()
      {

      }

      [Test]
      public void GetUniquePositionsVisitedByTail_Correct()
      {
         var result = Day9.GetUniquePositionsVisitedByTail(GetSampleInput());

         Assert.AreEqual(13, result);
      }

      [Test]
      public void GetUniquePositionsVisitedByLastTail_TailDoesntMove_Correct()
      {
         var result = Day9.GetUniquePositionsVisitedByLastTail(GetSampleInput(), 10);

         Assert.AreEqual(1, result);
      }

      [Test]
      public void GetUniquePositionsVisitedByLastTail_TailMoves_Correct()
      {
         var moves = new string[]
         {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20",
         };

         var result = Day9.GetUniquePositionsVisitedByLastTail(moves, 10);

         Assert.AreEqual(36, result);
      }

      private string[] GetSampleInput()
      {
         return new string[]
         {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2",
         };
      }
   }
}
