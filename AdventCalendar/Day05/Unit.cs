using System.Collections.Generic;

namespace AdventCalendar.Day05
{
    internal class Unit
    {
        private Unit(char type)
        {
            Type = char.ToLowerInvariant(type);
            Polarity = char.IsUpper(type) ? Polarity.Up : Polarity.Down;
        }

        public char Type { get; }

        public Polarity Polarity { get; }

        public static Unit[] ConvertPolymer(string polymer)
        {
            var units = new List<Unit>();
            foreach (var unit in polymer)
            {
                units.Add(new Unit(unit));
            }

            return units.ToArray();
        }

        public override string ToString()
        {
            switch (Polarity)
            {
                case Polarity.Down:
                    return Type.ToString().ToLowerInvariant();

                case Polarity.Up:
                    return Type.ToString().ToUpperInvariant();

                default:
                    throw new System.Exception("Don't event know what this is");
            }
        }
    }
}