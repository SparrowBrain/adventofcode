using System.Collections.Generic;
using System.IO;

namespace AdventCalendar
{
    internal class FileReader : IInputReader
    {
        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}