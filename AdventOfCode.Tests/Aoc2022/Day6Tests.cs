using AdventOfCode.Aoc2022.Day6;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tests.Aoc2022
{
   [TestFixture]
   class Day6Tests
   {
      private Day6 _day6;

      [SetUp]
      public void Setup()
      {
         _day6 = new Day6();
      }

      [Test]
      [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 4, 5)]
      [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 4, 6)]
      [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 4, 10)]
      [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 4, 11)]

      [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14, 19)]
      [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 14, 23)]
      [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 14, 23)]
      [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 14, 29)]
      [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 14, 26)]
      public void GetFirstMarkerCharacter_Tests(string buffer, int numChars, int expected)
      {
         var result = _day6.GetFirstMarkerCharacter(buffer, numChars);

         Assert.AreEqual(expected, result);
      }
   }
}
