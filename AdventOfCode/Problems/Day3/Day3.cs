using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Problems.Day2
{
   public class Day3 : IProblem
   {
      public void Run()
      {
         var lines = File.ReadAllLines("Problems/Day3/input.txt");

         Part1(lines);
         Part2(lines);
      }

      private void Part1(string[] lines)
      {
         var prioritySum = 0;

         foreach (var line in lines)
         {
            var itemType = GetItemTypeAppearingInBothCompartments(line);
            prioritySum += GetPriority(itemType);
         }

         Console.WriteLine("Part 1:");
         Console.WriteLine(prioritySum);
      }

      private void Part2(string[] lines)
      {
         var prioritySum = 0;
         const int groupSize = 3;

         if (lines.Length % 3 != 0)
         {
            throw new ApplicationException("Lines not divisible by 3.");
         }

         var badge = char.MinValue;
         for (int i = 0; i < lines.Length; i+= groupSize)
         {
            badge = GetGroupBadge(new[] { lines[i], lines[i + 1], lines[i + 2] });
            prioritySum += GetPriority(badge);
         }

         Console.WriteLine("Part 2:");
         Console.WriteLine(prioritySum);
      }

      private int GetPriority(char itemType)
      {
         switch ((int)itemType)
         {
            case int n when (n >= 65 && n <= 90):
               return n - (65 - 27);
            case int n when (n >= 97 && n <= 122):
               return n - (97 - 1);
            default:
               throw new ApplicationException($"Unexpected input: {itemType}");
         }
      }

      private char GetItemTypeAppearingInBothCompartments(string rucksack)
      {
         if (rucksack.Length % 2 != 0)
         {
            throw new ApplicationException("Invalid rucksack has an odd number of items.");
         }

         var firstCompartment = rucksack.Take(rucksack.Length / 2).ToHashSet();
         var secondCompartment = rucksack.TakeLast(rucksack.Length / 2).ToHashSet();

         var sharedItemTypes = firstCompartment.Intersect(secondCompartment);

         if (sharedItemTypes.Count() > 1)
         {
            throw new ApplicationException("Found more than one item type shared between both compartments in rucksack.");
         }

         return sharedItemTypes.First();
      }

      private char GetGroupBadge(string[] rucksacks)
      {
         var sharedItemTypes = rucksacks.Select(r => r.ToList()).AsParallel().Aggregate((a, b) => a.Intersect(b).ToList());
         
         if (sharedItemTypes.Count() > 1)
         {
            throw new ApplicationException("Found more than one item type shared between both compartments in rucksack.");
         }

         return sharedItemTypes.First();
      }
   }
}

