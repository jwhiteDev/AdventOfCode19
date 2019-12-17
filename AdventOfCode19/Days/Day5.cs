using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode19
{
    class Day5 : Day
    {
        public override void SolvePart1()
        {
            //The TEST diagnostic program will start by requesting from the user the ID of the system 
            //to test by running an input instruction - provide it 1 the ID for the ship's air conditioner unit.

            //var list = new List<int>() { 1002, 4, 3, 4, 33 };
            var list = InputDataAsList;

            var computer = new IntcodeComputer(list);
            var result = computer.Run();
            //13294380 is the diagnostic
            Console.WriteLine($"Diagnostics Complete");
        }

        public override void SolvePart2()
        {
            Console.WriteLine("Part 2 not implemented");

        }
    }
}
