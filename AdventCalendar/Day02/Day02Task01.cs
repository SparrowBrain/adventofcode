using System.Collections.Generic;

namespace AdventCalendar.Day02
{
    internal class Day02Task01 : Puzzle
    {
        private readonly SymbolAnalyzer _analyzer;

        public Day02Task01(IInputReader inputReader, SymbolAnalyzer analyzer) : base(inputReader)
        {
            _analyzer = analyzer;
        }

        protected override string Path => "Day02\\input.txt";

        public override string Solve()
        {
            var twoTimes = 0;
            var threeTimes = 0;
            foreach (var line in Lines)
            {
                var (two, three) = _analyzer.Analyze(line);
                if (two)
                {
                    twoTimes++;
                }

                if (three)
                {
                    threeTimes++;
                }
            }

            var checksum = twoTimes * threeTimes;

            return checksum.ToString();
        }
    }

    internal class SymbolAnalyzer
    {
        public (bool, bool) Analyze(string line)
        {
            var symbols = new Dictionary<char, int>();
            foreach (var letter in line)
            {
                if (symbols.ContainsKey(letter))
                {
                    symbols[letter]++;
                }
                else
                {
                    symbols[letter] = 1;
                }
            }

            return (symbols.ContainsValue(2), symbols.ContainsValue(3));
        }
    }
}