using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode19.Days
{
    class Day4 : Day
    {
        public override void SolvePart1()
        {
            //string code = "223450";
            string code = "123789";

            var startCode = 152085;
            var endCode = 670283;

            var numCodes = FindAllCodes(startCode, endCode).Count;
            Console.WriteLine($"Number of Valid Codes: {numCodes}");
            //1764
        }

        public override void SolvePart2()
        {
            var startCode = 152085;
            var endCode = 670283;

            //Console.WriteLine(TestCode2("112233"));
            //Console.WriteLine(TestCode2("123444"));
            //Console.WriteLine(TestCode2("111122"));
            //Console.WriteLine(TestCode2("223333"));

            var numCodes = FindAllCodes(startCode, endCode, true).Count;
            Console.WriteLine($"Number of Valid Codes Strict: {numCodes}");
        }

        public List<int> FindAllCodes(int start, int end, bool part2 = false)
        {
            var validCodes = new List<int>();
            for(int i = start; i<= end; i++)
            {
                if(part2)
                {
                    if (TestCode2(i.ToString()))
                    {
                        validCodes.Add(i);
                    }
                }
                else
                {
                    if(TestCode(i.ToString()))
                    {
                        validCodes.Add(i);
                    }
                }
 
            }

            return validCodes;
        }

        public bool TestCode(string code)
        {
            //we know all codes are 6 digit, can operate accordingly
            bool containsDupe = false;
            for (int i = 0; i < 5; i++)
            {
                int curNum = int.Parse(code[i]+"");
                int nextNum = int.Parse(code[i + 1] + "");
                
                //check descending, break if ever false
                if (curNum > nextNum)
                    return false;

                //check for dupes
                if (curNum == nextNum)
                {
                    containsDupe = true;
                }
            }

            return containsDupe;
        }

        public bool TestCode2(string code)
        {
            int numDupe = 0;
            //we know all codes are 6 digit, can operate accordingly
            bool containsDouble = false;
            for (int i = 0; i < 5; i++)
            {
                int curNum = int.Parse(code[i] + "");
                int nextNum = int.Parse(code[i + 1] + "");

                //check descending, break if ever false
                if (curNum > nextNum)
                    return false;

                //check for dupes
                if (curNum == nextNum)
                {
                    numDupe++;
                }
                else
                {
                    if (numDupe == 1)
                    {
                        containsDouble = true;
                    }
                    numDupe = 0;
                }
                
            }

            if (numDupe == 1)
                containsDouble = true;

            return containsDouble;
        }

    }
}
