namespace ChartExample.Components;

public static class ChartSourceExtensions
{
    public static double CalcMax1(this IChartSource source)
    {
        var max = 0d;
        for (var item = 0; item < source.Value1Count; item++)
        {
            for (var index = 0; index < source.Size; index++)
            {
                var value = source.GetValue1(item, index);
                if (value > max)
                {
                    max = value.Value;
                }
            }
        }

        for (var item = 0; item < source.ThresholdCount; item++)
        {
            for (var index = 0; index < source.Size; index++)
            {
                var value = source.GetThreshold(item, index);
                if (value > max)
                {
                    max = value;
                }
            }
        }

        return max;
    }

    public static double CalcMax2(this IChartSource source)
    {
        var max = 0d;
        for (var item = 0; item < source.Value2Count; item++)
        {
            for (var index = 0; index < source.Size; index++)
            {
                var value = source.GetValue2(item, index);
                if (value > max)
                {
                    max = value.Value;
                }
            }
        }

        return max;
    }
}
