using System;

public static class TimeUtil
{
    private static readonly DateTime EpochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static double UtcNowMillis()
    {
        return DateTime.UtcNow.Subtract(EpochUtc).TotalMilliseconds;
    }
}