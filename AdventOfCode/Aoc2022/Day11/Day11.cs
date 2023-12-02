using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Aoc2022.Day11
{
   public class Day11 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Aoc2022/Day11/input.txt");
         var monkeyBusiness = GetMonkeyBusiness(lines, 20, true);
         Console.WriteLine("Part 1:");
         Console.WriteLine(monkeyBusiness);

         monkeyBusiness = GetMonkeyBusiness(lines, 10000, false);
         Console.WriteLine("Part 2:");
         Console.WriteLine(monkeyBusiness);
      }

      private static long GetMonkeyBusiness(string[] lines, int numRounds, bool worryReduces)
      {
         var monkeys = ParseMonkeys(lines, worryReduces);

         for (int i = 0; i < numRounds; i++)
         {
            foreach (var monkey in monkeys)
            {
               var thrownItems = monkey.Value.InspectAllItems();

               foreach (var receivingMonkey in thrownItems)
               {
                  monkeys[receivingMonkey.Key].CatchItems(receivingMonkey.Value);
               }
            }
         }

         var monkeyBusiness = monkeys.Select(x => (long)x.Value.NumInspections).OrderByDescending(x => x).Take(2).Aggregate((a, b) => a * b);
         return monkeyBusiness;
      }

      private static Dictionary<int, Monkey> ParseMonkeys(string[] lines, bool worryReduces)
      {
         var monkeys = new Dictionary<int, Monkey>();
         var numMonkeys = Math.Ceiling(lines.Length / 7.0);
         long? mod = null;
         mod = worryReduces ? mod : lines.Where((line, index) => index % 7 == 3).Select(x => long.Parse(x.Trim().Split("Test: divisible by ")[1])).Aggregate((a, b) => a * b);

         for (int i = 0; i < numMonkeys; i++)
         {
            var startingIdx = i * 7;

            var items = new List<long>();
            var operation = string.Empty;
            var divisorTest = int.MinValue;
            var toMonkeyOnFalse = int.MinValue;
            var toMonkeyOnTrue = int.MinValue;

            items = lines[startingIdx + 1].Trim().Split("Starting items: ")[1].Split(", ").Select(x => long.Parse(x)).ToList();
            operation = lines[startingIdx + 2].Trim().Split("Operation: new = ")[1];
            divisorTest = int.Parse(lines[startingIdx + 3].Trim().Split("Test: divisible by ")[1]);
            toMonkeyOnTrue = int.Parse(lines[startingIdx + 4].Trim().Split("If true: throw to monkey ")[1]);
            toMonkeyOnFalse = int.Parse(lines[startingIdx + 5].Trim().Split("If false: throw to monkey ")[1]);

            monkeys[i] = new Monkey(items, operation, divisorTest, toMonkeyOnFalse, toMonkeyOnTrue, mod);
         }

         return monkeys;
      }
   }
}

