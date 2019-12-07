namespace AoC2019.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            int[] memory = data
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();


            AsyncIntCodeVm[] amps = Enumerable.Range(0, 5).Select(_ => new AsyncIntCodeVm(memory)).ToArray();

            var results = new List<(int A, int B, int C, int D, int E, int Output)>(100000);

            for (int phaseA = 5; phaseA < 10; phaseA++)
            {
                for (int phaseB = 5; phaseB < 10; phaseB++)
                {
                    for (int phaseC = 5; phaseC < 10; phaseC++)
                    {
                        for (int phaseD = 5; phaseD < 10; phaseD++)
                        {
                            for (int phaseE = 5; phaseE < 10; phaseE++)
                            {
                                if (phaseA == phaseB || phaseA == phaseC || phaseA == phaseD || phaseA == phaseE
                                    || phaseB == phaseC || phaseB == phaseD || phaseB == phaseE
                                    || phaseC == phaseD || phaseC == phaseE
                                    || phaseD == phaseE)
                                {
                                    continue;
                                }

                                int result = this.GetOutputAsync(amps, phaseA, phaseB, phaseC, phaseD, phaseE).Result;
                                results.Add((phaseA, phaseB, phaseC, phaseD, phaseE, result));
                            }
                        }
                    }
                }
            }

            return results.Max(x => x.Output).ToString();
        }

        private async Task<int> GetOutputAsync(AsyncIntCodeVm[] amps, params int[] phases)
        {
            foreach (AsyncIntCodeVm current in amps)
            {
                current.Reset();
            }

            BufferBlock<int>[] buffers = phases.Select(x =>
            {
                var buffer = new BufferBlock<int>();
                buffer.Post(x);
                return buffer;
            })
            .ToArray();

            buffers[0].Post(0);

            IEnumerable<Task> tasks = Enumerable.Range(0, amps.Length)
                .Select(i => amps[i].ExecuteAsync(buffers[i], buffers[(i + 1) % buffers.Length]));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return buffers[0].Receive();
        }
    }
}
