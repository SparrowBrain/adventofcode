using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day04
{
    internal class Day04Puzzle01 : Puzzle
    {
        protected override string Path => "Day04\\input.txt";

        public override string Solve()
        {
            var logEntries = new Parser().Parse(Lines);
            var enrichedEntries = EnrichedLogEntry.Factory(logEntries);
            var solver = new ScheduleScrubber();

            var guardId = solver.MostAsleepGuard(enrichedEntries);
            var minute = solver.MostAsleepGuardMinute(enrichedEntries, guardId);

            return (guardId * minute).ToString();
        }

        public Day04Puzzle01(IInputReader inputReader) : base(inputReader)
        {
        }
    }

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

    internal class ScheduleScrubber
    {
        public int MostAsleepGuard(IEnumerable<EnrichedLogEntry> entries)
        {
            var guardMinutes = new Dictionary<int, int>();
            var asleepTime = DateTime.MinValue;

            foreach (var entry in entries)
            {
                if (!guardMinutes.ContainsKey(entry.GuardId))
                {
                    guardMinutes[entry.GuardId] = 0;
                }

                switch (entry.Type)
                {
                    case EntryType.Asleep:
                        asleepTime = entry.Time;
                        break;

                    case EntryType.Awake:
                        guardMinutes[entry.GuardId] += (int)(entry.Time - asleepTime).TotalMinutes;
                        break;
                }
            }

            return guardMinutes.OrderByDescending(x => x.Value).First().Key;
        }

        public int MostAsleepGuardMinute(IEnumerable<EnrichedLogEntry> entries, int guardId)
        {
            var guardMinutes = new Dictionary<int, int>();

            var enrichedLogEntries = entries as EnrichedLogEntry[] ?? entries.ToArray();
            for (var i = 0; i < enrichedLogEntries.Count(); i++)
            {
                var entry = enrichedLogEntries[i];
                if (entry.Type == EntryType.Asleep && entry.GuardId == guardId)
                {
                    for (var m = entry.Time.Minute; m < enrichedLogEntries[i + 1].Time.Minute; m++)
                    {
                        if (!guardMinutes.ContainsKey(m))
                        {
                            guardMinutes[m] = 1;
                        }
                        else
                        {
                            guardMinutes[m]++;
                        }
                    }
                }
            }

            return guardMinutes.OrderByDescending(x => x.Value).First().Key;
        }

        internal (int, int) MostAsleepGuardOnSameMinute(IEnumerable<EnrichedLogEntry> entries)
        {
            var guardLogs = new Dictionary<int, IDictionary<int, int>>();

            var enrichedLogEntries = entries as EnrichedLogEntry[] ?? entries.ToArray();
            for (var i = 0; i < enrichedLogEntries.Count(); i++)
            {
                var entry = enrichedLogEntries[i];
                if (entry.Type == EntryType.Asleep)
                {
                    for (var m = entry.Time.Minute; m < enrichedLogEntries[i + 1].Time.Minute; m++)
                    {
                        if (!guardLogs.ContainsKey(entry.GuardId))
                        {
                            guardLogs[entry.GuardId] = new Dictionary<int, int>();
                        }

                        var guardLog = guardLogs[entry.GuardId];
                        if (!guardLog.ContainsKey(m))
                        {
                            guardLog[m] = 1;
                        }
                        else
                        {
                            guardLog[m]++;
                        }
                    }
                }
            }

            var mostFrequentSleep = guardLogs.OrderByDescending(x => x.Value.Values.OrderByDescending(m => m).First()).First();

            var guardId = mostFrequentSleep.Key;
            var minute = mostFrequentSleep.Value.First(x => x.Value == mostFrequentSleep.Value.Values.OrderByDescending(m => m).First()).Key;

            return (guardId, minute);
        }
    }

    internal class LogEntry
    {
        public DateTime Time { get; }
        public string Event { get; }

        public LogEntry(DateTime time, string @event)
        {
            Time = time;
            Event = @event;
        }
    }

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

    internal enum EntryType
    {
        Starts,
        Asleep,
        Awake
    }
}