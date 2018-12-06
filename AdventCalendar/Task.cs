﻿using System.Collections.Generic;
using System.IO;
using AdventCalendar.Day01;

namespace AdventCalendar.Day02
{
    internal abstract class Task : ITask
    {
        protected IEnumerable<string> Lines { get; }
        
        protected abstract string Path { get;}

        public Task()
        {
            Lines = File.ReadAllLines(Path);
        }

        public abstract string Solve();
    }
}