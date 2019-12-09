namespace AoC2019.Solutions.Day09
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public static class BufferedInstructionFactory
    {
        private static readonly ConcurrentDictionary<int, BufferedInstruction> Cache = new ConcurrentDictionary<int, BufferedInstruction>();

        public static BufferedInstruction GetBufferedInstruction(int instruction)
        {
            return Cache.GetOrAdd(instruction, x => new BufferedInstruction(x));
        }
    }
}
