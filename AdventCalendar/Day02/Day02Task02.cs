using System;
using System.Collections.Generic;

namespace AdventCalendar.Day02
{
    internal class Day02Task02 : Puzzle
    {
        protected override string Path => "Day02\\input.txt";

        public override string Solve()
        {
            return new MatchFinder().Find(Lines);
        }

        public Day02Task02(IInputReader inputReader) : base(inputReader)
        {
        }
    }

    internal class Code
    {
        public Code(string symbols, int missingIndex)
        {
            Symbols = symbols;
            MissingIndex = missingIndex;
        }

        public string Symbols { get; set; }
        public int MissingIndex { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Code;
            if (other is null)
            {
                return false;
            }

            return Symbols.Equals(other.Symbols) && MissingIndex == other.MissingIndex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Symbols, MissingIndex);
        }
    }

    internal class MatchFinder
    {
        public string Find(IEnumerable<string> lines)
        {
            var variations = new HashSet<Code>();

            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    var variation = new Code(line.Remove(i, 1), i);
                    if (variations.Contains(variation))
                    {
                        return variation.Symbols;
                    }

                    variations.Add(variation);
                }
            }

            throw new Exception("not found");
        }
    }
}