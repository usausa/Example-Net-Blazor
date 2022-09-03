namespace ChartExample;

public static class Extensions
{
    public static DateTime TruncateSecond(this DateTime value) =>
        new(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
}
