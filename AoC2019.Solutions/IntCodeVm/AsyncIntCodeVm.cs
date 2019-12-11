namespace AoC2019.Solutions.IntCodeVm
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class AsyncIntCodeVm
    {
        private readonly VmState state;

        public AsyncIntCodeVm(string memory)
            : this(CreateMemoryFromProgramInput(memory))
        {
        }

        public AsyncIntCodeVm(long[] memory)
        {
            this.state = new VmState();
            memory.CopyTo(this.state.Memory, 0);

            this.InputBuffer = new BufferBlock<long>();
            this.OutputBuffer = new BufferBlock<long>();
        }

        public BufferBlock<long> InputBuffer { get; set; }

        public BufferBlock<long> OutputBuffer { get; set; }

        public static long[] CreateMemoryFromProgramInput(string input)
        {
            return input
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();
        }

        public Task ExecuteAsync()
        {
            return Task.Run(() =>
            {
                int pointer = 0;

                while (this.state.Memory[pointer] != 99)
                {
                    BufferedInstruction instruction = BufferedInstructionFactory.GetBufferedInstruction((int)this.state.Memory[pointer]);
                    pointer = instruction.Execute(this.state, pointer, this.InputBuffer, this.OutputBuffer);
                }
            });
        }
    }
}
