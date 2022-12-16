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
      private readonly string _operation;
      private readonly int _divisorTest;
      private readonly Dictionary<bool, int> _testResultMonkey;

      public Monkey(List<int> items, string operation, int divisorTest, int toMonkeyOnFalse, int toMonkeyOnTrue)
      {
         _items = new Queue<int>(items);
         _operation = operation;
         _divisorTest = divisorTest;
         _testResultMonkey = new Dictionary<bool, int>
         {
            { false, toMonkeyOnFalse },
            { true, toMonkeyOnTrue },
         };
      }

      public async Task<Dictionary<int, Queue<int>>> InspectAllItems()
      {
         var tossedItems = new Dictionary<int, Queue<int>>();

         while(_items.Count > 0)
         {
            var inspectedItem = await Inspect();
            var monkey = GetMonkeyForItem(inspectedItem);
            if (!tossedItems.ContainsKey(monkey))
            {
               tossedItems[monkey] = new Queue<int>();
            }
            tossedItems[monkey].Enqueue(inspectedItem);
         }

         return tossedItems;
      }

      private async Task<int> Inspect()
      {
         var item = _items.Dequeue();
         item = await GetWorryLevelAfterInspectionAsync(item);
         item = GetWorryLevelAfterFeelingRelieved(item);
         return item;
      }

      private int GetMonkeyForItem(int item)
      {
         var testResult = item % _divisorTest == 0;
         return _testResultMonkey[testResult];
      }

      private async Task<int> GetWorryLevelAfterInspectionAsync(int item)
      {
         try
         {
            var result = await CSharpScript.EvaluateAsync<int>(_operation, globals: new Globals { old = item });
            return result;
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
