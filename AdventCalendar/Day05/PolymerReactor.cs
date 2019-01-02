using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day05
{
    internal class PolymerReactor
    {
        public IEnumerable<Unit> ReactPolymerUnits(IEnumerable<Unit> originalPolymer)
        {
            var polymer = originalPolymer.ToArray();
            var newPolymer = new List<Unit>(polymer);
            bool reactionHappened;

            do
            {
                polymer = newPolymer.ToArray();
                newPolymer = new List<Unit>();
                reactionHappened = false;
                for (var i = 0; i < polymer.Length - 1; i++)
                {
                    var unit1 = polymer[i];
                    var unit2 = polymer[i + 1];
                    if (Reacts(unit1, unit2))
                    {
                        i++;
                        reactionHappened = true;
                        continue;
                    }

                    newPolymer.Add(polymer[i]);
                }

                var beforeLastUnit = polymer.SkipLast(1).Last();
                var lastUnit = polymer.Last();
                if (!Reacts(beforeLastUnit, lastUnit))
                {
                    newPolymer.Add(polymer.Last());
                }
            } while (newPolymer.Any() && reactionHappened);

            return newPolymer;
        }

        private bool Reacts(Unit unit1, Unit unit2)
        {
            return SameUnitType(unit1, unit2) && !SamePolarity(unit1, unit2);
        }

        private bool SameUnitType(Unit unit1, Unit unit2)
        {
            return unit1.Type == unit2.Type;
        }

        private static bool SamePolarity(Unit unit1, Unit unit2)
        {
            return unit1.Polarity == unit2.Polarity;
        }
    }
}