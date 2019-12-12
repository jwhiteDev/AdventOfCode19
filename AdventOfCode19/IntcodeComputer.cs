using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode19
{
    public static class IntcodeComputer
    {
        public static List<int> Run(List<int> program)
        {
            int index = 0;
            bool exitFlag = false;
            while(index < program.Count && !exitFlag)
            {
                int op = program[index];
                int x = program[index + 1];
                int y = program[index + 2];
                int z = program[index + 3];
                switch (op)
                {
                    case 1:
                        program[z] = program[x] + program[y];
                        break;
                    case 2:
                        program[z] = program[x] * program[y];
                        break;
                    case 99:
                        exitFlag = true;
                        break;
                    default:
                        Console.WriteLine($"Errors at position[{index}] numFound: {op}");
                        break;
                }
                index = index + 4;
            }

            return program;
        }

    }
}
