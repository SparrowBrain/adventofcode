using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day04
{
    internal class Day04Puzzle02 : Puzzle
    {
        public override string Solve()
        {
            var solver = new ScheduleScrubber(new GuardMinuteFactory(_inputReader));

            var (guardId, minute) = solver.MostAsleepGuardOnSameMinute();

            return (guardId * minute).ToString();
        }

        public Day04Puzzle02(IInputReader inputReader) : base(inputReader)
        {
        }
    }

    internal class GuardMinute
    {
        public GuardMinute(int guardId, int minute)
        {
            GuardId = guardId;
            Minute = minute;
        }

        public int GuardId { get; }
        public int Minute { get; }

        protected bool Equals(GuardMinute other)
        {
            return GuardId == other.GuardId && Minute == other.Minute;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (GuardId * 397) ^ Minute;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is GuardMinute guardMinute && Equals(guardMinute);
        }
    }

    internal class GuardMinuteFactory
    {
        private IInputReader _inputReader;

        public GuardMinuteFactory(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public IEnumerable<GuardMinute> GenerateGuardMinutes()
        {
            var logEntries = new Parser().Parse(_inputReader.ReadLines());
            var enrichedEntries = EnrichedLogEntry.Factory(logEntries);
            var enrichedLogEntries = enrichedEntries as EnrichedLogEntry[] ?? enrichedEntries.ToArray();

            var guardMinutes = new List<GuardMinute>();
            for (var i = 0; i < enrichedLogEntries.Count(); i++)
            {
                var entry = enrichedLogEntries[i];
                if (entry.Type == EntryType.Asleep)
                {
                    for (var m = entry.Time.Minute; m < enrichedLogEntries[i + 1].Time.Minute; m++)
                    {
                        guardMinutes.Add(new GuardMinute(entry.GuardId, m));
                    }
                }
            }

            return guardMinutes;
        }
    }
}