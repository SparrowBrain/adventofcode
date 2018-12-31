namespace AdventCalendar.Day04
{
    internal class Day04Puzzle02 : Puzzle
    {
        public Day04Puzzle02(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            var solver = new ScheduleScrubber(new GuardMinuteFactory(InputReader));

            var (guardId, minute) = solver.MostAsleepGuardOnSameMinute();

            return (guardId * minute).ToString();
        }
    }
}