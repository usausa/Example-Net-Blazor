namespace ChartExample.Components;

public static class DefaultValueAxisCalculator
{
    public static double Calc(double value)
    {
        var baseValue = 30;
        var log = 0;
        while (value >= baseValue)
        {
            baseValue *= 10;
            log++;
        }

        var pow = (int)Math.Pow(10, log);

        if (value < 6 * pow)
        {
            return pow;
        }
        if (value < 12 * pow)
        {
            return 2 * pow;
        }
        if (value < 15 * pow)
        {
            return 2.5 * pow;
        }

        return 5 * pow;
    }
}
