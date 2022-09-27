namespace ChartExample.Components;

public sealed class LegendEntry
{
    public string Value { get; }

    public string Threshold1 { get; }

    public string Threshold2 { get; }

    public LegendEntry(string value, string threshold1 = "", string threshold2 = "")
    {
        Value = value;
        Threshold1 = threshold1;
        Threshold2 = threshold2;
    }
}
