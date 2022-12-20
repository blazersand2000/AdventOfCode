using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Problems.Day11
{
   public class Day11 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Problems/Day11/input.txt");
         var monkeyBusiness = GetMonkeyBusiness(lines);
         Console.WriteLine("Part 1:");
         Console.WriteLine(monkeyBusiness);

         Console.WriteLine("Part 2:");
      }

      private static int GetMonkeyBusiness(string[] lines)
      {
         var monkeys = ParseMonkeys(lines);

         for (int i = 0; i < 20; i++)
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

         var monkeyBusiness = monkeys.Select(x => x.Value.NumInspections).OrderByDescending(x => x).Take(2).Aggregate((a, b) => a * b);
         return monkeyBusiness;
      }

      private static Dictionary<int, Monkey> ParseMonkeys(string[] lines)
      {
         var monkeys = new Dictionary<int, Monkey>();
         var numMonkeys = Math.Ceiling(lines.Length / 7.0);

         for (int i = 0; i < numMonkeys; i++)
         {
            var startingIdx = i * 7;

            var items = new List<int>();
            var operation = string.Empty;
            var divisorTest = int.MinValue;
            var toMonkeyOnFalse = int.MinValue;
            var toMonkeyOnTrue = int.MinValue;

            items = lines[startingIdx + 1].Trim().Split("Starting items: ")[1].Split(", ").Select(x => int.Parse(x)).ToList();
            operation = lines[startingIdx + 2].Trim().Split("Operation: new = ")[1];
            divisorTest = int.Parse(lines[startingIdx + 3].Trim().Split("Test: divisible by ")[1]);
            toMonkeyOnTrue = int.Parse(lines[startingIdx + 4].Trim().Split("If true: throw to monkey ")[1]);
            toMonkeyOnFalse = int.Parse(lines[startingIdx + 5].Trim().Split("If false: throw to monkey ")[1]);

            monkeys[i] = new Monkey(items, operation, divisorTest, toMonkeyOnFalse, toMonkeyOnTrue);
         }

         return monkeys;
      }
   }
}

