// <copyright file="AoCTestCases.cs" company="Endjin">
// Copyright (c) Endjin. All rights reserved.
// </copyright>

namespace AoC2019.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using AoC2019.Solutions;
    using AoC2019.Solutions.Day05;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "12", "2")]
        [TestCase(1, 1, "14", "2")]
        [TestCase(1, 1, "1969", "654")]
        [TestCase(1, 1, "100756", "33583")]
        [TestCase(1, 2, "14", "2")]
        [TestCase(1, 2, "1969", "966")]
        [TestCase(1, 2, "100756", "50346")]
        [TestCase(2, 1, "1,9,10,3,2,3,11,0,99,30,40,50", "3500")]
        [TestCase(2, 1, "1,0,0,0,99", "2")]
        [TestCase(2, 1, "2,3,0,3,99", "2")]
        [TestCase(2, 1, "2,4,4,5,99,0", "2")]
        [TestCase(2, 1, "1,1,1,4,99,5,6,0,99", "30")]
        [TestCase(3, 1, "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83", "159")]
        [TestCase(3, 1, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", "135")]
        [TestCase(3, 2, "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83", "610")]
        [TestCase(3, 2, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", "410")]
        [TestCase(4, 1, "111111-111111", "1")]
        [TestCase(4, 1, "223450-223450", "0")]
        [TestCase(4, 1, "123789-123789", "0")]
        [TestCase(4, 2, "112233-112233", "1")]
        [TestCase(4, 2, "123444-123444", "0")]
        [TestCase(4, 2, "111122-111122", "1")]
        [TestCase(6, 1, "COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L", "42")]
        [TestCase(6, 2, "COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L\r\nK)YOU\r\nI)SAN", "4")]
        [TestCase(7, 1, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "43210")]
        [TestCase(7, 1, "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", "54321")]
        [TestCase(7, 1, "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", "65210")]
        [TestCase(7, 2, "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", "139629729")]
        [TestCase(7, 2, "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", "18216")]
        [TestCase(8, 1, "123456789012", "1")]
        [TestCase(8, 2, "0222112222120000", " X\r\nX ")]
        [TestCase(9, 1, "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", "99")]
        [TestCase(9, 1, "1102,34915192,34915192,7,4,7,99,0", "1219070632396864")]
        [TestCase(9, 1, "104,1125899906842624,99", "1125899906842624")]
        [TestCase(10, 1, ".#..#\r\n.....\r\n#####\r\n....#\r\n...##", "8")]
        [TestCase(10, 1, "......#.#.\r\n#..#.#....\r\n..#######.\r\n.#.#.###..\r\n.#..#.....\r\n..#....#.#\r\n#..#....#.\r\n.##.#..###\r\n##...#..#.\r\n.#....####", "33")]
        [TestCase(10, 1, "#.#...#.#.\r\n.###....#.\r\n.#....#...\r\n##.#.#.#.#\r\n....#.#.#.\r\n.##..###.#\r\n..#...##..\r\n..##....##\r\n......#...\r\n.####.###.", "35")]
        [TestCase(10, 1, ".#..#..###\r\n####.###.#\r\n....###.#.\r\n..###.##.#\r\n##.##.#.#.\r\n....###..#\r\n..#.#..#.#\r\n#..#.#.###\r\n.##...##.#\r\n.....#.#..", "41")]
        [TestCase(10, 1, ".#..##.###...#######\r\n##.############..##.\r\n.#.######.########.#\r\n.###.#######.####.#.\r\n#####.##.#.##.###.##\r\n..#####..#.#########\r\n####################\r\n#.####....###.#.#.##\r\n##.#################\r\n#####.##.###..####..\r\n..######..##.#######\r\n####.##.####...##..#\r\n.#####..#.######.###\r\n##...#.##########...\r\n#.##########.#######\r\n.####.#.###.###.#.##\r\n....##.##.###..#####\r\n.#.#.###########.###\r\n#.#.#.#####.####.###\r\n###.##.####.##.#..##", "210")]
        [TestCase(10, 2, ".#..##.###...#######\r\n##.############..##.\r\n.#.######.########.#\r\n.###.#######.####.#.\r\n#####.##.#.##.###.##\r\n..#####..#.#########\r\n####################\r\n#.####....###.#.#.##\r\n##.#################\r\n#####.##.###..####..\r\n..######..##.#######\r\n####.##.####...##..#\r\n.#####..#.######.###\r\n##...#.##########...\r\n#.##########.#######\r\n.####.#.###.###.#.##\r\n....##.##.###..#####\r\n.#.#.###########.###\r\n#.#.#.#####.####.###\r\n###.##.####.##.#..##", "802")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 8, 1)]
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 1, 0)]
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 7, 0)]
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 9, 0)]
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 10, 0)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 0, 1)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 7, 1)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 8, 0)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 9, 0)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 0, 0)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 7, 0)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 8, 1)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 9, 0)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 0, 1)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 7, 1)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 8, 0)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 9, 0)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 0, 0)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 1, 1)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 1000, 1)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 0, 0)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 1, 1)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 1000, 1)]
        [TestCase(new[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 0, 999)]
        [TestCase(new[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 7, 999)]
        [TestCase(new[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 8, 1000)]
        [TestCase(new[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 9, 1001)]
        [TestCase(new[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 10000, 1001)]
        public void Day5IntCodeVmTests(int[] memory, int input, int expectedDiagnosticCode)
        {
            var vm = new IntCodeVm(memory);
            IEnumerable<int> output = vm.Execute(input);
            Assert.That(output.Last(), Is.EqualTo(expectedDiagnosticCode));
        }
    }
}