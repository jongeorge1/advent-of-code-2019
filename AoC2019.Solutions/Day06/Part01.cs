namespace AoC2019.Solutions.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var directOrbits = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(x => x[0])
                .ToDictionary(x => x.Key, x => x.Select(i => i[1]).ToArray());

            return directOrbits.Keys.Sum(k => this.CountAllOrbits(k, directOrbits)).ToString();
        }

        private int CountAllOrbits(string origin, Dictionary<string, string[]> directOrbits)
        {
            if (directOrbits.TryGetValue(origin, out string[] directOrbitsOfOrigin))
            {
                return directOrbitsOfOrigin.Length + directOrbitsOfOrigin.Sum(x => this.CountAllOrbits(x, directOrbits));
            }

            return 0;
        }
    }
}
