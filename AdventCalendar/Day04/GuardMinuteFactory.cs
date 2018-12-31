using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day04
{
    internal class GuardMinuteFactory
    {
        private readonly IInputReader _inputReader;

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