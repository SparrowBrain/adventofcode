using AdventCalendar.Day07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class StepFactory
{
    private readonly StepSettings _stepSettings;
    private const int AsciiOffset = 64;
    private static readonly Regex StepPattern = new Regex(@"Step (?<step1>\w) must be finished before step (?<step2>\w) can begin\.", RegexOptions.Compiled);

    public StepFactory(StepSettings stepSettings)
    {
        _stepSettings = stepSettings;
    }

    public HashSet<Step> Create(IEnumerable<string> lines)
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

            var step1 = steps.FirstOrDefault(x => x.Name == step1Name) ?? new Step(step1Name, ResolveDuration(step1Name));
            var step2 = steps.FirstOrDefault(x => x.Name == step2Name) ?? new Step(step2Name, ResolveDuration(step2Name));

            step1.AddNext(step2);

            steps.Add(step1);
            steps.Add(step2);
        }

        return steps;
    }

    private int ResolveDuration(char stepName)
    {
        return (int)stepName - AsciiOffset + _stepSettings.DurationOffset;
    }
}