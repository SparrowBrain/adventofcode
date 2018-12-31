using System;

namespace AdventCalendar.Day04
{
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
}