using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdventOfCode.Aoc2024.Day11
{
   public class Day11 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(SimpleGetStones(25, lines).Count);

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetNumberOfStonesWithMemoization(75, lines));
      }

      public static long GetNumberOfStonesWithMemoization(int n, string[] lines)
      {
         var stones = ExtractStones(lines);

         return stones.Sum(stone => GetNumberOfStonesAfter(stone, n, []));
      }

      private static long GetNumberOfStonesAfter(string seed, int blinks, Dictionary<MemoKey, long> lookup)
      {
         if (blinks == 0)
         {
            return 1;
         }
         var key = new MemoKey(seed, blinks);
         if (lookup.TryGetValue(key, out var memoized))
         {
            return memoized;
         }

         var stones = Blink(seed);
         var numberOfStones = stones.Sum(stone => GetNumberOfStonesAfter(stone, blinks - 1, lookup));
         lookup[key] = numberOfStones;

         return numberOfStones;
      }

      public static List<string> SimpleGetStones(int n, string[] lines)
      {
         var stones = ExtractStones(lines);

         for (int i = 0; i < n; i++)
         {
            stones = stones.SelectMany(Blink).ToList();
         }

         return stones;
      }

      private static List<string> ExtractStones(string[] lines)
      {
         return lines[0].Split().ToList();
      }

      private static List<string> Blink(string stone)
      {
         return stone switch
         {
            "0" => ["1"],
            _ when stone.Length % 2 == 0 => Split(stone).ToList(),
            _ => [(long.Parse(stone) * 2024).ToString()]
         };
      }

      private static IEnumerable<string> Split(string stone)
      {
         var mid = stone.Length / 2;
         foreach (var half in new List<string> { stone[..mid], stone[mid..] })
         {
            // strip leading 0s
            yield return long.Parse(half).ToString();
         }
      }

   }

   public record struct MemoKey(string Seed, int Blinks);
}