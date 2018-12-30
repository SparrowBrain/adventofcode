using System.Collections.Generic;
using AdventCalendar.Day01;

namespace AdventCalendar
{
    internal abstract class Puzzle : IPuzzle
    {
        private readonly IInputReader _inputReader;
        protected IEnumerable<string> Lines { get; }
        
        protected abstract string Path { get;}

        public Puzzle(IInputReader inputReader)
        {
            _inputReader = inputReader;
            Lines = inputReader.ReadLines(Path);
        }

        public abstract string Solve();
    }
}