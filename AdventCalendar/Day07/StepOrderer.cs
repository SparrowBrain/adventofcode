using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCalendar.Day07
{
    internal class StepOrderer
    {
        public string Order(HashSet<Step> steps)
        {
            var stepOrder = new StringBuilder();
            

            while (steps.Any(x=>!x.Done))
            {
                var nextStep = steps.Where(x => !x.Done && x.PrerequisiteSteps.Count == 0).OrderBy(x => x.Name).First();
                nextStep.PerformStep();

                stepOrder.Append(nextStep.Name);
            }

            return stepOrder.ToString();
        }
    }
}