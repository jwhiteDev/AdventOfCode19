using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode19
{
    public class IntcodeComputer
    {
        private static List<int> program = new List<int>();
        public IntcodeComputer(List<int> input)
        {
            program = input;
        }

        public List<int> Run()
        {
            int i = 0;
            bool exitFlag = false;
            while(i < program.Count && !exitFlag)
            {
                
                var opcode = program[i]+"";
                //process Opcode
                int op = 0;
                var paramModes = ProcessOpcode(program[i] + "", ref op);
                
                int x;
                int y;
                int z;
                switch (op)
                {
                    case 1:
                        x = GetParameter(i + 1, 0, paramModes);
                        y = GetParameter(i + 2, 1, paramModes);
                        z = program[i + 3];
                        program[z] = x + y;
                        i = i + 4;
                        break;
                    case 2:
                        x = GetParameter(i + 1, 0, paramModes);
                        y = GetParameter(i + 2, 1, paramModes);
                        z = program[i + 3];
                        program[z] = x * y;
                        i = i + 4;
                        break;
                    case 3:
                        Console.Write("Please Enter a Diagnostic: ");
                        var res = Console.ReadKey();
                        //validate input

                        z = program[i + 1]; //find index to store input
                        program[z] = int.Parse(res.KeyChar+"");
                        Console.WriteLine();
                        i = i + 2;
                        break;
                    case 4:
                        //Output the value of its parameter

                        z = GetParameter(i + 1, 0, paramModes);
                        Console.WriteLine($"Diagnostic Results for index {i}: {z}");
                        i += 2;
                        break;
                    case 99:
                        exitFlag = true;
                        break;
                    default:
                        Console.WriteLine($"Errors at position[{i}] numFound: {opcode}, {op}");
                        break;
                }
                
            }

            return program;
        }

        private int GetParameter(int index, int param, List<int> paramModes)
        {
            if(paramModes.Count == 0 || paramModes[param] == 0)
            {
                return program[program[index]];
            }
            return program[index];
        }

        private List<int> ProcessOpcode(string input, ref int op)
        {
            if(input.Length<= 2)
            {
                op = int.Parse(input);
                return new List<int>();
            }

            op = int.Parse(input.Substring(input.Length - 2, 2));
            input = input.Substring(0, input.Length - 2);

            var result = input.Select(x => int.Parse(x.ToString())).ToList();
            result.Reverse();
            result.Add(0); //I guess we expect an additional 0 for the non-existant number
            return result;
        }
    }

}
