namespace AdventCalendar.Day07
{
    internal class Day07Puzzle02 : Puzzle
    {
        private readonly Settings _settings;

        public Day07Puzzle02(IInputReader inputReader, Settings settings) : base(inputReader)
        {
            _settings = settings;
        }

        public override string Solve()
        {
            var lines = InputReader.ReadLines();
            var steps = new StepFactory(_settings.StepSettings).Create(lines);
            var instructionHelper = new InstructionHelper();

            var time = instructionHelper.TimeToAssemble(steps, _settings.WorkerSettings.WorkerCount);

            return time.ToString();
        }
    }
}