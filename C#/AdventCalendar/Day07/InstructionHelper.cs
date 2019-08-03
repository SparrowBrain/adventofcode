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

            while (steps.Any(x => x.StepState != StepState.Done))
            {
                var nextStep = NextAvailableStep(steps);
                nextStep.PerformStep();

                stepOrder.Append(nextStep.Name);
            }

            return stepOrder.ToString();
        }

        private static Step NextAvailableStep(HashSet<Step> steps)
        {
            return steps.Where(x => x.StepState == StepState.Idle && x.PrerequisiteSteps.Count == 0).OrderBy(x => x.Name).FirstOrDefault();
        }

        public int TimeToAssemble(HashSet<Step> steps, int workerCount)
        {
            var stepsInProgress = new Worker[workerCount];
            for (var i = 0; i < workerCount; i++)
            {
                stepsInProgress[i] = new Worker();
            }

            var seconds = 0;
            while (steps.Any(x => x.StepState != StepState.Done))
            {
                seconds++;

                var availableWorkerCount = stepsInProgress.Count(x => x.Step == null);
                for (var i = 0; i < availableWorkerCount; i++)
                {
                    var nextStep = NextAvailableStep(steps);
                    if (nextStep == null)
                    {
                        break;
                    }

                    var availableWorker = stepsInProgress.First(x => x.Step == null);
                    availableWorker.Step = nextStep;
                    availableWorker.Work = new Work() { TimeLeft = nextStep.Duration };
                    nextStep.StartStep();
                }

                for (var i = 0; i < stepsInProgress.Length; i++)
                {
                    if (stepsInProgress[i].Step == null)
                    {
                        continue;
                    }

                    stepsInProgress[i].Work.TimeLeft--;

                    if (stepsInProgress[i].Work.TimeLeft == 0)
                    {
                        stepsInProgress[i].Step.PerformStep();
                        stepsInProgress[i] = new Worker();
                    }
                }
            }

            return seconds;
        }
    }

    internal class Worker
    {
        public Step Step { get; set; }
        public Work Work { get; set; }
    }
}