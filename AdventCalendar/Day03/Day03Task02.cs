namespace AdventCalendar.Day03
{
    internal class Day03Task02 : Puzzle
    {
        public override string Solve()
        {
            var claims = new Parser().Parse(Lines);
            var fabricChecker = new FabricChecker();
            return fabricChecker.GetNonOverlappingId(claims).ToString();
        }

        public Day03Task02(IInputReader inputReader) : base(inputReader)
        {
        }
    }
}