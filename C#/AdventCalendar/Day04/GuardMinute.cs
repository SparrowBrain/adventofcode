namespace AdventCalendar.Day04
{
    internal class GuardMinute
    {
        public GuardMinute(int guardId, int minute)
        {
            GuardId = guardId;
            Minute = minute;
        }

        public int GuardId { get; }
        public int Minute { get; }

        protected bool Equals(GuardMinute other)
        {
            return GuardId == other.GuardId && Minute == other.Minute;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (GuardId * 397) ^ Minute;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is GuardMinute guardMinute && Equals(guardMinute);
        }
    }
}