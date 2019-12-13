using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode19
{
    class Day3 : Day
    {
        Wire _redWire;
        Wire _blueWire;
        public override void SolvePart1()
        {
            //string first = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            //string second = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            //string first = "R8,U5,L5,D3";
            //string second = "U7,R6,D4,L4";
            //_redWire = new Wire(first.Split(","));
            //_blueWire = new Wire(second.Split(","));

            _redWire = new Wire(InputData[0].Split(","));
            _blueWire = new Wire(InputData[1].Split(","));

            var distance = FindMinDistance();   
            Console.WriteLine($"Distance equals {distance}");

            

        }

        private int FindMinDistance()
        {
            var minDistance = -1;
            List<Point> intersects = new List<Point>();
            foreach(Line red in _redWire.Lines)
            {
                foreach (Line blue in _blueWire.Lines)
                {
                    var intersect = red.GetIntersect(blue);
                    int dist = Math.Abs(intersect.X) + Math.Abs(intersect.Y);
                    if (dist == 0)
                    {
                        continue;
                    }
                    else if (minDistance == -1 && dist != 0)
                        minDistance = dist;
                    else 
                        minDistance = dist < minDistance ? dist : minDistance;
                    
                }
            }
            
            return minDistance;
        }
        
        public override void SolvePart2()
        {
            Console.Write("Part 2 Not Implemented Yet");
        }
    }

    public class Wire
    {
        private List<Line> lines = new List<Line>();
        public List<Line> Lines
        {
            get
            {
                return lines;
            }
        }

        public Wire(string[] points)
        {
            
            Point start = new Point(0, 0);
            foreach (string x in points)
            {
                var nextPoint = GetNextPoint(start, x);
                lines.Add(new Line(start, nextPoint));
                
                start = nextPoint;
            }
        }

        private Point GetNextPoint(Point origin, string data)
        {
            //get direction
            var direction = data[0];
            //get distance
            var dist = int.Parse(data.Substring(1));
            var nextPoint = origin;
            switch (direction)
            {
                case 'U':
                    nextPoint.Y = nextPoint.Y + dist;
                    break;
                case 'D':
                    nextPoint.Y = nextPoint.Y - dist;
                    break;
                case 'L':
                    nextPoint.X = nextPoint.X - dist;
                    break;
                case 'R':
                    nextPoint.X = nextPoint.X + dist;
                    break;
            };

            return nextPoint;
        }

    }

    public class Line
    {
        private Point Start
        {
            get;
            set;
        }
        private Point End
        {
            get;
            set;
        }
        private string Direction
        {
            get;
            set;
        }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
            Direction = start.Y == end.Y ? "X" : "Y";
        }

        public Point GetIntersect(Line other)
        {
            var intersect = Point.Empty;
            //if parallel, then they dont intersect, we assume overlap == parallel
            if (this.Direction == other.Direction) {
                return intersect; // no intersect
            }

            if(this.Direction == "X") // Only one comparison for y
            {
                var doesIntersect = IntersectX(other.Start.X, other.End.X) && other.IntersectY(Start.Y, other.End.Y);

                if(doesIntersect)
                {
                    intersect.X = other.Start.X;
                    intersect.Y = Start.Y;
                } 
            }
            else // only one comparison for x
            {
                var doesIntersect = IntersectY(other.Start.Y, other.End.Y) && other.IntersectX(Start.X, End.X);

                if (doesIntersect)
                {
                    intersect.X = Start.X;
                    intersect.Y = other.Start.Y;
                }
            }
            /*
            var doesIntersect = IntersectX(other.Start.X, other.End.X) && IntersectY(other.Start.Y, other.End.Y);
            */
            return intersect;
        }

        private bool IntersectX(int x1, int x2)
        {
            var othermin = Math.Min(x1, x2);
            var othermax = Math.Max(x1, x2);
            var min = Math.Min(Start.X, End.X);
            var max = Math.Max(Start.X, End.X);
            return othermin >= min && othermax <= max;
        }

        private bool IntersectY(int y1, int y2)
        {
            var othermin = Math.Min(y1, y2);
            var othermax = Math.Max(y1, y2);
            var min = Math.Min(Start.Y, End.Y);
            var max = Math.Max(Start.Y, End.Y);
            return othermin >= min && othermax <= max;
        }

    }

}
