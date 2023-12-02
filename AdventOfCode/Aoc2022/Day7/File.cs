namespace AdventOfCode.Aoc2022.Day7
{
   public class File : IFile
   {
      public string Name { get; }
      public int Size { get; }

      public File(string name, int size)
      {
         Name = name;
         Size = size;
      }
   }
}
