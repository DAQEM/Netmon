namespace Netmon.Models.Component.Cpu.Core.Metric;

public class CpuCoreMetric : ICpuCoreMetric
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public int Load { get; set; }
}