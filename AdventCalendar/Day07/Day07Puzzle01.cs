namespace AdventCalendar.Day07
{
    internal class Day07Puzzle01 : Puzzle
    {
        public Day07Puzzle01(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            var lines = InputReader.ReadLines();
            var steps = Step.Create(lines);
            var orderer = new StepOrderer();

            return orderer.Order(steps);
        }
    }
}