using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode19
{
    class Day2 : Day
    {
        public override void SolvePart1()
        {
            //var temp = new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            //var result = IntcodeComputer.Run(temp);

            /*
             * before running the program, 
             * replace position 1 with the value 12 and replace position 2 with the value 2.
             */
            var list = InputDataAsList;
            list[1] = 12;
            list[2] = 2;

            var result = IntcodeComputer.Run(list);
            Console.WriteLine(result[0]);
        }

        public override void SolvePart2()
        {
            //Determine what pair of inputs produces the output 19690720.
            
            int noun;
            int verb;
            for(noun = 0; noun <100; noun++)
            {
                for(verb = 0; verb<99; verb++)
                {
                    var list = InputDataAsList;
                    list[1] = noun;
                    list[2] = verb;

                    var res = IntcodeComputer.Run(list);
                    if (res[0] == 19690720)
                    {
                        Console.WriteLine($"Noun == {noun} and Verb == {verb}");
                        int endval = (noun * 100) + verb;
                        Console.WriteLine($"Final Answer: {endval}");
                        return;
                    }
                }
            }

            Console.WriteLine("Answer not found");
        }
    }
}
