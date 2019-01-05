using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day06
{
    internal class BoardFactory
    {
        private readonly IEnumerable<Source> _sources;

        public BoardFactory(IEnumerable<Source> sources)
        {
            _sources = sources;
        }

        public Vector[][] CreateBoard()
        {
            var maxX = _sources.Max(p => p.X);
            var maxY = _sources.Max(p => p.Y);
            var fields = new Vector[maxX + 1][];
            for (var x = 0; x <= maxX; x++)
            {
                fields[x] = new Vector[maxY + 1];
            }

            return fields;
        }
    }
}