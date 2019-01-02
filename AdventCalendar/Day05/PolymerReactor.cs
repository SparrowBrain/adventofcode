using System;
using System.Linq;
using System.Text;

namespace AdventCalendar.Day05
{
    internal class PolymerReactor
    {
        public string ReactPolymerUnits(string polymer)
        {
            var newPolymer = new StringBuilder(polymer);
            bool reactionHappened;

            do
            {
                polymer = newPolymer.ToString();
                newPolymer = new StringBuilder();
                reactionHappened = false;
                for (var i = 0; i < polymer.Length - 1; i++)
                {
                    var unit1 = polymer.Substring(i, 1);
                    var unit2 = polymer.Substring(i + 1, 1);
                    if (Reacts(unit1, unit2))
                    {
                        i++;
                        reactionHappened = true;
                        continue;
                    }

                    newPolymer.Append(polymer[i]);
                }

                var beforeLastUnit = polymer.SkipLast(1).Last().ToString();
                var lastUnit = polymer.Last().ToString();
                if (!Reacts(beforeLastUnit, lastUnit))
                {
                    newPolymer.Append(polymer.Last());
                }
            } while (newPolymer.Length > 0 && reactionHappened);

            return newPolymer.ToString();
        }

        private bool Reacts(string unit1, string unit2)
        {
            return SameUnitType(unit1, unit2) && DifferentUnitOrDifferentPolarity(unit1, unit2);
        }

        private static bool DifferentUnitOrDifferentPolarity(string unit1, string unit2)
        {
            return !string.Equals(unit1, unit2);
        }

        private bool SameUnitType(string unit1, string unit2)
        {
            return string.Equals(unit1, unit2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}