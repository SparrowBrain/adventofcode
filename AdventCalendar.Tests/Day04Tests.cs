using AdventCalendar.Day04;
using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Theory, AutoData]
        public void TaskSolves_GuardWithMostAsleepMinutesTimesId(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
                "[1518-11-05 00:55] wakes up",
            });

            var scrubber = new ScheduleScrubber(new GuardMinuteFactory(inputReaderMock.Object));

            var guardId = scrubber.MostAsleepGuard();

            Assert.Equal(10, guardId);
        }

        [Theory, AutoData]
        public void TaskSolves_GuardMostAsleepMinutes(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
                "[1518-11-05 00:55] wakes up",
            });

            var scrubber = new ScheduleScrubber(new GuardMinuteFactory(inputReaderMock.Object));
           
            var minute = scrubber.MostAsleepGuardMinute(10);

            Assert.Equal(24, minute);
        }

        [Theory, AutoData]
        public void MostAsleepGuardOnSameMinute(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
                "[1518-11-05 00:55] wakes up",
            });

            var scrubber = new ScheduleScrubber(new GuardMinuteFactory(inputReaderMock.Object));

            var (guard, minute) = scrubber.MostAsleepGuardOnSameMinute();

            Assert.Equal(99, guard);
            Assert.Equal(45, minute);
        }

        [Theory, AutoData]
        public void MostAsleepGuard(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
                "[1518-11-05 00:55] wakes up",
            });

            var puzzle = new Day04Puzzle01(inputReaderMock.Object);

            var result = puzzle.Solve();

            Assert.Equal("240", result);
        }

        [Theory, AutoData]
        public void GuardMostFrequentlyAsleepOnSameMinute(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:25] wakes up",
                "[1518-11-01 00:30] falls asleep",
                "[1518-11-01 00:55] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:50] wakes up",
                "[1518-11-03 00:05] Guard #10 begins shift",
                "[1518-11-03 00:24] falls asleep",
                "[1518-11-03 00:29] wakes up",
                "[1518-11-04 00:02] Guard #99 begins shift",
                "[1518-11-04 00:36] falls asleep",
                "[1518-11-04 00:46] wakes up",
                "[1518-11-05 00:03] Guard #99 begins shift",
                "[1518-11-05 00:45] falls asleep",
                "[1518-11-05 00:55] wakes up",
            });

            var puzzle = new Day04Puzzle02(inputReaderMock.Object);

            var result = puzzle.Solve();

            Assert.Equal("4455", result);
        }

        [Theory, AutoData]
        public void GuardMinuteFactory_GetsCorrectAmountOfMinutes(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:07] wakes up",
            });

            var guardMinuteFactory = new GuardMinuteFactory(inputReaderMock.Object);

            var result = guardMinuteFactory.GenerateGuardMinutes();

            Assert.Equal(2, result.Count());
        }

        [Theory, AutoData]
        public void GuardMinuteFactory_GetsCorrectGuardId(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:07] wakes up",
            });

            var guardMinuteFactory = new GuardMinuteFactory(inputReaderMock.Object);

            var result = guardMinuteFactory.GenerateGuardMinutes();

            Assert.Equal(10, result.First().GuardId);
        }

        [Theory, AutoData]
        public void GuardMinuteFactory_GetsCorrectGuardIdForSecondGuard(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "[1518-11-01 00:00] Guard #10 begins shift",
                "[1518-11-01 00:05] falls asleep",
                "[1518-11-01 00:06] wakes up",
                "[1518-11-01 23:58] Guard #99 begins shift",
                "[1518-11-02 00:40] falls asleep",
                "[1518-11-02 00:41] wakes up",
            });

            var guardMinuteFactory = new GuardMinuteFactory(inputReaderMock.Object);

            var result = guardMinuteFactory.GenerateGuardMinutes();

            Assert.Equal(99, result.Last().GuardId);
        }
    }
}