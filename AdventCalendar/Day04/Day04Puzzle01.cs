using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day04
{
    internal class Day04Puzzle01 : Puzzle
    {
        public override string Solve()
        {
            var logEntries = new Parser().Parse(Lines);
            var enrichedEntries = EnrichedLogEntry.Factory(logEntries);
            var solver = new ScheduleScrubber(new GuardMinuteFactory(_inputReader));

            var guardId = solver.MostAsleepGuard();
            var minute = solver.MostAsleepGuardMinute(guardId);

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
        private readonly GuardMinuteFactory _guardMinuteFactory;

        public ScheduleScrubber(GuardMinuteFactory guardMinuteFactory)
        {
            _guardMinuteFactory = guardMinuteFactory;
        }

        public int MostAsleepGuard()
        {
            var guardMinutes = _guardMinuteFactory.GenerateGuardMinutes();

            return guardMinutes.GroupBy(x => x.GuardId).OrderByDescending(x => x.Count()).First().Key;
        }

        public int MostAsleepGuardMinute(int guardId)
        {
            var guardMinutes = _guardMinuteFactory.GenerateGuardMinutes();
            guardMinutes = guardMinutes.Where(x => x.GuardId == guardId);

            return guardMinutes.GroupBy(x => x.Minute).OrderByDescending(x => x.Count()).First().Key;
        }

        public (int, int) MostAsleepGuardOnSameMinute()
        {
            var minutes = _guardMinuteFactory.GenerateGuardMinutes();

            var minuteGroups = (minutes.GroupBy(x => x));
            minuteGroups = minuteGroups.OrderByDescending(x => x.Count());

            var mostFrequentGuardMinute = minuteGroups.First();
            var guardId = mostFrequentGuardMinute.Key.GuardId;
            var minute = mostFrequentGuardMinute.Key.Minute;

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