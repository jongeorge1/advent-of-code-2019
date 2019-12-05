namespace AoC2019.Solutions.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            int[] memory = data
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var vm = new IntCodeVm(memory);
            IEnumerable<int> outputs = vm.Execute(5);

            return outputs.LastOrDefault().ToString();
        }
    }
}
