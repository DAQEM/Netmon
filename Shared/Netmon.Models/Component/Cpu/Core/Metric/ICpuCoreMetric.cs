namespace Netmon.Models.Component.Cpu.Core.Metric;

public interface ICpuCoreMetric
{
    public DateTime Timestamp { get; set; }
    public int Load { get; set; }
}