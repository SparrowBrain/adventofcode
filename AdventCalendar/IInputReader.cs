using System.Collections.Generic;

namespace AdventCalendar
{
    public interface IInputReader
    {
        IEnumerable<string> ReadLines(string path);
    }
}