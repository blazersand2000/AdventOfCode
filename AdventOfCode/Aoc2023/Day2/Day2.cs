using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day2
{
   public class Day2 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSumOfPossibleGameIds(lines));
      }

      public static long GetSumOfPossibleGameIds(string[] lines)
      {
         var games = ParseGames(lines);
         var possibleGames = games.Where(game => game.Rounds.Max(round => round.Red) <= 12 && game.Rounds.Max(round => round.Green) <= 13 && game.Rounds.Max(round => round.Blue) <= 14);
         return possibleGames.Sum(game => game.Id);
      }

      private static IEnumerable<Game> ParseGames(string[] lines)
      {
         return lines.Select(line =>
         {
            var gameParts = line.Split(':');
            var id = int.Parse(gameParts[0].Split(' ')[1]);
            var rounds = gameParts[1].Split(';').Select(round =>
            {
               var roundCounts = round.Split(',', StringSplitOptions.TrimEntries).ToDictionary(s => s.Split(' ')[1], s => int.Parse(s.Split(' ')[0]));
               return new BagContents(roundCounts.GetValueOrDefault("red", 0), roundCounts.GetValueOrDefault("green", 0), roundCounts.GetValueOrDefault("blue", 0));
            });

            return new Game(id, rounds.ToList().AsReadOnly());
         });

      }

      private readonly record struct Game(int Id, IReadOnlyList<BagContents> Rounds);

      private readonly record struct BagContents(int Red, int Green, int Blue);

   }
}
