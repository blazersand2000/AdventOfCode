using AdventOfCode.Problems;
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
         Console.WriteLine("Hello World!");

         var problems = Assembly.GetExecutingAssembly().DefinedTypes.Where(ti => ti.ImplementedInterfaces.Contains(typeof(IProblem)));
         var mostRecentProblem = problems.OrderBy(p => p.Name.Substring(3)).Last();
         services.AddScoped(typeof(IProblem), mostRecentProblem);
         var serviceProvider = services.BuildServiceProvider();

         var problem = serviceProvider.GetService<IProblem>();
         problem.Run();

         Console.ReadKey();
      }
   }
}
