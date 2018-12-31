using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day04
{
    internal class EnrichedLogEntry
    {
        private static readonly Regex _regex = new Regex("Guard #(?<id>\\d+) begins shift", RegexOptions.Compiled);

        public int GuardId { get; }
        public DateTime Time { get; }
        public EntryType Type { get; }

        public EnrichedLogEntry(LogEntry logEntry, int guardId)
        {
            GuardId = guardId;
            Time = logEntry.Time;
            Type = ParseType(logEntry.Event);
        }

        private EntryType ParseType(string logEntryEvent)
        {
            if (logEntryEvent.StartsWith("Guard"))
            {
                return EntryType.Starts;
            }

            if (logEntryEvent.StartsWith("wakes up"))
            {
                return EntryType.Awake;
            }

            if (logEntryEvent.StartsWith("falls asleep"))
            {
                return EntryType.Asleep;
            }

            throw new Exception("unknown type");
        }

        public static IEnumerable<EnrichedLogEntry> Factory(IEnumerable<LogEntry> entries)
        {
            entries = entries.OrderBy(x => x.Time);

            var guardId = -1;
            var enrichedEntries = new List<EnrichedLogEntry>();
            foreach (var entry in entries)
            {
                var match = _regex.Match(entry.Event);
                if (match.Success)
                {
                    guardId = int.Parse(match.Groups["id"].Value);
                    continue;
                }

                enrichedEntries.Add(new EnrichedLogEntry(entry, guardId));
            }

            return enrichedEntries;
        }
    }
}