using System.Linq;

namespace AdventCalendar.Day04
{
    internal class ScheduleScrubber
    {
        private readonly GuardMinuteFactory _guardMinuteFactory;

        public ScheduleScrubber(GuardMinuteFactory guardMinuteFactory)
        {
            _guardMinuteFactory = guardMinuteFactory;
        }

        public int MostAsleepGuard()
        {
            var guardMinutes = _guardMinuteFactory.GenerateGuardMinutes();

            return guardMinutes.GroupBy(x => x.GuardId).OrderByDescending(x => x.Count()).First().Key;
        }

        public int MostAsleepGuardMinute(int guardId)
        {
            var guardMinutes = _guardMinuteFactory.GenerateGuardMinutes();
            guardMinutes = guardMinutes.Where(x => x.GuardId == guardId);

            return guardMinutes.GroupBy(x => x.Minute).OrderByDescending(x => x.Count()).First().Key;
        }

        public (int, int) MostAsleepGuardOnSameMinute()
        {
            var minutes = _guardMinuteFactory.GenerateGuardMinutes();

            var minuteGroups = (minutes.GroupBy(x => x));
            minuteGroups = minuteGroups.OrderByDescending(x => x.Count());

            var mostFrequentGuardMinute = minuteGroups.First();
            var guardId = mostFrequentGuardMinute.Key.GuardId;
            var minute = mostFrequentGuardMinute.Key.Minute;

            return (guardId, minute);
        }
    }
}