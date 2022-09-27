namespace ChartExample.Components;

public static class ChartSourceExtensions
{
    public static double? FindMinValue1(this IChartSource source, int item)
    {
        var min = default(double?);
        for (var index = 0; index < source.Size; index++)
        {
            var value = source.GetValue1(item, index);
            if ((value is not null) && ((min is null) || (value.Value < min.Value)))
            {
                min = value.Value;
            }
        }

        return min;
    }

    public static double? FindMaxValue1(this IChartSource source, int item)
    {
        var max = default(double?);
        for (var index = 0; index < source.Size; index++)
        {
            var value = source.GetValue1(item, index);
            if ((value is not null) && ((max is null) || (value.Value > max.Value)))
            {
                max = value.Value;
            }
        }

        return max;
    }

    public static double? FindMinValue2(this IChartSource source, int item)
    {
        var min = default(double?);
        for (var index = 0; index < source.Size; index++)
        {
            var value = source.GetValue2(item, index);
            if ((value is not null) && ((min is null) || (value.Value < min.Value)))
            {
                min = value.Value;
            }
        }

        return min;
    }

    public static double? FindMaxValue2(this IChartSource source, int item)
    {
        var max = default(double?);
        for (var index = 0; index < source.Size; index++)
        {
            var value = source.GetValue2(item, index);
            if ((value is not null) && ((max is null) || (value.Value > max.Value)))
            {
                max = value.Value;
            }
        }

        return max;
    }

    public static double? FindThresholdValue(this IChartSource source, int item) =>
        source.Size > 0 ? source.GetThreshold(item, source.Size - 1) : null;

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
