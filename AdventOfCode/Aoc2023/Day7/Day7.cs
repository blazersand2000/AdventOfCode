using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day7
{
   public class Day7 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();

         var regularHands = ParseHands(lines);
         var part1 = GetTotalWinnings(regularHands);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var jacksAreJokersHands = ParseHands(lines, true);
         var part2 = GetTotalWinnings(jacksAreJokersHands);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long GetTotalWinnings(IEnumerable<Hand> hands)
      {
         var orderedHands = hands.Order();
         // foreach (var (hand, rank) in orderedHands.Select((hand, index) => (hand, index + 1)))
         // {
         //    Console.WriteLine($"Rank: {rank}, Hand: {hand.Cards}, Bid: {hand.Bid}, Winnings: {hand.Bid * rank}");
         // }
         return hands.Order().Select((hand, index) => (long)hand.Bid * (index + 1)).Sum();
      }

      public static IEnumerable<Hand> ParseHands(IEnumerable<string> lines, bool jacksAreJokers = false)
      {
         return lines.Select(line =>
         {
            var parts = line.Split(' ');
            var cards = parts.First();
            var bid = int.Parse(parts.Last());
            return new Hand(cards, bid, jacksAreJokers);
         });
      }

      public readonly record struct Hand(string Cards, int Bid, bool JacksAreJokers = false) : IComparable<Hand>
      {
         public HandType HandType { get; } = DetermineHandType(Cards, JacksAreJokers);

         private static HandType DetermineHandType(string cards, bool jacksAreJokers)
         {
            var uniqueCardGroups = cards.GroupBy(c => c).Select(g => new { Card = g.Key, Count = g.Count() });

            if (jacksAreJokers && uniqueCardGroups.Any(c => c.Card == 'J'))
            {
               var jokerCount = uniqueCardGroups.First(c => c.Card == 'J').Count;
               if (jokerCount == 5)
               {
                  return HandType.FiveOfAKind;
               }
               uniqueCardGroups = uniqueCardGroups.Where(c => c.Card != 'J');
               var maxGroup = uniqueCardGroups.OrderByDescending(c => c.Count).First();
               var uniqueCardCounts = uniqueCardGroups.Select(g => g.Card == maxGroup.Card ? g.Count + jokerCount : g.Count);
               return GetHandTypeFromUniqueCardCounts(uniqueCardCounts);
            }

            return GetHandTypeFromUniqueCardCounts(uniqueCardGroups.Select(c => c.Count));

            static HandType GetHandTypeFromUniqueCardCounts(IEnumerable<int> uniqueCardCounts)
            {
               return uniqueCardCounts.Count() switch
               {
                  5 => HandType.HighCard,
                  4 => HandType.OnePair,
                  3 => uniqueCardCounts.Any(c => c == 3) ? HandType.ThreeOfAKind : HandType.TwoPair,
                  2 => uniqueCardCounts.Any(c => c == 4) ? HandType.FourOfAKind : HandType.FullHouse,
                  1 => HandType.FiveOfAKind,
                  _ => throw new InvalidOperationException("Invalid hand"),
               };
            }
         }

         public int CompareTo(Hand other)
         {
            if (HandType < other.HandType)
            {
               return -1;
            }
            if (HandType > other.HandType)
            {
               return 1;
            }
            var firstDifferentCard = Cards.Zip(other.Cards).FirstOrDefault(c => c.First != c.Second);
            if (firstDifferentCard != default)
            {
               return GetCardValue(firstDifferentCard.First) - GetCardValue(firstDifferentCard.Second) < 0 ? -1 : 1;
            }
            return 0;
         }

         private int GetCardValue(char card)
         {
            // Values for face cards are arbitrary as long as they are ordered relative to each other and to the underlying integer values of the number cards
            return card switch
            {
               'A' => 100004,
               'K' => 100003,
               'Q' => 100002,
               'J' => JacksAreJokers ? 1 : 100001,
               'T' => 100000,
               _ => card
            };
         }
      }

      public enum HandType
      {
         HighCard,
         OnePair,
         TwoPair,
         ThreeOfAKind,
         FullHouse,
         FourOfAKind,
         FiveOfAKind
      }
   }
}
