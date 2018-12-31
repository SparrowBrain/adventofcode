using System.Collections.Generic;
using AdventCalendar.Day01;

namespace AdventCalendar
{
    internal abstract class Puzzle : IPuzzle
    {
        protected readonly IInputReader _inputReader;
        protected IEnumerable<string> Lines { get; }

        public Puzzle(IInputReader inputReader)
        {
            _inputReader = inputReader;
            Lines = inputReader.ReadLines();
        }

        public abstract string Solve();
    }
}