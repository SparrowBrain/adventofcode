using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCalendar.Day07
{
    internal class InstructionHelper
    {
        public string Order(HashSet<Step> steps)
        {
            var stepOrder = new StringBuilder();

            while (steps.Any(x => !x.Done))
            {
                var nextStep = NextAvailableStep(steps);
                nextStep.PerformStep();

                stepOrder.Append(nextStep.Name);
            }

            return stepOrder.ToString();
        }

        private static Step NextAvailableStep(HashSet<Step> steps)
        {
            return steps.Where(x => !x.Done && x.PrerequisiteSteps.Count == 0).OrderBy(x => x.Name).First();
        }

        public int TimeToAssemble(HashSet<Step> steps, int workerCount)
        {
            var stepsInProgress = new KeyValuePair<Step, Work>[workerCount];

            var seconds = 0;
            while (steps.Any(x => !x.Done))
            {
                seconds++;

                var nextStep = NextAvailableStep(steps);
                if (nextStep != null && stepsInProgress.Any(x => x.Key == null))
                {
                    for (var i = 0; i < stepsInProgress.Length; i++)
                    {
                        if (stepsInProgress[i].Key == null)
                        {
                            stepsInProgress[i] = new KeyValuePair<Step, Work>(nextStep, new Work() { TimeLeft = nextStep.Duration });
                            break;
                        }
                    }
                }

                for (var i = 0; i < stepsInProgress.Length; i++)
                {
                    if (stepsInProgress[i].Key != null)
                    {
                        stepsInProgress[i].Value.TimeLeft--;

                        if (stepsInProgress[i].Value.TimeLeft == 0)
                        {
                            stepsInProgress[i].Key.PerformStep();
                            stepsInProgress[i] = new KeyValuePair<Step, Work>();
                        }
                    }
                }
            }

            return seconds;
        }
    }
}