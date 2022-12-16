using AdventOfCode.Problems.Day11;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
   [TestFixture]
   class Day11Tests
   {
      [SetUp]
      public void Setup()
      {

      }

      [Test]
      public async Task Test()
      {
         var items = new List<int> { 79, 98 };
         var monkey = new Monkey(items, "old * 19", 23, 3, 2);
         var tossedItems = monkey.InspectAllItems();
      }
   }

}
