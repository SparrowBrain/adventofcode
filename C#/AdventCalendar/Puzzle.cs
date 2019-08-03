using AdventCalendar.Day01;
using System.Collections.Generic;

namespace AdventCalendar
{
    internal abstract class Puzzle : IPuzzle
    {
        protected readonly IInputReader InputReader;

        protected Puzzle(IInputReader inputReader)
        {
            InputReader = inputReader;
        }

        public abstract string Solve();
    }
}