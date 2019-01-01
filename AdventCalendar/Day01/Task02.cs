using System.Collections.Generic;

namespace AdventCalendar.Day01
{
    internal class Task02 : Puzzle
    {
        public override string Solve()
        {
            var frequency = 0;
            var reachedFrequencies = new HashSet<int>();
            while (true)
            {
                foreach (var line in InputReader.ReadLines())
                {
                    frequency += FrequencyParser.ParseChange(line);
                    if (reachedFrequencies.Contains(frequency))
                    {
                        return frequency.ToString();
                    }

                    reachedFrequencies.Add(frequency);
                }
            }
        }

        public Task02(IInputReader inputReader) : base(inputReader)
        {
        }
    }
}