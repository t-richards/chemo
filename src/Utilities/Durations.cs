using System;

namespace Chemo.Utilities
{
    public static class Durations
    {
        /// <summary>
        /// Describes a duration the way a person would say it, such as "under a second" or "1 minute 5 seconds".
        /// </summary>
        public static string Humanize(TimeSpan duration)
        {
            if (duration < TimeSpan.FromSeconds(1))
            {
                return "under a second";
            }

            int totalSeconds = (int)Math.Round(duration.TotalSeconds, MidpointRounding.AwayFromZero);
            if (totalSeconds < 60)
            {
                return Count(totalSeconds, "second");
            }

            int totalMinutes = totalSeconds / 60;
            if (totalMinutes < 60)
            {
                return Join(Count(totalMinutes, "minute"), totalSeconds % 60, "second");
            }

            return Join(Count(totalMinutes / 60, "hour"), totalMinutes % 60, "minute");
        }

        private static string Join(string larger, int smaller, string smallerUnit)
        {
            return smaller == 0 ? larger : $"{larger} {Count(smaller, smallerUnit)}";
        }

        private static string Count(int count, string unit)
        {
            return count == 1 ? $"1 {unit}" : $"{count} {unit}s";
        }
    }
}
