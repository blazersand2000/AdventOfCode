using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace AdventOfCode.Aoc2022.Day7
{
   public class Day7 : IProblem
   {
      private const int TotalDiskSpace = 70000000;
      private const int DiskSpaceRequiredForUpdate = 30000000;

      public void Run()
      {
         var lines = System.IO.File.ReadAllLines("Aoc2022/Day7/input.txt");

         var root = ParseFileSystem(lines);

         Console.WriteLine("Part 1:");
         var sum = GetAllDirectories(root).Where(d => d.Size <= 100_000).Sum(d => d.Size);
         Console.WriteLine(sum);

         Console.WriteLine("Part 2:");
         var availableDiskSpace = TotalDiskSpace - root.Size;
         var additionalSpaceNeeded = DiskSpaceRequiredForUpdate - availableDiskSpace;
         var candidateDirectories = GetAllDirectoriesOfMinimumSize(root, additionalSpaceNeeded);
         var directoryToDelete = candidateDirectories.OrderBy(d => d.Size).First();
         Console.WriteLine(directoryToDelete.Size);
      }

      public static IDirectory ParseFileSystem(IEnumerable<string> lines)
      {
         var commands = GetCommands(lines).SkipWhile(c => c.Name != "ls" && c.Argument != "/").Skip(1);

         var currentDirectory = new Directory("root", null) as IDirectory;
         foreach (var command in commands)
         {
            switch (command.Name)
            {
               case "cd":
                  currentDirectory = ChangeDirectory(currentDirectory, command.Argument);
                  break;
               case "ls":
                  ListDirectory(currentDirectory, command.Output);
                  break;
               default:
                  break;
            }
         }

         return currentDirectory.Root;
      }

      private static IDirectory ChangeDirectory(IDirectory currentDirectory, string argument)
      {
         switch (argument)
         {
            case "/":
               return currentDirectory.Root;
            case "..":
               return currentDirectory.Parent;
            default:
               return currentDirectory.Directories.FirstOrDefault(d => d.Name == argument) ?? throw new ApplicationException("Invalid directory");
         }
      }

      private static void ListDirectory(IDirectory currentDirectory, List<string> items)
      {
         foreach (var item in items)
         {
            if (item.StartsWith("dir"))
            {
               currentDirectory.Directories.Add(new Directory(item.Split(' ')[1], currentDirectory));
            }
            else
            {
               var file = item.Split(' ');
               currentDirectory.Files.Add(new File(file[1], int.Parse(file[0])));
            }
         }
      }

      private static List<Command> GetCommands(IEnumerable<string> lines)
      {
         var commands = new List<Command>();
         var first = lines.Take(1).First();
         var currentCommand = new Command(first);
         foreach (var line in lines.Skip(1))
         {
            if (line.StartsWith("$"))
            {
               commands.Add(currentCommand);
               currentCommand = new Command(line);
            }
            else
            {
               currentCommand.Output.Add(line);
            }
         }
         commands.Add(currentCommand);

         return commands;
      }

      private static IEnumerable<IDirectory> GetAllDirectories(IDirectory root)
      {
         return new List<IDirectory> { root }.Concat(root.Directories.SelectMany(d => GetAllDirectories(d)));
      }

      private static IEnumerable<IDirectory> GetAllDirectoriesOfMinimumSize(IDirectory root, int minimumSize)
      {
         if (root.Size >= minimumSize)
         {
            return new List<IDirectory> { root }.Concat(root.Directories.SelectMany(d => GetAllDirectoriesOfMinimumSize(d, minimumSize)));
         }

         return new List<IDirectory>();
      }

      struct Command
      {
         public string Name { get; set; }
         public string Argument { get; set; }
         public List<string> Output { get; set; }

         public Command(string line)
         {
            Name = ParseName(line);
            Argument = ParseArgument(line);
            Output = new List<string>();
         }

         public static string ParseName(string line)
         {
            return line.Split(' ')[1];
         }

         public static string ParseArgument(string line)
         {
            var parts = line.Split(' ');
            return parts.Length < 3 ? null : parts[2];
         }
      }
   }
}

