using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Problems.Day7
{
   public class Directory : IDirectory
   {
      public List<IDirectory> Directories { get; } = new List<IDirectory>();
      public List<IFile> Files { get; } = new List<IFile>();
      public string Name { get; }
      public IDirectory Parent { get; }
      public int Size => Directories.Concat<IFileSystemItem>(Files).Sum(x => x.Size);
      public bool IsRoot => Parent == null;
      public IDirectory Root => IsRoot ? this : Parent.Root;

      public Directory(string name, IDirectory parent)
      {
         Name = name;
         Parent = parent;
      }
   }
}
