// <copyright file="AoCTestCases.cs" company="Endjin">
// Copyright (c) Endjin. All rights reserved.
// </copyright>

namespace AoC2019.Tests
{
    using AoC2019.Solutions;
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
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}