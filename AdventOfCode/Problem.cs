using System.IO;
using System.Reflection;

namespace AdventOfCode;

public abstract class Problem : IProblem
{
   protected string[] ReadInputFile()
   {
      string namespacePath = GetType().Namespace.Replace('.', '/');
      string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      string projectDirectory = Directory.GetParent(assemblyLocation).Parent.Parent.Parent.FullName;
      string filePath = Path.Combine(projectDirectory, $"{namespacePath}/input.txt");
      return File.ReadAllLines(filePath);
   }

   public abstract void Run();
}
