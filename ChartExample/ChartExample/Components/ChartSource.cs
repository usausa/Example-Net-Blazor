namespace ChartExample.Components;

using System.Runtime.CompilerServices;

public class ChartSource<T> : IChartSource
{
    private readonly IList<T> list;

    public int Size => list.Count;

    public DateTime? MinDateTime { get; }

    public DateTime? MaxDateTime { get; }

    public double? MaxValue1 { get; }

    public double? MaxValue2 { get; }

    public Func<T, DateTime> TimeSelector { get; set; } = default!;

    public List<Func<T, double?>> Value1Selectors { get; } = new();

    public List<Func<T, double?>> Value2Selectors { get; } = new();

    public List<Func<T, double>> Threshold1Selectors { get; } = new();

    public List<string> ValueLegends { get; } = new();

    public List<string> ThresholdLegends { get; } = new();

    public int Value1Count => Value1Selectors.Count;

    public int Value2Count => Value2Selectors.Count;

    public int Threshold1Count => Threshold1Selectors.Count;

    public ChartSource(IList<T> list, DateTime? minDateTime = null, DateTime? maxDateTime = null, double? maxValue1 = null, double? maxValue2 = null)
    {
        this.list = list;
        MinDateTime = minDateTime;
        MaxDateTime = maxDateTime;
        MaxValue1 = maxValue1;
        MaxValue2 = maxValue2;
    }

    public string GetValueLegend(int item) => ValueLegends[item];

    public string GetThresholdLegend(int item) => ThresholdLegends[item];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DateTime GetTime(int index) => TimeSelector(list[index]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double? GetValue1(int item, int index) => Value1Selectors[item](list[index]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double? GetValue2(int item, int index) => Value2Selectors[item](list[index]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetThreshold1(int item, int index) => Threshold1Selectors[item](list[index]);
}
