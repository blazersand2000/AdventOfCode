using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
   class Program
   {
      static void Main(string[] args)
      {
         var allProblems = FindAllProblems();
         var groupedProblems = GroupProblemsByYear(allProblems);

         var problemToRun = args.Length == 2
            ? GetProblemFromCommandLineArgs(args, groupedProblems)
            : SelectMostRecentProblem(groupedProblems);

         var services = new ServiceCollection();
         services.AddScoped(typeof(IProblem), problemToRun.Type);
         Console.WriteLine($"Running {problemToRun.Year} - {problemToRun.Type.Name}\n", ConsoleColor.Blue);
         var serviceProvider = services.BuildServiceProvider();

         var problem = serviceProvider.GetService<IProblem>();
         problem.Run();
      }

      private static ProblemInfo SelectMostRecentProblem(Dictionary<int, Dictionary<int, ProblemInfo>> groupedProblems)
      {
         ProblemInfo problemToRun;
         // Select most recent problem
         var mostRecentYear = groupedProblems.Keys.Max();
         var mostRecentDay = groupedProblems[mostRecentYear].Keys.Max();
         problemToRun = groupedProblems[mostRecentYear][mostRecentDay];
         return problemToRun;
      }


      private static ProblemInfo GetProblemFromCommandLineArgs(string[] args, Dictionary<int, Dictionary<int, ProblemInfo>> groupedProblems)
      {
         ProblemInfo problemToRun;
         // Parse year and day from command line arguments
         var year = int.Parse(args[0]);
         var day = int.Parse(args[1]);

         // Select problem for given year and day
         problemToRun = groupedProblems[year][day];
         return problemToRun;
      }


      private static Dictionary<int, Dictionary<int, ProblemInfo>> GroupProblemsByYear(List<ProblemInfo> allProblems)
      {
         return allProblems
            .GroupBy(p => p.Year)
            .ToDictionary(
               g => g.Key,
               g => g.ToDictionary(p => p.Day, p => p));
      }


      private static List<ProblemInfo> FindAllProblems()
      {
         // Get all concrete types that implement IProblem
         var allProblems = Assembly.GetExecutingAssembly().DefinedTypes
             .Where(ti => ti.ImplementedInterfaces.Contains(typeof(IProblem)) && !ti.IsAbstract)
             .Select(ti => new ProblemInfo
             {
                Type = ti,
                Year = int.Parse(ti.Namespace.Split('.').First(ns => ns.StartsWith("Aoc")).Substring(3)), // Extract year from namespace
                Day = int.Parse(Regex.Match(ti.Name, @"\d+").Value) // Extract day from class name
             })
             .ToList();
         return allProblems;
      }

      private class ProblemInfo
      {
         public Type Type { get; set; }
         public int Year { get; set; }
         public int Day { get; set; }
      }
   }
}
