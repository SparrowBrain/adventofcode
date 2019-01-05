using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day06
{
    internal class Day06Puzzle01 : Puzzle
    {
        public Day06Puzzle01(IInputReader inputReader) : base(inputReader)
        {
        }

        public override string Solve()
        {
            var sources = InputReader.ReadLines().Select(Source.Parse).ToList();
            var fieldGenerator = new FieldGenerator(sources);

            var fields = fieldGenerator.CreateFields();

            var nonInfiniteVectors = fields.SelectMany(x => x.ToList()).Where(x => !Equals(x, Vector.Conflict) && x.Source.Infinity == false);

            var nonInfiniteSourceCount = new Dictionary<Source, int>();
            foreach (var source in sources)
            {
                nonInfiniteSourceCount[source] = nonInfiniteVectors.Count(x => Equals(x.Source, source));
            }

            return nonInfiniteSourceCount.OrderByDescending(x => x.Value).First().Value.ToString();
        }
    }
}