namespace AoC2019.Solutions.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var directOrbits = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(x => x[1], x => x[0]);

            HashSet<string> youOrbitChain = this.BuildChain("YOU", directOrbits);
            HashSet<string> sanOrbitChain = this.BuildChain("SAN", directOrbits);

            youOrbitChain.SymmetricExceptWith(sanOrbitChain);
            return (youOrbitChain.Count - 2).ToString();
        }

        private HashSet<string> BuildChain(string origin, Dictionary<string, string> directOrbits)
        {
            var chain = new HashSet<string>() { origin };

            while (directOrbits.TryGetValue(origin, out origin))
            {
                chain.Add(origin);
            }

            return chain;
        }
    }
}
