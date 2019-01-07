using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day07
{
    internal class Step
    {
        private static readonly Regex StepPattern = new Regex(@"Step (?<step1>\w) must be finished before step (?<step2>\w) can begin\.", RegexOptions.Compiled);
        public char Name { get; }

        public Step(char name)
        {
            Name = name;
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

        public static HashSet<Step> Create(IEnumerable<string> lines)
        {
            var steps = new HashSet<Step>();
            foreach (var line in lines)
            {
                var match = StepPattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Fake input");
                }

                var step1Name = match.Groups["step1"].Value.First();
                var step2Name = match.Groups["step2"].Value.First();

                var step1 = steps.FirstOrDefault(x => x.Name == step1Name) ?? new Step(step1Name);
                var step2 = steps.FirstOrDefault(x => x.Name == step2Name) ?? new Step(step2Name);

                step1.AddNext(step2);

                steps.Add(step1);
                steps.Add(step2);
            }

            return steps;
        }
    }
}