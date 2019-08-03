namespace AdventCalendar.Day07
{
    internal class Day07Puzzle01 : Puzzle
    {
        private readonly Settings _settings;

        public Day07Puzzle01(IInputReader inputReader, Settings settings) : base(inputReader)
        {
            _settings = settings;
        }

        public override string Solve()
        {
            var lines = InputReader.ReadLines();
            var steps = new StepFactory(_settings.StepSettings).Create(lines);
            var orderer = new InstructionHelper();

            return orderer.Order(steps);
        }
    }
}