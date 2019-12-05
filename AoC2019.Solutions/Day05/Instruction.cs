namespace AoC2019.Solutions.Day05
{
    using System;
    using System.Collections.Generic;

    public class Instruction
    {
        private const int MaximumParameters = 3;

        private static readonly Func<int[], int, int[], int, List<int>, int>[] InstructionStrategies = new Func<int[], int, int[], int, List<int>, int>[]
        {
            null,

            // 1. Add
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, memory[pointer + 1], parameterModes[0]) + GetParameter(memory, memory[pointer + 2], parameterModes[1]);
                return pointer + 4;
            },

            // 2. Multiply
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, memory[pointer + 1], parameterModes[0]) * GetParameter(memory, memory[pointer + 2], parameterModes[1]);
                return pointer + 4;
            },

            // 3. Input
            (memory, pointer, _, input, __) =>
            {
                memory[memory[pointer + 1]] = input;
                return pointer + 2;
            },

            // 4. Output
            (memory, pointer, parameterModes, _, outputs) =>
            {
                outputs.Add(GetParameter(memory, memory[pointer + 1], parameterModes[0]));
                return pointer + 2;
            },

            // 5. Jump-if-true
            (memory, pointer, parameterModes, _, __) => GetParameter(memory, memory[pointer + 1], parameterModes[0]) == 0 ? pointer + 3 : GetParameter(memory, memory[pointer + 2], parameterModes[1]),

            // 6. Jump-if-false
            (memory, pointer, parameterModes, _, __) => GetParameter(memory, memory[pointer + 1], parameterModes[0]) != 0 ? pointer + 3 : GetParameter(memory, memory[pointer + 2], parameterModes[1]),

            // 7. Less than
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, memory[pointer + 1], parameterModes[0]) < GetParameter(memory, memory[pointer + 2], parameterModes[1]) ? 1 : 0;
                return pointer + 4;
            },

            // 8. Equal to
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, memory[pointer + 1], parameterModes[0]) == GetParameter(memory, memory[pointer + 2], parameterModes[1]) ? 1 : 0;
                return pointer + 4;
            },
        };

        private readonly int[] parameterModes = new int[MaximumParameters];

        private readonly Func<int[], int, int[], int, List<int>, int> operatorFunc;

        public Instruction(int instruction)
        {
            for (int i = 0; i < MaximumParameters; i++)
            {
                int parameter = MaximumParameters - i - 1;
                int divisor = (int)Math.Pow(10, parameter + 2);
                this.parameterModes[parameter] = instruction / divisor;
                instruction %= divisor;
            }

            this.operatorFunc = InstructionStrategies[instruction];
        }

        public int Execute(int[] memory, int pointer, int input, List<int> outputs) => this.operatorFunc(memory, pointer, this.parameterModes, input, outputs);

        private static int GetParameter(int[] memory, int parameter, int mode) => mode == 0 ? memory[parameter] : parameter;
    }
}
