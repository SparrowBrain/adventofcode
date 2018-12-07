using System.Collections.Generic;
using System.IO;
using AdventCalendar.Day02;

namespace AdventCalendar.Day01
{
    class Task02 : Puzzle
    {
        protected override string Path => "Day01\\input1-1.txt";

        public override string Solve()
        {

            var frequency = 0;
            var reachedFrequencies = new HashSet<int>();
            while (true)
            {
                foreach (var line in Lines)
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
    }
}