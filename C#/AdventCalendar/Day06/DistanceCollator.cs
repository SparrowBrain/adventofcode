using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day06
{
    internal class DistanceCollator
    {
        public int[][] AddUpDistances(IEnumerable<Vector[][]> fields)
        {
            var combinedField = new int[fields.First().Length][];
            for (var i = 0; i < combinedField.Length; i++)
            {
                combinedField[i] = new int[fields.First()[0].Length];
            }

            foreach (var field in fields)
            {
                for (var x = 0; x < field.Length; x++)
                {
                    for (var y = 0; y < field[0].Length; y++)
                    {
                        combinedField[x][y] += field[x][y].Distance;
                    }
                }
            }

            return combinedField;
        }
    }
}