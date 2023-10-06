namespace Netmon.Models.Component.Cpu.Metric;

public class CpuMetric : ICpuMetric
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int OneMinuteLoad { get; set; }
    public int FiveMinuteLoad { get; set; }
    public int FifteenMinuteLoad { get; set; }
}