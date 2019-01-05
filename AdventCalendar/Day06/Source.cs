using System;

namespace AdventCalendar.Day06
{
    internal class Source
    {
        public Source(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public bool Infinity { get; set; }

        public static Source Parse(string line)
        {
            var elements = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var x = elements[0];
            var y = elements[1];

            return new Source(int.Parse(x), int.Parse(y));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Source other))
            {
                return false;
            }

            return Equals(other);
        }

        protected bool Equals(Source other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}