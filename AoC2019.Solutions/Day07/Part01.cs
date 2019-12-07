namespace AoC2019.Solutions.Day07
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
            int[] memory = data
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var results = new List<(int A, int B, int C, int D, int E, int Output)>(100000);

            for (int phaseA = 0; phaseA < 5; phaseA++)
            {
                for (int phaseB = 0; phaseB < 5; phaseB++)
                {
                    for (int phaseC = 0; phaseC < 5; phaseC++)
                    {
                        for (int phaseD = 0; phaseD < 5; phaseD++)
                        {
                            for (int phaseE = 0; phaseE < 5; phaseE++)
                            {
                                if (phaseA == phaseB || phaseA == phaseC || phaseA == phaseD || phaseA == phaseE
                                    || phaseB == phaseC || phaseB == phaseD || phaseB == phaseE
                                    || phaseC == phaseD || phaseC == phaseE
                                    || phaseD == phaseE)
                                {
                                    continue;
                                }

                                results.Add((phaseA, phaseB, phaseC, phaseD, phaseE, this.GetOutputAsync(memory, phaseA, phaseB, phaseC, phaseD, phaseE).Result));
                            }
                        }
                    }
                }
            }

            return results.Max(x => x.Output).ToString();
        }

        private async Task<int> GetOutputAsync(int[] memory, params int[] phases)
        {
            AsyncIntCodeVm[] amps = phases.Select(_ => new AsyncIntCodeVm(memory)).ToArray();
            var inputBuffer = new BufferBlock<int>();
            var outputBuffer = new BufferBlock<int>();
            outputBuffer.Post(0);

            for (int i = 0; i < amps.Length; i++)
            {
                inputBuffer.Post(phases[i]);
                inputBuffer.Post(outputBuffer.Receive());
                await amps[i].ExecuteAsync(inputBuffer, outputBuffer).ConfigureAwait(false);
            }

            return outputBuffer.Receive();
        }
    }
}
