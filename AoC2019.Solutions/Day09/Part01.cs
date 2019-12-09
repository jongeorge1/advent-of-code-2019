namespace AoC2019.Solutions.Day09
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class Part01 : ISolution
    {
        public string Solve(string data)
        {
            long[] memory = data
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var vm = new AsyncIntCodeVm(memory);
            var input = new BufferBlock<long>();
            var output = new BufferBlock<long>();
            input.Post(1);

            vm.ExecuteAsync(input, output).Wait();

            while (output.Count > 1)
            {
                Console.WriteLine(output.Receive());
            }

            return output.Receive().ToString();
        }
    }
}
