using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Problems.Day11
{
   public class Monkey
   {
      private readonly Queue<long> _items;
      private readonly Func<long, long> _operation;
      private readonly int _divisorTest;
      private readonly Dictionary<bool, int> _testResultMonkey;
      private readonly long? _mod;

      public int NumInspections { get; private set; } = 0;

      public Monkey(List<long> items, string operation, int divisorTest, int toMonkeyOnFalse, int toMonkeyOnTrue, long? mod = null)
      {
         _items = new Queue<long>(items);
         _operation = GetOperation(operation).GetAwaiter().GetResult();
         _divisorTest = divisorTest;
         _testResultMonkey = new Dictionary<bool, int>
         {
            { false, toMonkeyOnFalse },
            { true, toMonkeyOnTrue },
         };
         _mod = mod;
      }

      public Dictionary<int, Queue<long>> InspectAllItems()
      {
         var tossedItems = new Dictionary<int, Queue<long>>();

         while(_items.Count > 0)
         {
            var inspectedItem = Inspect();
            var monkey = GetMonkeyForItem(inspectedItem);
            if (!tossedItems.ContainsKey(monkey))
            {
               tossedItems[monkey] = new Queue<long>();
            }
            tossedItems[monkey].Enqueue(inspectedItem);
         }

         return tossedItems;
      }

      public void CatchItems(Queue<long> items)
      {
         foreach (var item in items)
         {
            _items.Enqueue(item);
         }
      }

      private long Inspect()
      {
         var item = _items.Dequeue();
         item = GetWorryLevelAfterInspectionAsync(item);
         item = GetWorryLevelAfterFeelingRelieved(item);
         NumInspections++;
         return item;
      }

      private int GetMonkeyForItem(long item)
      {
         var testResult = item % _divisorTest == 0;
         return _testResultMonkey[testResult];
      }

      private long GetWorryLevelAfterInspectionAsync(long item)
      {
         return _operation(item);
      }

      private static async Task<Func<long, long>> GetOperation(string operation)
      {
         try
         {
            var func = await CSharpScript.EvaluateAsync<Func<long, long>>($"old => {operation}");
            return func;
         }
         catch (Exception)
         {

            throw;
         }
      }

      private long GetWorryLevelAfterFeelingRelieved(long item)
      {
         return _mod == null ? item / 3 : item % (long)_mod;
      }

      public class Globals
      {
         public int old { get; set; }
      }
   }
}
