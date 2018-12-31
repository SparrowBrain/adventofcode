namespace AdventCalendar.Day01
{
    internal class Task01 : Puzzle
    {
        public override string Solve()
        {
            var frequency = 0;
            foreach (var line in Lines)
            {
                frequency += FrequencyParser.ParseChange(line);
            }

            return frequency.ToString();
        }

        public Task01(IInputReader inputReader) : base(inputReader)
        {
        }
    }

    internal class FrequencyParser
    {
        public static int ParseChange(string line)
        {
            return int.Parse(line);
        }
    }
}