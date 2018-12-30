using System;
using System.Collections.Generic;
using System.Linq;
using AdventCalendar.Day04;
using Xunit;

namespace AdventCalendar.Tests
{
    public class Day04Tests
    {
        [Theory]
        [InlineData("[1518-11-01 00:00] Guard #10 begins shift", "1518-11-01 00:00", "Guard #10 begins shift")]
        [InlineData("[1518-11-01 00:05] falls asleep", "1518-11-01 00:05", "falls asleep")]
        [InlineData("[1518-11-01 00:25] wakes up", "1518-11-01 00:25", "wakes up")]
        public void Parser_ReturnsCorrectLogEntries(string line, string time, string @event)
        {
            var lines = new List<string> { line };
            var parser = new Parser();

            var entries = parser.Parse(lines);

            Assert.Equal(DateTime.Parse(time), entries.First().Time);
            Assert.Equal(@event, entries.First().Event);
        }

        [Fact]
        public void TaskSolves_GuardWithMostAsleepMinutesTimesId()
        {
            var scrubber = new ScheduleScrubber();
            var entries = new List<EnrichedLogEntry>()
            {
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:05"), "falls asleep"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:25"), "wakes up"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:30"), "falls asleep"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:55"), "wakes up"), 10),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-02 00:40"), "falls asleep"), 99),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-02 00:50"), "wakes up"), 99),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-03 00:24"), "falls asleep"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-03 00:29"), "wakes up"), 10),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-04 00:36"), "falls asleep"), 99),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-04 00:46"), "wakes up"), 99),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-05 00:45"), "falls asleep"), 99),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-05 00:55"), "wakes up"), 99),
            };
            
            var guardId = scrubber.MostAsleepGuard(entries);

            Assert.Equal(10, guardId);
        }

        [Fact]
        public void TaskSolves_GuardMostAsleepMinutes()
        {
            var scrubber = new ScheduleScrubber();
            var entries = new List<EnrichedLogEntry>()
            {
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:05"), "falls asleep"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:25"), "wakes up"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:30"), "falls asleep"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-01 00:55"), "wakes up"), 10),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-02 00:40"), "falls asleep"), 99),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-02 00:50"), "wakes up"), 99),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-03 00:24"), "falls asleep"), 10),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-03 00:29"), "wakes up"), 10),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-04 00:36"), "falls asleep"), 99),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-04 00:46"), "wakes up"), 99),

                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-05 00:45"), "falls asleep"), 99),
                new EnrichedLogEntry(new LogEntry(DateTime.Parse("1518-11-05 00:55"), "wakes up"), 99),
            };

            var minute = scrubber.MostAsleepGuardMinute(entries, 10);

            Assert.Equal(24, minute);
        }
    }
}