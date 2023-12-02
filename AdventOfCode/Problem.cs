using System.IO;

namespace AdventOfCode;

public abstract class Problem : IProblem
{
   protected string[] ReadInputFile()
   {
      string namespacePath = GetType().Namespace.Replace('.', '/');
      string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
      string filePath = Path.Combine(solutionDirectory, $"{namespacePath}/input.txt");
      return File.ReadAllLines(filePath);
   }

   public abstract void Run();
}
