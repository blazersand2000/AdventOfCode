using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day4
{
   public class Day4 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();
         var cards = ParseCards(lines);

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSumOfCardPoints(cards));

         // Console.WriteLine("Part 2:");
         // Console.WriteLine(part2);
      }

      public static long GetSumOfCardPoints(Dictionary<int, Card> cards)
      {
         return cards.Values.Sum(card => card.Points());
      }

      public static Dictionary<int, Card> ParseCards(string[] lines)
      {
         return lines.ToDictionary(line =>
            int.Parse(line.Split(':', StringSplitOptions.TrimEntries)[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]),
         line =>
         {
            var cardParts = line.Split(new[] { ':', '|' }, StringSplitOptions.TrimEntries);
            var winningNumbers = cardParts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            var numbersYouHave = cardParts[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            return new Card(winningNumbers, numbersYouHave);
         });
      }

      public readonly record struct Card(HashSet<string> WinningNumbers, HashSet<string> NumbersYouHave)
      {
         public int Points()
         {
            var numberOfMatches = WinningNumbers.Intersect(NumbersYouHave).Count();
            return numberOfMatches == 0 ? 0 : 1 << (numberOfMatches - 1);
         }
      }
   }
}
