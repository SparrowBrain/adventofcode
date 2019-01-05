namespace AdventCalendar.Day06
{
    internal class Vector
    {
        public Vector(Source source, int distance)
        {
            Source = source;
            Distance = distance;
        }

        public Source Source { get; }
        public int Distance { get; }
        public static Vector Conflict => new Vector(null, int.MinValue);

        public override bool Equals(object obj)
        {
            return obj is Vector other && Equals(other);
        }

        protected bool Equals(Vector other)
        {
            return Equals(Source, other.Source) && Distance == other.Distance;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^ Distance;
            }
        }
    }
}