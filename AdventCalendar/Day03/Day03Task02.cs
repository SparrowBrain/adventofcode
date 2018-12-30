using AdventCalendar.Day02;

namespace AdventCalendar.Day03
{
    internal class Day03Task02 : Puzzle
    {
        protected override string Path => "Day03\\input.txt";

        public override string Solve()
        {
            var claims = new Parser().Parse(Lines);
            var fabricChecker = new FabricChecker();
            return fabricChecker.GetNonOverlappingId(claims).ToString();
        }
    }
}