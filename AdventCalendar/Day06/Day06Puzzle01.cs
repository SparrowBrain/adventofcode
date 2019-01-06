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
            var board = new BoardFactory(sources).CreateBoard();
            var fieldGenerator = new FieldGenerator(board);

            var fields = fieldGenerator.CreateFields(sources);

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