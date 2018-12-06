using System;
using System.IO;
using System.Text;
using AdventCalendar.Day02;

namespace AdventCalendar.Day01
{
    class Task01 : Task
    {
        protected override string Path => "Day01\\input1-1.txt";

        public override string Solve()
        {
            var frequency = 0;
            foreach (var line in Lines)
            {
                frequency += FrequencyParser.ParseChange(line);
            }

            return frequency.ToString();
        }
    }

    class FrequencyParser
    {
        public static int ParseChange(string line)
        {
            return int.Parse(line);
        }
    }
}
