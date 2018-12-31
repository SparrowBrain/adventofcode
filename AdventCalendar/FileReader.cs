using System.Collections.Generic;
using System.IO;

namespace AdventCalendar
{
    internal class FileReader : IInputReader
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public IEnumerable<string> ReadLines()
        {
            return File.ReadAllLines(_path);
        }
    }
}