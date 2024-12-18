using System.Collections.Generic;
using AdventOfCode.Aoc2024.Day15;
using NUnit.Framework;

namespace AdventOfCode.Tests.Aoc2024
{
   [TestFixture]
   class Day15Tests
   {
      private readonly string[] testData =
      [
         "##########",
         "#..O..O.O#",
         "#......O.#",
         "#.OO..O.O#",
         "#..O@..O.#",
         "#O#..O...#",
         "#O..O..O.#",
         "#.OO.O.OO#",
         "#....O...#",
         "##########",
         "",
         "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
         "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
         "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
         "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
         "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
         "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
         ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
         "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
         "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
         "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^"
      ];

      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void GetSumOfAllBoxGpsCoordinates()
      {
         var result = Day15.GetSumOfAllBoxGpsCoordinates(testData);

         Assert.AreEqual(10092, result);
      }

      [Test]
      public void GetSumOfAllBoxGpsCoordinatesWidened()
      {
         var result = Day15.GetSumOfAllBoxGpsCoordinatesWidened(testData);

         Assert.AreEqual(9021, result);
      }
   }
}
