namespace ChartExample.Components;

public static class DefaultTimeAxisCalculator
{
    private static readonly TimeSpan MaxCandidateValue = TimeSpan.FromDays(1);

    private static readonly TimeSpan[] CandidateValues =
    {
        TimeSpan.FromMinutes(1),
        TimeSpan.FromMinutes(2),
        TimeSpan.FromMinutes(5),
        TimeSpan.FromMinutes(10),
        TimeSpan.FromMinutes(15),
        TimeSpan.FromMinutes(30),
        TimeSpan.FromHours(1),
        TimeSpan.FromHours(2),
        TimeSpan.FromHours(3),
        TimeSpan.FromHours(4),
        TimeSpan.FromHours(6),
        TimeSpan.FromHours(12)
    };

    public static TimeSpan Calc(TimeSpan range)
    {
        var value = range / 6;
        foreach (var candidateValue in CandidateValues)
        {
            if (value <= candidateValue)
            {
                return candidateValue;
            }
        }

        return MaxCandidateValue;
    }
}
