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

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetTotalScratchCards(cards));
      }

      public static long GetSumOfCardPoints(Dictionary<int, Card> cards)
      {
         return cards.Values.Sum(card => card.Points);
      }

      public static long GetTotalScratchCards(Dictionary<int, Card> cards)
      {
         return cards.Sum(card => GetTotalCardsForCard(cards, card.Key));
      }

      public static long GetTotalCardsForCard(Dictionary<int, Card> cards, int cardNumber)
      {
         var numberOfMatches = cards[cardNumber].NumberOfMatches;
         return 1 + Enumerable.Range(1, numberOfMatches).Sum(i => GetTotalCardsForCard(cards, cardNumber + i));
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

      public readonly struct Card
      {
         public HashSet<string> WinningNumbers { get; }
         public HashSet<string> NumbersYouHave { get; }
         public int NumberOfMatches { get; }
         public int Points { get; }

         public Card(HashSet<string> winningNumbers, HashSet<string> numbersYouHave)
         {
            WinningNumbers = winningNumbers;
            NumbersYouHave = numbersYouHave;
            NumberOfMatches = WinningNumbers.Intersect(NumbersYouHave).Count();
            Points = NumberOfMatches == 0 ? 0 : 1 << (NumberOfMatches - 1);
         }
      }
   }
}
