using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Problems.Day11
{
   public class Monkey
   {
      private readonly Queue<int> _items;
      private readonly Func<int, int> _operation;
      private readonly int _divisorTest;
      private readonly Dictionary<bool, int> _testResultMonkey;

      public int NumInspections { get; private set; } = 0;

      public Monkey(List<int> items, string operation, int divisorTest, int toMonkeyOnFalse, int toMonkeyOnTrue)
      {
         _items = new Queue<int>(items);
         _operation = GetOperation(operation).GetAwaiter().GetResult();
         _divisorTest = divisorTest;
         _testResultMonkey = new Dictionary<bool, int>
         {
            { false, toMonkeyOnFalse },
            { true, toMonkeyOnTrue },
         };
      }

      public Dictionary<int, Queue<int>> InspectAllItems()
      {
         var tossedItems = new Dictionary<int, Queue<int>>();

         while(_items.Count > 0)
         {
            var inspectedItem = Inspect();
            var monkey = GetMonkeyForItem(inspectedItem);
            if (!tossedItems.ContainsKey(monkey))
            {
               tossedItems[monkey] = new Queue<int>();
            }
            tossedItems[monkey].Enqueue(inspectedItem);
         }

         return tossedItems;
      }

      public void CatchItems(Queue<int> items)
      {
         foreach (var item in items)
         {
            _items.Enqueue(item);
         }
      }

      private int Inspect()
      {
         var item = _items.Dequeue();
         item = GetWorryLevelAfterInspectionAsync(item);
         item = GetWorryLevelAfterFeelingRelieved(item);
         NumInspections++;
         return item;
      }

      private int GetMonkeyForItem(int item)
      {
         var testResult = item % _divisorTest == 0;
         return _testResultMonkey[testResult];
      }

      private int GetWorryLevelAfterInspectionAsync(int item)
      {
         return _operation(item);
      }

      private static async Task<Func<int, int>> GetOperation(string operation)
      {
         try
         {
            var func = await CSharpScript.EvaluateAsync<Func<int, int>>($"old => {operation}");
            return func;
         }
         catch (Exception)
         {

            throw;
         }
      }

      private int GetWorryLevelAfterFeelingRelieved(int item)
      {
         return item / 3;
      }

      public class Globals
      {
         public int old { get; set; }
      }
   }
}
