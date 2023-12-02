using AdventOfCode.Aoc2022;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
   class Program
   {
      static void Main(string[] args)
      {
         var services = new ServiceCollection();

         var problems = Assembly.GetExecutingAssembly().DefinedTypes.Where(ti => ti.ImplementedInterfaces.Contains(typeof(IProblem)));
         var mostRecentProblem = problems.OrderBy(p => int.Parse(p.Name.Substring(3))).Last();
         services.AddScoped(typeof(IProblem), mostRecentProblem);
         Console.WriteLine($"Running {mostRecentProblem.Name}\n", ConsoleColor.Blue);
         var serviceProvider = services.BuildServiceProvider();

         var problem = serviceProvider.GetService<IProblem>();
         problem.Run();

         Console.ReadKey();
      }
   }
}
