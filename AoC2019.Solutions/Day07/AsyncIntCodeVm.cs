namespace AoC2019.Solutions.Day07
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class AsyncIntCodeVm
    {
        private readonly int[] originalMemory;
        private int[] memory;

        public AsyncIntCodeVm(int[] memory)
        {
            this.originalMemory = memory;
            this.Reset();
        }

        public void Reset()
        {
            this.memory = (int[])this.originalMemory.Clone();
        }

        public Task ExecuteAsync(BufferBlock<int> inputs, BufferBlock<int> outputs)
        {
            return Task.Run(() =>
            {
                int pointer = 0;

                while (this.memory[pointer] != 99)
                {
                    BufferedInstruction instruction = BufferedInstructionFactory.GetBufferedInstruction(this.memory[pointer]);
                    pointer = instruction.Execute(this.memory, pointer, inputs, outputs);
                }
            });
        }
    }
}
