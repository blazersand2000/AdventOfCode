using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Aoc2023.Day8
{
   public class Day8 : Problem
   {
      public override void Run()
      {
         var lines = ReadInputFile();
         var input = ParseInput(lines);

         var part1 = GetTotalSteps(input);
         Console.WriteLine("Part 1:");
         Console.WriteLine(part1);

         var part2 = GetTotalStepsSimultaneous(input);
         Console.WriteLine("Part 2:");
         Console.WriteLine(part2);
      }

      public static long GetTotalSteps(Input input)
      {
         return FindNode("ZZZ", "AAA", input);
      }

      public static long GetTotalStepsSimultaneous(Input input)
      {
         return FindSimultaneousNodes('Z', 'A', input);
      }

      private static long FindNode(string nodeToFind, string currentNode, Input input, long currentStep = 0)
      {
         if (currentNode == nodeToFind)
         {
            return currentStep;
         }
         var nextNode = input.GetInstruction(currentStep) == 'L' ? input.Network[currentNode].Left : input.Network[currentNode].Right;
         return FindNode(nodeToFind, nextNode, input, currentStep + 1);
      }

      private static long FindNode(string nodeToFind, string currentNode, Input input)
      {
         long currentStep = 0;
         while (currentNode != nodeToFind)
         {
            var nextNode = input.GetInstruction(currentStep) == 'L' ? input.Network[currentNode].Left : input.Network[currentNode].Right;
            currentNode = nextNode;
            currentStep++;
         }
         return currentStep;
      }

      private static long FindSimultaneousNodes(char lastCharOfNodesToFind, char lastCharOfStartingNodes, Input input)
      {
         var currentNodes = input.Network.Keys.Where(k => k.Last() == lastCharOfStartingNodes).ToList();
         var cycles = currentNodes.Select(n => GetCycle(lastCharOfNodesToFind, n, input)).OrderByDescending(c => c.CycleLength).ToList();
         var longest = cycles.First();
         var remaining = cycles.Skip(1).ToList();
         var largestCycleNumber = 0;
         while (true)
         {
            foreach (var number in longest.Hits.Select(h => largestCycleNumber * longest.CycleLength + h))
            {
               if (remaining.All(r => r.Hits.Any(h => (number - h) % r.CycleLength == 0)))
               {
                  return number;
               }
            }
            largestCycleNumber++;
         }
      }

      private static (long CycleLength, IEnumerable<long> Hits) GetCycle(char lastCharOfNodesToFind, string startingNode, Input input)
      {
         long currentStep = 0;
         var currentNode = startingNode;
         var stepsOfFoundNodes = new HashSet<long>();
         while (true)
         {
            currentStep++;
            currentNode = input.GetInstruction(currentStep - 1) == 'L' ? input.Network[currentNode].Left : input.Network[currentNode].Right;
            if (currentNode.Last() == lastCharOfNodesToFind)
            {
               var firstFoundNode = stepsOfFoundNodes.Order().FirstOrDefault();
               if (firstFoundNode != default && (currentStep - firstFoundNode) % input.Instructions.Length == 0)
               {
                  return (currentStep - firstFoundNode, stepsOfFoundNodes);
               }
               stepsOfFoundNodes.Add(currentStep);
            }
         }
      }

      private static long FindSimultaneousNodesBruteForce(char lastCharOfNodesToFind, char lastCharOfStartingNodes, Input input)
      {
         var currentNodes = input.Network.Keys.Where(k => k.Last() == lastCharOfStartingNodes).ToList();
         long currentStep = 0;
         while (!currentNodes.All(n => n.Last() == lastCharOfNodesToFind))
         {
            currentNodes = currentNodes.Select(n => input.GetInstruction(currentStep) == 'L' ? input.Network[n].Left : input.Network[n].Right).ToList();
            currentStep++;
         }
         return currentStep;
      }

      public static Input ParseInput(IEnumerable<string> lines)
      {
         var instructions = lines.First();
         var network = lines.Skip(2).ToDictionary(line => line[..3], line => new Edges(line[7..10], line[12..15]));
         return new Input(network, instructions);
      }

      public readonly record struct Edges(string Left, string Right);

      public readonly record struct Input(Dictionary<string, Edges> Network, string Instructions)
      {
         public char GetInstruction(long step)
         {
            return Instructions[(int)(step % Instructions.Length)];
         }
      }
   }
}
