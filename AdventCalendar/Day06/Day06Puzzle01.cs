using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day06
{
    internal class Day06Puzzle01 : Puzzle
    {
        public Day06Puzzle01(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            throw new NotImplementedException();
        }
    }

    internal class BoardFactory
    {
        private readonly IEnumerable<Source> _sources;

        public BoardFactory(IEnumerable<Source> sources)
        {
            _sources = sources;
        }

        public (int, int) GetMaxXY()
        {
            return (_sources.Max(p => p.X), _sources.Max(p => p.Y));
        }
    }

    internal class Source
    {
        public Source(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Source Parse(string line)
        {
            var elements = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var x = elements[0];
            var y = elements[1];

            return new Source(int.Parse(x), int.Parse(y));
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

    internal class Vector
    {
        public Vector(Source source, int distance)
        {
            Source = source;
            Distance = distance;
        }

        public Source Source { get; }
        public int Distance { get; }
    }

    internal class FieldGenerator
    {
        private readonly IEnumerable<Source> _sources;

        public FieldGenerator(IEnumerable<Source> sources)
        {
            _sources = sources;
        }

        public Vector[][] CreateFields()
        {
            (var maxX, var maxY) = new BoardFactory(_sources).GetMaxXY();
            var board = CreateBoard(maxX, maxY);

            foreach (var source in _sources)
            {
                board[source.X][source.Y] = new Vector(source, 0);
            }

            return board;
        }

        private static Vector[][] CreateBoard(int maxX, int maxY)
        {
            var fields = new Vector[maxX + 1][];
            for (var x = 0; x <= maxX; x++)
            {
                fields[x] = new Vector[maxY + 1];
            }

            return fields;
        }
    }
}