namespace AoC2019.Solutions.Day09
{
    using System;

    public class VmState
    {
        public long[] Memory { get; set; } = new long[100000];

        public int RelativeBase { get; set; } = 0;

        public long GetParameter(int pointer, int mode)
        {
            switch (mode)
            {
                case 0: return this.Memory[this.Memory[pointer]];

                case 1: return this.Memory[pointer];

                case 2: return this.Memory[this.RelativeBase + this.Memory[pointer]];
            }

            throw new ArgumentException();
        }

        public void SetMemory(int pointer, int mode, long value)
        {
            switch (mode)
            {
                case 0:
                    this.Memory[this.Memory[pointer]] = value;
                    break;

                case 2:
                    this.Memory[this.RelativeBase + this.Memory[pointer]] = value;
                    break;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
