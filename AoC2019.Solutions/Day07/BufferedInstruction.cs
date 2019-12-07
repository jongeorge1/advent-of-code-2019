namespace AoC2019.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks.Dataflow;

    public class BufferedInstruction
    {
        private const int MaximumParameters = 2;

        private static readonly Func<int[], int, int[], BufferBlock<int>, BufferBlock<int>, int>[] InstructionStrategies = new Func<int[], int, int[], BufferBlock<int>, BufferBlock<int>, int>[]
        {
            null,

            // 1. Add
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, pointer + 1, parameterModes[0]) + GetParameter(memory, pointer + 2, parameterModes[1]);
                return pointer + 4;
            },

            // 2. Multiply
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, pointer + 1, parameterModes[0]) * GetParameter(memory, pointer + 2, parameterModes[1]);
                return pointer + 4;
            },

            // 3. Input
            (memory, pointer, _, input, __) =>
            {
                memory[memory[pointer + 1]] = input.Receive(TimeSpan.FromSeconds(1));
                return pointer + 2;
            },

            // 4. Output
            (memory, pointer, parameterModes, _, outputs) =>
            {
                outputs.Post(GetParameter(memory, pointer + 1, parameterModes[0]));
                return pointer + 2;
            },

            // 5. Jump-if-true
            (memory, pointer, parameterModes, _, __) => GetParameter(memory, pointer + 1, parameterModes[0]) == 0 ? pointer + 3 : GetParameter(memory, pointer + 2, parameterModes[1]),

            // 6. Jump-if-false
            (memory, pointer, parameterModes, _, __) => GetParameter(memory, pointer + 1, parameterModes[0]) != 0 ? pointer + 3 : GetParameter(memory, pointer + 2, parameterModes[1]),

            // 7. Less than
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, pointer + 1, parameterModes[0]) < GetParameter(memory, pointer + 2, parameterModes[1]) ? 1 : 0;
                return pointer + 4;
            },

            // 8. Equal to
            (memory, pointer, parameterModes, _, __) =>
            {
                memory[memory[pointer + 3]] = GetParameter(memory, pointer + 1, parameterModes[0]) == GetParameter(memory, pointer + 2, parameterModes[1]) ? 1 : 0;
                return pointer + 4;
            },
        };

        private readonly int[] parameterModes = new int[MaximumParameters];

        private readonly Func<int[], int, int[], BufferBlock<int>, BufferBlock<int>, int> operatorFunc;

        public BufferedInstruction(int instruction)
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

        public int Execute(int[] memory, int pointer, BufferBlock<int> inputs, BufferBlock<int> outputs) => this.operatorFunc(memory, pointer, this.parameterModes, inputs, outputs);

        private static int GetParameter(int[] memory, int pointer, int mode) => mode == 0 ? memory[memory[pointer]] : memory[pointer];
    }
}
