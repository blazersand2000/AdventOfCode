using System.Collections.Generic;

namespace AdventOfCode.Problems.Day7
{
   public interface IDirectory : IFileSystemItem
   {
      List<IDirectory> Directories { get; }
      List<IFile> Files { get; }
      IDirectory Parent { get; }
      IDirectory Root { get; }
      bool IsRoot { get; }
   }
}
