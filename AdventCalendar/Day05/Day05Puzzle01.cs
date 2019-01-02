using System.Linq;

namespace AdventCalendar.Day05
{
    internal class Day05Puzzle01 : Puzzle
    {
        public Day05Puzzle01(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            var polymer = InputReader.ReadLines().First();
            var reactor = new PolymerReactor();
            return reactor.ReactPolymerUnits(polymer).Length.ToString();
        }
    }
}