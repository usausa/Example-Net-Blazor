namespace ChartExample.Components;

public class ChartBuildOption
{
    public string TimeFormat { get; set; } = "H:mm";

    public string DateFormat { get; set; } = "M/dd";

    public string Value1Format { get; set; } = "F2";

    public string Value2Format { get; set; } = "F2";

    public string Label1 { get; set; } = string.Empty;

    public string Label2 { get; set; } = string.Empty;

    public Func<TimeSpan, TimeSpan> TimeAxisCalculator { get; set; } = DefaultTimeAxisCalculator.Calc;

    public Func<double, double> Value1AxisCalculator { get; set; } = DefaultValueAxisCalculator.Calc;

    public Func<double, double> Value2AxisCalculator { get; set; } = DefaultValueAxisCalculator.Calc;

    public string[] ValuePalette { get; set; } =
    {
        "#00e676",
        "#2979ff",
        "#00e5ff"
    };

    public string[] ThresholdPalette { get; set; } =
    {
        "#ff9100",
        "#ff1744",
        "#651fff"
    };
}
