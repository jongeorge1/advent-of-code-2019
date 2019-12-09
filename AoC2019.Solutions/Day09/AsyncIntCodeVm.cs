
namespace AoC2019.Solutions.Day09
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class AsyncIntCodeVm
    {
        private VmState state;

        public AsyncIntCodeVm(long[] memory)
        {
            this.state = new VmState();
            memory.CopyTo(this.state.Memory, 0);
        }

        public Task ExecuteAsync(BufferBlock<long> inputs, BufferBlock<long> outputs)
        {
            return Task.Run(() =>
            {
                int pointer = 0;

                while (this.state.Memory[pointer] != 99)
                {
                    BufferedInstruction instruction = BufferedInstructionFactory.GetBufferedInstruction((int)this.state.Memory[pointer]);
                    pointer = instruction.Execute(this.state, pointer, inputs, outputs);
                }
            });
        }
    }
}
