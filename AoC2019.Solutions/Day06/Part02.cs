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

            List<string> youOrbitChain = this.BuildChain("YOU", directOrbits);
            List<string> sanOrbitChain = this.BuildChain("SAN", directOrbits);
  
            // Find the first common ancestor
            int i = 0;
            while (youOrbitChain[i] == sanOrbitChain[i])
            {
                i++;
            }

            return (youOrbitChain.Count - i + sanOrbitChain.Count - i - 2).ToString();
        }

        private List<string> BuildChain(string origin, Dictionary<string, string> directOrbits)
        {
            var chain = new List<string>() { origin };

            while (directOrbits.TryGetValue(origin, out origin))
            {
                chain.Add(origin);
            }

            chain.Reverse();
            return chain;
        }
    }
}
