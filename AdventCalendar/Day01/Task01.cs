using System.Collections.Generic;

namespace AdventCalendar.Day01
{
    internal class Task01 : Puzzle
    {
        private readonly IEnumerable<string> _lines;

        public Task01(IInputReader inputReader) : base(inputReader)
        {
            _lines = inputReader.ReadLines();
        }

        public override string Solve()
        {
            var frequency = 0;
            foreach (var line in _lines)
            {
                frequency += FrequencyParser.ParseChange(line);
            }

            return frequency.ToString();
        }
    }

    internal class FrequencyParser
    {
        public static int ParseChange(string line)
        {
            return int.Parse(line);
        }
    }
}