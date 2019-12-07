namespace AoC2019.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string data)
        {
            int[] memory = data
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            IntCodeVm[] amps = Enumerable.Range(0, 5).Select(_ => new IntCodeVm(memory)).ToArray();

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

                                results.Add((phaseA, phaseB, phaseC, phaseD, phaseE, this.GetOutput(amps, phaseA, phaseB, phaseC, phaseD, phaseE)));
                            }
                        }
                    }
                }
            }

            return results.Max(x => x.Output).ToString();
        }

        private int GetOutput(IntCodeVm[] amps, params int[] phases)
        {
            var inputs = new Queue<int>();
            int lastOutput = 0;

            for (int i = 0; i < amps.Length; i++)
            {
                inputs.Enqueue(phases[i]);
                inputs.Enqueue(lastOutput);
                amps[i].Reset();
                IEnumerable<int> outputs = amps[i].Execute(inputs);
                lastOutput = outputs.Last();
            }

            return lastOutput;
        }
    }
}
