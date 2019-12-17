using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode19
{
    abstract class Day
    {
        string _filepath;
        private string[] _inputData;

        public List<int> InputDataAsInt => InputData.Select(x => int.Parse(x)).ToList();

        public List<int> InputDataAsList => InputData[0].Split(",").Select(int.Parse).ToList();

        public List<string> InputData
        {
            get
            {
                if(_inputData == null)
                {
                    _inputData = File.ReadAllLines(_filepath);
                }
                return _inputData.ToList();
            }
        }

        public Day()
        {
            _filepath = $"Data/{this.GetType().Name}.txt";
        }

        public abstract void SolvePart1();
        public abstract void SolvePart2();

        public void Solve()
        {
            var problemName = this.GetType().Name;
            Console.WriteLine($"Solving {problemName}: Part 1");
            SolvePart1();

            Console.WriteLine($"Solving {problemName}: Part 2");
            SolvePart2();
            
        }

    }
}
