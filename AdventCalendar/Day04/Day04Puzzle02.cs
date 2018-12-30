namespace AdventCalendar.Day04
{
    internal class Day04Puzzle02 : Puzzle
    {
        protected override string Path => "Day04\\input.txt";

        public override string Solve()
        {
            var logEntries = new Parser().Parse(Lines);
            var enrichedEntries = EnrichedLogEntry.Factory(logEntries);
            var solver = new ScheduleScrubber();

            var (guardId, minute) = solver.MostAsleepGuardOnSameMinute(enrichedEntries);

            return (guardId * minute).ToString();
        }

        public Day04Puzzle02(IInputReader inputReader) : base(inputReader)
        {
        }
    }
}