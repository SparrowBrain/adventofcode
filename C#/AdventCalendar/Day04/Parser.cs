using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day04
{
    internal class Parser
    {
        private readonly Regex _regex = new Regex(@"^\[(?<time>\d{4}-\d{2}-\d{2} \d{2}:\d{2})\] (?<event>[\w #]+)$", RegexOptions.Compiled);

        public IEnumerable<LogEntry> Parse(IEnumerable<string> lines)
        {
            var entries = new List<LogEntry>();
            foreach (var line in lines)
            {
                var match = _regex.Match(line);
                if (!match.Success)
                {
                    throw new Exception("failure to parse");
                }

                entries.Add(new LogEntry(DateTime.Parse(match.Groups["time"].Value), match.Groups["event"].Value));
            }

            return entries;
        }
    }
}