using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day06
{
    internal class Day06Puzzle02 : Puzzle
    {
        private readonly int _limit;

        public Day06Puzzle02(IInputReader inputReader, int limit) : base(inputReader)
        {
            _limit = limit;
        }

        public override string Solve()
        {
            var sources = InputReader.ReadLines().Select(Source.Parse).ToList();
            var board = new BoardFactory(sources).CreateBoard();
            var fieldGenerator = new FieldGenerator(board);

            var fields = new List<Vector[][]>();
            foreach (var source in sources)
            {
                fields.Add(fieldGenerator.CreateFields(new List<Source> {source}));
            }

            var combinedDistanceField = new DistanceCollator().AddUpDistances(fields);

            return combinedDistanceField.SelectMany(x => x.ToList()).Count(x => x < _limit).ToString();
        }
    }
}