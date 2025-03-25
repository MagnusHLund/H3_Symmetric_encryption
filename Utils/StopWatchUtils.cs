using System.Diagnostics;

namespace H3_Symmetric_encryption.Utils
{
    public static class StopWatchUtils
    {
        public static double ConvertTicksToMilliSeconds(long ticks)
        {
            return (double)ticks / Stopwatch.Frequency * 1000.0;
        }
    }
}