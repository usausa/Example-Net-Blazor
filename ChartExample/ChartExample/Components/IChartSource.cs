namespace ChartExample.Components;

public interface IChartSource
{
    int Size { get; }

    DateTime? MinDateTime { get; }

    DateTime? MaxDateTime { get; }

    double? MaxValue1 { get; }

    double? MaxValue2 { get; }

    int Value1Count { get; }

    int Value2Count { get; }

    int ThresholdCount { get; }

    IList<LegendEntry> Legends { get; }

    DateTime GetTime(int index);

    double? GetValue1(int item, int index);

    double? GetValue2(int item, int index);

    double GetThreshold(int item, int index);
}
