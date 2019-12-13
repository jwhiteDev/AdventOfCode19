using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;
using System.Xml.Schema;

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

        public override void SolvePart2()
        {
            //string first = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            //string second = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            //string first = "R8,U5,L5,D3";
            //string second = "U7,R6,D4,L4";
            //_redWire = new Wire(first.Split(","));
            //_blueWire = new Wire(second.Split(","));

            _redWire = new Wire(InputData[0].Split(","));
            _blueWire = new Wire(InputData[1].Split(","));

            var distance = FindMinSteps();
            Console.WriteLine($"Shortest Steps equals {distance}");
        }

        private int FindMinDistance()
        {
            var minDistance = -1;
            var intersections = FindIntersections();
            foreach(Point x in intersections)
            {
                int dist = Math.Abs(x.X) + Math.Abs(x.Y);
                if (minDistance == -1)
                    minDistance = dist;
                else
                    minDistance = dist < minDistance ? dist : minDistance;
            }
            
            return minDistance;
        }

        private int FindMinSteps()
        {
            int steps = 0;
            foreach (Line red in _redWire.Lines)
            {
                foreach (Line blue in _blueWire.Lines)
                {
                    var intersect = red.GetIntersect(blue);
                    int dist = Math.Abs(intersect.X) + Math.Abs(intersect.Y);
                    if (dist != 0)
                    {
                        //we know we have an intersection
                        red.End = intersect;
                        blue.End = intersect;
                        int totalSteps = red.Length + blue.Length;
                        if (steps == 0)
                        {
                            steps = totalSteps;
                        }
                        else if(steps > totalSteps)
                        {
                            steps = totalSteps;
                        }
                    }
                }
            }

            return steps;
        }

        private List<Point> FindIntersections()
        {
            var intersections = new List<Point>();

            foreach (Line red in _redWire.Lines)
            {
                foreach (Line blue in _blueWire.Lines)
                {
                    var intersect = red.GetIntersect(blue);
                    int dist = Math.Abs(intersect.X) + Math.Abs(intersect.Y);
                    if (dist != 0)
                    {
                        intersections.Add(intersect);
                    }
                }
            }
            return intersections;

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
            Line previous = null;
            foreach (string x in points)
            {
                var nextPoint = GetNextPoint(start, x);
                var newLine = new Line(start, nextPoint, previous);
                lines.Add(newLine);
                previous = newLine;
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
        public Point Start
        {
            get;
            set;
        }
        public Point End
        {
            get;
            set;
        }
        private string Direction
        {
            get;
            set;
        }

        private Line _parent = null;

        public int Length
        {
            get
            {

                int _length = 0;
                int min = 0;
                int max = 0;
                if (Direction == "X")
                {
                    min = Math.Min(Start.X, End.X);
                    max = Math.Max(Start.X, End.X);
                }
                else
                {
                    min = Math.Min(Start.Y, End.Y);
                    max = Math.Max(Start.Y, End.Y);
                }

                _length = max - min;
                if (_parent != null)
                    _length += _parent.Length;
                
                return _length;
            }
        }
        public Line(Point start, Point end, Line parent)
        {
            Start = start;
            End = end;
            Direction = start.Y == end.Y ? "X" : "Y";
            _parent = parent;
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
