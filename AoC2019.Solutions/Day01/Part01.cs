namespace AoC2019.Solutions.Day01
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .Select(this.CalculateFuelRequirement)
                .Sum()
                .ToString();
        }

        public int CalculateFuelRequirement(int mass)
        {
            return (mass / 3) - 2;
        }
    }
}
