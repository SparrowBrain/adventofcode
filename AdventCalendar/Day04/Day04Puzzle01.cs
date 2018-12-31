namespace AdventCalendar.Day04
{
    internal class Day04Puzzle01 : Puzzle
    {
        public Day04Puzzle01(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            var solver = new ScheduleScrubber(new GuardMinuteFactory(InputReader));

            var guardId = solver.MostAsleepGuard();
            var minute = solver.MostAsleepGuardMinute(guardId);

            return (guardId * minute).ToString();
        }
    }
}