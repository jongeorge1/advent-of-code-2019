namespace AoC2019.Solutions.Day02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int[] program = input
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    int[] memory = (int[])program.Clone();
                    memory[1] = noun;
                    memory[2] = verb;

                    this.Execute(memory);

                    if (memory[0] == 19690720)
                    {
                        return ((100 * noun) + verb).ToString();
                    }
                }
            }

            return "bork!";
        }

        public void Execute(int[] program)
        {
            int pointer = 0;

            while (program[pointer] != 99)
            {
                if (program[pointer] == 1)
                {
                    program[program[pointer + 3]] = program[program[pointer + 1]] + program[program[pointer + 2]];
                }
                else if (program[pointer] == 2)
                {
                    program[program[pointer + 3]] = program[program[pointer + 1]] * program[program[pointer + 2]];
                }

                pointer += 4;
            }
        }
    }
}
