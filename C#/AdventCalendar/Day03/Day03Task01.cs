using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day03
{
    internal class Day03Task01 : Puzzle
    {
        public override string Solve()
        {
            var claims = new Parser().Parse(InputReader.ReadLines());
            var fabricChecker = new FabricChecker();
            return fabricChecker.GetOverlapNumber(claims).ToString();
        }

        public Day03Task01(IInputReader inputReader) : base(inputReader)
        {
        }
    }

    internal class FabricChecker
    {
        public int GetOverlapNumber(IEnumerable<Claim> claims)
        {
            var map = GenerateMap(claims);
            return map.Values.Count(x => x == true);
        }

        private Dictionary<Coordinate, bool> GenerateMap(IEnumerable<Claim> claims)
        {
            var map = new Dictionary<Coordinate, bool>();
            foreach (var claim in claims)
            {
                for (var x = claim.Pos.X; x < claim.Pos.X + claim.Size.X; x++)
                {
                    for (var y = claim.Pos.Y; y < claim.Pos.Y + claim.Size.Y; y++)
                    {
                        var coordinate = new Coordinate(x, y);
                        if (map.ContainsKey(coordinate))
                        {
                            map[coordinate] = true;
                        }
                        else
                        {
                            map[coordinate] = false;
                        }
                    }
                }
            }

            return map;
        }

        public string GetNonOverlappingId(IEnumerable<Claim> claims)
        {
            var map = GenerateMap(claims);
            foreach (var claim in claims)
            {
                var conflicts = false;
                for (var x = claim.Pos.X; x < claim.Pos.X + claim.Size.X; x++)
                {
                    for (var y = claim.Pos.Y; y < claim.Pos.Y + claim.Size.Y; y++)
                    {
                        var coordinate = new Coordinate(x, y);
                        conflicts |= map[coordinate];
                    }
                }

                if (!conflicts)
                {
                    return claim.Id;
                }
            }

            return "";
        }
    }

    internal class Parser
    {
        private const string RegexPattern = "#(?<id>\\w+) @ (?<xPos>\\d+),(?<yPos>\\d+): (?<xSize>\\d+)x(?<ySize>\\d+)";
        private readonly Regex _regex = new Regex(RegexPattern, RegexOptions.Compiled);

        public IEnumerable<Claim> Parse(IEnumerable<string> lines)
        {
            var claims = new List<Claim>();

            foreach (var line in lines)
            {
                var match = _regex.Match(line);
                if (!match.Success)
                {
                    throw new ArgumentException("Could not parse");
                }

                var id = match.Groups["id"].Value;
                var xPos = int.Parse(match.Groups["xPos"].Value);
                var yPos = int.Parse(match.Groups["yPos"].Value);
                var xSize = int.Parse(match.Groups["xSize"].Value);
                var ySize = int.Parse(match.Groups["ySize"].Value);
                var claim = new Claim(id, new Coordinate(xPos, yPos), new Coordinate(xSize, ySize));

                claims.Add(claim);
            }

            return claims;
        }
    }

    internal class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object obj)
        {
            var other = obj as Coordinate;
            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        protected bool Equals(Coordinate other)
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

    internal class Claim
    {
        public string Id { get; }
        public Coordinate Pos { get; }
        public Coordinate Size { get; }

        public Claim(string id, Coordinate pos, Coordinate size)
        {
            Id = id;
            Pos = pos;
            Size = size;
        }
    }
}