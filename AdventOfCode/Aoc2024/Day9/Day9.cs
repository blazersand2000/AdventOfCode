using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace AdventOfCode.Aoc2024.Day9
{
   public class Day9 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetFileSystemChecksum(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetFileSystemChecksumAfterRearrangingEntireFiles(lines));

      }

      public static long GetFileSystemChecksum(string[] lines)
      {
         var blocks = InitializeBlocksFromMap(lines[0]);
         var rearranged = RearrangeBlocks(blocks.ToList());
         var checksum = CalculateChecksum(rearranged);
         return checksum;
      }

      public static long GetFileSystemChecksumAfterRearrangingEntireFiles(string[] lines)
      {
         var blocks = InitializeBlocksFromMap(lines[0]);
         var rearranged = RearrangeEntireFiles(blocks.ToList());
         var checksum = CalculateChecksum(rearranged);
         return checksum;
      }

      public static List<int?> RearrangeBlocks(List<int?> original)
      {
         var blocks = original.ToList();
         var to = 0;
         var from = blocks.Count - 1;

         while (from > to)
         {
            if (blocks[to] != null)
            {
               to++;
               continue;
            }
            if (blocks[from] == null)
            {
               from--;
               continue;
            }

            blocks[to] = blocks[from];
            blocks[from] = null;
            to++;
            from--;
         }

         return blocks;
      }

      public static List<int?> RearrangeEntireFiles(List<int?> original)
      {
         // I feel like I should have tried this using the map form.

         var blocks = original.ToList();

         for (int i = maxFileId(); i >= 0; i--)
         {
            var file = findFile(i);
            var to = firstIndexOfFreeSpace(file.Length);
            if (to > -1 && to < file.Index)
            {
               move(file.Index, to, file.Length);
            }
         }

         return blocks;

         void move(int from, int to, int blockSize)
         {
            for (int i = 0; i < blockSize; i++)
            {
               blocks[to + i] = blocks[from + i];
               blocks[from + i] = null;
            }
         }

         int firstIndexOfFreeSpace(int blockSize)
         {
            for (int i = 0, start = int.MinValue; i < blocks.Count; i++)
            {
               if (blocks[i] != null)
               {
                  start = int.MinValue;
               }
               else if (start == int.MinValue)
               {
                  start = i;
               }
               if (i - start + 1 == blockSize)
               {
                  return start;
               }
            }

            return -1;
         }

         (int Index, int Length) findFile(int fileId)
         {
            var index = blocks.IndexOf(fileId);
            var length = blocks.Where(b => b == fileId).Count();
            return (index, length);
         }

         int maxFileId() => blocks.Select(b => b ?? 0).Max();
      }

      public static IEnumerable<int?> InitializeBlocksFromMap(string diskMap)
      {
         for (int i = 0; i < diskMap.Length; i++)
         {
            var count = int.Parse(diskMap[i].ToString());
            if (i % 2 == 0)
            {
               foreach (var item in Enumerable.Repeat(i / 2, count))
               {
                  yield return item;
               }
            }
            else
            {
               foreach (var item in Enumerable.Repeat((int?)null, count))
               {
                  yield return item;
               }
            }
         }
      }

      private static long CalculateChecksum(List<int?> blocks)
      {
         return blocks.Select((id, position) => (long)position * (id ?? 0)).Sum();
      }
   }
}