namespace AoC2019.Solutions.Day05
{
    using System.Collections.Generic;

    public class IntCodeVm
    {
        private readonly int[] memory;

        public IntCodeVm(int[] memory)
        {
            this.memory = memory;
        }

        public IEnumerable<int> Execute(int input)
        {
            int pointer = 0;
            var outputs = new List<int>(500);

            while (this.memory[pointer] != 99)
            {
                Instruction instruction = InstructionFactory.GetInstruction(this.memory[pointer]);
                pointer = instruction.Execute(this.memory, pointer, input, outputs);
            }

            return outputs;
        }
    }
}
