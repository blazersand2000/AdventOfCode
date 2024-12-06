using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdventOfCode.Aoc2024.Day5
{
   public class Day5 : Problem
   {
      public override void Run()
      {
         string[] lines = ReadInputFile();

         Console.WriteLine("Part 1:");
         Console.WriteLine(GetSumOfMiddlePagesOfCorrectlyOrderedUpdates(lines));

         Console.WriteLine("Part 2:");
         Console.WriteLine(GetSumOfMiddlePagesOfInCorrectlyOrderedUpdatesAfterOrdering(lines));
      }

      public static int GetSumOfMiddlePagesOfCorrectlyOrderedUpdates(string[] lines)
      {
         var (rules, updates) = ExtractRulesAndUpdates(lines);

         var correctlyOrderedUpdates = updates.Where(update => UpdateIsCorrectlyOrdered(rules, update, new List<int>()));

         return correctlyOrderedUpdates.Sum(update => update.ElementAt(update.Count / 2));
      }

      public static int GetSumOfMiddlePagesOfInCorrectlyOrderedUpdatesAfterOrdering(string[] lines)
      {
         var (rules, updates) = ExtractRulesAndUpdates(lines);

         var incorrectlyOrderedUpdates = updates.Where(update => !UpdateIsCorrectlyOrdered(rules, update, new List<int>()));

         var corrected = incorrectlyOrderedUpdates.Select(update => OrderPages(update, rules));

         return corrected.Sum(update => update.ElementAt(update.Count / 2));
      }

      private static List<int> OrderPages(List<int> update, List<Rule> rules)
      {
         Node tree = null;
         foreach (var page in update)
         {
            tree = InsertInOrder(page, tree, rules);
         }

         return TraverseInOrder(tree);
      }

      private static List<int> TraverseInOrder(Node tree)
      {
         if (tree == null)
         {
            return new List<int>();
         }

         return TraverseInOrder(tree.Left)
         .Concat(new List<int> { tree.Value })
         .Concat(TraverseInOrder(tree.Right))
         .ToList();
      }

      private static Node InsertInOrder(int page, Node node, List<Rule> rules)
      {
         if (node == null)
         {
            return new Node(page);
         }

         var rule = rules.Single(rule => (rule.A == page && rule.B == node.Value) || (rule.A == node.Value && rule.B == page));

         if (page == rule.A)
         {
            node.Left = InsertInOrder(page, node.Left, rules);
            return node;
         }

         node.Right = InsertInOrder(page, node.Right, rules);
         return node;
      }

      private static bool UpdateIsCorrectlyOrdered(List<Rule> rules, List<int> update, List<int> invalidPages)
      {
         if (update.Count == 0)
         {
            return true;
         }
         if (update.Count == 1)
         {
            return !invalidPages.Contains(update.Single());
         }

         var middleIndex = update.Count / 2;
         var middlePage = update.ElementAt(middleIndex);
         var invalidPagesForLeft = rules.Where(r => r.A == middlePage).Select(r => r.B).Concat(invalidPages).ToList();
         var invalidPagesForRight = rules.Where(r => r.B == middlePage).Select(r => r.A).Concat(invalidPages).ToList();
         return UpdateIsCorrectlyOrdered(rules, update[..middleIndex], invalidPagesForLeft) && UpdateIsCorrectlyOrdered(rules, update[middleIndex..], invalidPagesForRight);
      }

      private static (List<Rule> Rules, List<List<int>> Updates) ExtractRulesAndUpdates(string[] lines)
      {
         var rules = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).Select(l =>
         {
            var lineParts = l.Split('|');
            return new Rule(int.Parse(lineParts[0]), int.Parse(lineParts[1]));
         });

         var updates = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).Select(l =>
         {
            var lineParts = l.Split(',');
            return lineParts.Select(part => int.Parse(part)).ToList();
         });

         return (rules.ToList(), updates.ToList());
      }
   }

   public record struct Rule(int A, int B);

   public class Node
   {
      public int Value { get; set; }
      public Node Left { get; set; }
      public Node Right { get; set; }

      public Node(int value)
      {
         Value = value;
      }
   }
}
