using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day05
{
    internal class Day05Puzzle02 : Puzzle
    {
        public Day05Puzzle02(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            var polymer = InputReader.ReadLines().First();
            var units = Unit.ConvertPolymer(polymer);
            var reactor = new PolymerReactor();

            var shortestAfterReaction = new List<Unit>(units);

            foreach (var type in units.Select(x => x.Type).Distinct())
            {
                var modifiedPolymer = units.Where(x => x.Type != type);
                var afterReaction = reactor.ReactPolymerUnits(modifiedPolymer);
                if (afterReaction.Count() < shortestAfterReaction.Count)
                {
                    shortestAfterReaction = afterReaction.ToList();
                }
            }

            return shortestAfterReaction.Count.ToString();
        }
    }
}