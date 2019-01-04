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

        public Vector[][] CreateBoard(int maxX, int maxY)
        {
            var fields = new Vector[maxX + 1][];
            for (var x = 0; x <= maxX; x++)
            {
                fields[x] = new Vector[maxY + 1];
            }

            return fields;
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

    internal class Vector
    {
        public Vector(Source source, int distance)
        {
            Source = source;
            Distance = distance;
        }

        public Source Source { get; }
        public int Distance { get; }
        public static Vector Conflict => new Vector(null, int.MinValue);

        public override bool Equals(object obj)
        {
            return obj is Vector other && Equals(other);
        }

        protected bool Equals(Vector other)
        {
            return Equals(Source, other.Source) && Distance == other.Distance;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^ Distance;
            }
        }
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
            var boardFactory = new BoardFactory(_sources);
            (var maxX, var maxY) = boardFactory.GetMaxXY();
            var board = boardFactory.CreateBoard(maxX, maxY);

            var distance = 0;
            var modificationHappened = false;
            do
            {
                modificationHappened = false;
                foreach (var source in _sources)
                {
                    for (var deltaX = 0; deltaX <= distance; deltaX++)
                    {
                        var deltaY = distance - deltaX;

                        modificationHappened |= SetVector(board, source, distance, source.X + deltaX, source.Y + deltaY);
                        modificationHappened |= SetVector(board, source, distance, source.X + deltaX, source.Y - deltaY);
                        modificationHappened |= SetVector(board, source, distance, source.X - deltaX, source.Y + deltaY);
                        modificationHappened |= SetVector(board, source, distance, source.X - deltaX, source.Y - deltaY);
                    }
                }

                distance++;
            } while (modificationHappened);

            return board;
        }

        private static bool SetVector(Vector[][] board, Source source, int distance, int xCoord, int yCoord)
        {
            if (xCoord > board.Length - 1 || yCoord > board[0].Length - 1 || xCoord < 0 || yCoord < 0)
            {
                return false;
            }
            if (board[xCoord][yCoord] == null)
            {
                board[xCoord][yCoord] = new Vector(source, distance);
                return true;
            }

            if (board[xCoord][yCoord].Distance == distance && !Equals(board[xCoord][yCoord].Source, source))
            {
                board[xCoord][yCoord] = Vector.Conflict;
                return true;
            }

            return false;
        }
    }
}