using AdventCalendar.Day01;
using System.Collections.Generic;

namespace AdventCalendar
{
    internal abstract class Puzzle : IPuzzle
    {
        protected readonly IInputReader InputReader;
        protected IEnumerable<string> Lines { get; }

        protected Puzzle(IInputReader inputReader)
        {
            InputReader = inputReader;
            Lines = inputReader.ReadLines();
        }

        public abstract string Solve();
    }
}