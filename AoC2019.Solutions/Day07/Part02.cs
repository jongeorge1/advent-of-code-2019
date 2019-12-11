namespace AoC2019.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using AoC2019.Solutions.IntCodeVm;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            long[] memory = AsyncIntCodeVm.CreateMemoryFromProgramInput(data);

            var results = new List<(int A, int B, int C, int D, int E, long Output)>(100000);

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

                                long result = this.GetOutput(memory, phaseA, phaseB, phaseC, phaseD, phaseE);
                                results.Add((phaseA, phaseB, phaseC, phaseD, phaseE, result));
                            }
                        }
                    }
                }
            }

            return results.Max(x => x.Output).ToString();
        }

        private long GetOutput(long[] memory, params int[] phases)
        {
            AsyncIntCodeVm[] amps = phases.Select(_ => new AsyncIntCodeVm(memory)).ToArray();
            amps.Connect(true);
            amps.InitialiseInputs(phases);

            amps[0].InputBuffer.Post(0);

            amps.ExecuteAllAsync().Wait();

            return amps.Last().OutputBuffer.Receive();
        }
    }
}
