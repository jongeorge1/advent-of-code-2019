namespace AoC2019.Solutions.Day07
{
    using System.Collections.Generic;

    public static class InstructionFactory
    {
        private static readonly Dictionary<int, Instruction> Cache = new Dictionary<int, Instruction>();

        public static Instruction GetInstruction(int instruction)
        {
            if (!Cache.TryGetValue(instruction, out Instruction val))
            {
                val = new Instruction(instruction);
                Cache.Add(instruction, val);
            }

            return val;
        }
    }
}
