namespace AoC2019.Solutions.IntCodeVm
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks.Dataflow;

    public class BufferedInstruction
    {
        private const int MaximumParameters = 3;

        private static readonly Func<VmState, int, int[], BufferBlock<long>, BufferBlock<long>, int>[] InstructionStrategies = new Func<VmState, int, int[], BufferBlock<long>, BufferBlock<long>, int>[]
        {
            null,

            // 1. Add
            (state, pointer, parameterModes, _, __) =>
            {
                state.SetMemory(pointer + 3, parameterModes[2], state.GetParameter(pointer + 1, parameterModes[0]) + state.GetParameter(pointer + 2, parameterModes[1]));
                return pointer + 4;
            },

            // 2. Multiply
            (state, pointer, parameterModes, _, __) =>
            {
                state.SetMemory(pointer + 3, parameterModes[2], state.GetParameter(pointer + 1, parameterModes[0]) * state.GetParameter(pointer + 2, parameterModes[1]));
                return pointer + 4;
            },

            // 3. Input
            (state, pointer, parameterModes, input, __) =>
            {
                state.SetMemory(pointer + 1, parameterModes[0], input.Receive(TimeSpan.FromSeconds(1)));
                return pointer + 2;
            },

            // 4. Output
            (state, pointer, parameterModes, _, outputs) =>
            {
                outputs.Post(state.GetParameter(pointer + 1, parameterModes[0]));
                return pointer + 2;
            },

            // 5. Jump-if-true
            (state, pointer, parameterModes, _, __) => state.GetParameter(pointer + 1, parameterModes[0]) == 0 ? pointer + 3 : (int)state.GetParameter(pointer + 2, parameterModes[1]),

            // 6. Jump-if-false
            (state, pointer, parameterModes, _, __) => state.GetParameter(pointer + 1, parameterModes[0]) != 0 ? pointer + 3 : (int)state.GetParameter(pointer + 2, parameterModes[1]),

            // 7. Less than
            (state, pointer, parameterModes, _, __) =>
            {
                state.SetMemory(pointer + 3, parameterModes[2], state.GetParameter(pointer + 1, parameterModes[0]) < state.GetParameter(pointer + 2, parameterModes[1]) ? 1 : 0);
                return pointer + 4;
            },

            // 8. Equal to
            (state, pointer, parameterModes, _, __) =>
            {
                state.SetMemory(pointer + 3, parameterModes[2], state.GetParameter(pointer + 1, parameterModes[0]) == state.GetParameter(pointer + 2, parameterModes[1]) ? 1 : 0);
                return pointer + 4;
            },

            (state, pointer, parameterModes, _, __) =>
            {
                state.RelativeBase += (int)state.GetParameter(pointer + 1, parameterModes[0]);
                return pointer + 2;
            },
        };

        private readonly int[] parameterModes = new int[MaximumParameters];

        private readonly Func<VmState, int, int[], BufferBlock<long>, BufferBlock<long>, int> operatorFunc;

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

        public int Execute(VmState state, int pointer, BufferBlock<long> inputs, BufferBlock<long> outputs) => this.operatorFunc(state, pointer, this.parameterModes, inputs, outputs);
    }
}
