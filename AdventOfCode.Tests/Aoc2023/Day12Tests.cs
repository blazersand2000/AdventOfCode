using AdventOfCode.Aoc2023.Day12;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tests.Aoc2023
{
   [TestFixture]
   class Day12Tests
   {
      private readonly string[] testData = new[]
      {
         "???.### 1,1,3",
         ".??..??...?##. 1,1,3",
         "?#?#?#?#?#?#?#? 1,3,1,6",
         "????.#...#... 4,1,1",
         "????.######..#####. 1,6,5",
         "?###???????? 3,2,1"
      };

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void ParseRecords_Tests()
      {
         var result = Day12.ParseRecords(testData);
         var expected = new List<Day12.Record>
         {
            new("???.###", new[] {1, 1, 3}),
            new(".??..??...?##.", new[] {1, 1, 3}),
            new("?#?#?#?#?#?#?#?", new[] {1, 3, 1, 6}),
            new("????.#...#...", new[] {4, 1, 1}),
            new("????.######..#####.", new[] {1, 6, 5}),
            new("?###????????", new[] {3, 2, 1})
         };

         CollectionAssert.AreEqual(expected, result);
      }

      [Test]
      [TestCase("#.#.###", new[] { 1, 1, 3 })]
      [TestCase(".#...#....###.", new[] { 1, 1, 3 })]
      [TestCase(".#.###.#.######", new[] { 1, 3, 1, 6 })]
      [TestCase("####.#...#...", new[] { 4, 1, 1 })]
      [TestCase("#....######..#####.", new[] { 1, 6, 5 })]
      [TestCase(".###.##....#", new[] { 3, 2, 1 })]
      public void UndamagedRecordIsValid_Tests(string springs, int[] damagedGroups)
      {
         var record = new Day12.Record(springs, damagedGroups);

         var result = Day12.UndamagedRecordIsValid(record);
         Assert.IsTrue(result);
      }

      [Test]
      [TestCase("???.###", new[] { 1, 1, 3 }, 1)]
      [TestCase(".??..??...?##.", new[] { 1, 1, 3 }, 4)]
      [TestCase("?#?#?#?#?#?#?#?", new[] { 1, 3, 1, 6 }, 1)]
      [TestCase("????.#...#...", new[] { 4, 1, 1 }, 1)]
      [TestCase("????.######..#####.", new[] { 1, 6, 5 }, 4)]
      [TestCase("?###????????", new[] { 3, 2, 1 }, 10)]
      public void GetPossibleArrangements_Tests(string springs, int[] damagedGroups, int expectedCount)
      {
         var record = new Day12.Record(springs, damagedGroups);

         var result = Day12.GetPossibleArrangements(record);
         Assert.AreEqual(expectedCount, result.Count);
      }

      [Test]
      [TestCase("???.###", "???.###", false)]
      [TestCase("###.##.", "???.###", false)]
      [TestCase("#.#.###", "???.###", true)]
      public static void CombinationValidForDamagedRecord_Tests(string combination, string damagedRecord, bool expected)
      {
         var result = Day12.CombinationValidForDamagedRecord(combination, damagedRecord);
         Assert.AreEqual(expected, result);
      }

      [Test]
      [TestCase(".", new[] { 1 }, 0)]
      [TestCase("#...", new[] { 1 }, 1)]
      [TestCase("#...#...", new[] { 1 }, 2)]
      [TestCase("#...#...", new[] { 1, 1 }, 1)]
      [TestCase("###", new[] { 3 }, 1)]
      [TestCase("???.###", new[] { 1, 1, 3 }, 1)]
      [TestCase("???.###", new[] { 1, 1, 3 }, 1)]
      [TestCase(".??..??...?##.", new[] { 1, 1, 3 }, 4)]
      [TestCase("?#?#?#?#?#?#?#?", new[] { 1, 3, 1, 6 }, 1)]
      [TestCase("????.#...#...", new[] { 4, 1, 1 }, 1)]
      [TestCase("????.######..#####.", new[] { 1, 6, 5 }, 4)]
      [TestCase("?###????????", new[] { 3, 2, 1 }, 10)]
      [TestCase("???????", new[] { 2, 1 }, 10)]
      [TestCase("???????", new[] { 2, 1 }, 10)]
      [TestCase("??????", new[] { 2, 1 }, 6)]
      [TestCase("?????", new[] { 2, 1 }, 3)]
      [TestCase("????", new[] { 2, 1 }, 1)]
      [TestCase("????????", new[] { 2, 1 }, 15)]
      [TestCase("?????????", new[] { 2, 1 }, 21)]
      [TestCase(".#?.#?.#?.#?.#", new[] { 1, 1, 1, 1, 1 }, 1)]
      [TestCase("???.###????.###????.###????.###????.###", new[] { 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 3 }, 1)]
      [TestCase(".??..??...?##.?.??..??...?##.?.??..??...?##.?.??..??...?##.?.??..??...?##.", new[] { 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 3 }, 16384)]
      [TestCase("?#?#?#?#?#?#?#???#?#?#?#?#?#?#???#?#?#?#?#?#?#???#?#?#?#?#?#?#???#?#?#?#?#?#?#?", new[] { 1, 3, 1, 6, 1, 3, 1, 6, 1, 3, 1, 6, 1, 3, 1, 6, 1, 3, 1, 6 }, 1)]
      [TestCase("????.#...#...?????.#...#...?????.#...#...?????.#...#...?????.#...#...", new[] { 4, 1, 1, 4, 1, 1, 4, 1, 1, 4, 1, 1, 4, 1, 1 }, 16)]
      [TestCase("????.######..#####.?????.######..#####.?????.######..#####.?????.######..#####.?????.######..#####.", new[] { 1, 6, 5, 1, 6, 5, 1, 6, 5, 1, 6, 5, 1, 6, 5 }, 2500)]
      [TestCase("?###??????????###??????????###??????????###??????????###????????", new[] { 3, 2, 1, 3, 2, 1, 3, 2, 1, 3, 2, 1, 3, 2, 1 }, 506250)]
      // [TestCase("?###??????", new[] { 3, 2, 1 }, 3)]
      // [TestCase("?###?????", new[] { 3, 2, 1 }, 1)]
      public void GetNumberOfPossibleArrangements_Tests(string springs, int[] damagedGroups, int expectedCount)
      {
         var record = new Day12.Record(springs, damagedGroups);

         var result = Day12.GetNumberOfPossibleArrangements(record);
         Assert.AreEqual(expectedCount, result);
      }
   }
}