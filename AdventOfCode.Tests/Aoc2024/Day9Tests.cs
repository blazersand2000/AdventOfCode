using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day9;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day9Tests
   {
      private readonly string testData = "2333133121414131402";

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void InitializeBlocks()
      {
         var result = Day9.InitializeBlocksFromMap(testData);

         List<int?> expected = [0, 0, null, null, null, 1, 1, 1, null, null, null, 2, null, null, null, 3, 3, 3,
            null, 4, 4, null, 5, 5, 5, 5, null, 6, 6, 6, 6, null, 7, 7, 7, null, 8, 8, 8, 8, 9, 9];

         CollectionAssert.AreEqual(expected, result);
      }

      [Test]
      public void RearrangeBlocks()
      {
         List<int?> blocks = [0, 0, null, null, null, 1, 1, 1, null, null, null, 2, null, null, null, 3, 3, 3,
            null, 4, 4, null, 5, 5, 5, 5, null, 6, 6, 6, 6, null, 7, 7, 7, null, 8, 8, 8, 8, 9, 9];

         List<int?> expected = [0, 0, 9, 9, 8, 1, 1, 1, 8, 8, 8, 2, 7, 7, 7, 3, 3, 3, 6, 4, 4, 6, 5, 5, 5, 5, 6, 6,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null];

         var result = Day9.RearrangeBlocks(blocks);

         CollectionAssert.AreEqual(expected, result);
      }

      [Test]
      public void RearrangeEntireFiles()
      {
         List<int?> blocks = [0, 0, null, null, null, 1, 1, 1, null, null, null, 2, null, null, null, 3, 3, 3,
            null, 4, 4, null, 5, 5, 5, 5, null, 6, 6, 6, 6, null, 7, 7, 7, null, 8, 8, 8, 8, 9, 9];

         List<int?> expected = [0, 0, 9, 9, 2, 1, 1, 1, 7, 7, 7, null, 4, 4, null, 3, 3, 3, null, null, null, null,
         5, 5, 5, 5, null, 6, 6, 6, 6, null, null, null, null, null, 8, 8, 8, 8, null, null];

         var result = Day9.RearrangeEntireFiles(blocks);

         CollectionAssert.AreEqual(expected, result);
      }

      [Test]
      public void GetFileSystemChecksum()
      {
         var result = Day9.GetFileSystemChecksum([testData]);

         Assert.AreEqual(1928, result);
      }

      [Test]
      public void GetFileSystemChecksumAfterRearrangingEntireFiles()
      {
         var result = Day9.GetFileSystemChecksumAfterRearrangingEntireFiles([testData]);

         Assert.AreEqual(2858, result);
      }
   }
}
