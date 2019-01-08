using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day07
{
    internal class Step
    {
        public char Name { get; }

        public Step(char name, int duration)
        {
            Name = name;
            Duration = duration;
        }

        public HashSet<Step> PrerequisiteSteps { get; } = new HashSet<Step>();
        public HashSet<Step> NextSteps { get; } = new HashSet<Step>();

        public void AddNext(Step next)
        {
            NextSteps.Add(next);

            next.PrerequisiteSteps.Add(this);
        }

        public void PerformStep()
        {
            foreach (var step in NextSteps)
            {
                step.PrerequisiteSteps.Remove(this);
            }

            Done = true;
        }

        public bool Done { get; private set; }

        public int Duration { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Step other && Equals(other);
        }

        protected bool Equals(Step other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}