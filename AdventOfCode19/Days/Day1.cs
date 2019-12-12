using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode19
{
    class Day1 : Day
    {

        public override void SolvePart1()
        {
            var result = 0;
            foreach(int x in InputDataAsInt)
                result += CalculateFuelCost(x);
            Console.WriteLine($"Fuel Cost: {result}");
        }

        public override void SolvePart2()
        {
            var result = 0;
            foreach (int x in InputDataAsInt)
            {
                result += CalculateTotalFuel(x);
            }
            Console.WriteLine($"Total Fuel Cost: {result}");
        }    
        
        private int CalculateTotalFuel(int mass)
        {
            int result = 0;
            int fuelRemain = mass;
            while(fuelRemain >= 0)
            {
                int fuel = CalculateFuelCost(fuelRemain);
                if (fuel <= 0)
                    break;
                result += fuel;
                fuelRemain = fuel;
            }

            return result;
        }
        
        private int CalculateFuelCost(int mass)
        {
            return (mass / 3) - 2;
        }
    }
}
