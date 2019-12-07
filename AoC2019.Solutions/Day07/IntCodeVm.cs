namespace AoC2019.Solutions.Day07
{
    using System.Collections.Generic;

    public class IntCodeVm
    {
        private readonly int[] originalMemory;
        private int[] memory;

        public IntCodeVm(int[] memory)
        {
            this.originalMemory = memory;
            this.Reset();
        }

        public void Reset()
        {
            this.memory = (int[])this.originalMemory.Clone();
        }

        public IEnumerable<int> Execute(Queue<int> inputs)
        {
            int pointer = 0;
            var outputs = new List<int>(500);

            while (this.memory[pointer] != 99)
            {
                Instruction instruction = InstructionFactory.GetInstruction(this.memory[pointer]);
                pointer = instruction.Execute(this.memory, pointer, inputs, outputs);
            }

            return outputs;
        }
    }
}
