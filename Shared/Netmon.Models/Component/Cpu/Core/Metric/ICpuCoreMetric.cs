namespace Netmon.Models.Component.Cpu.Core.Metric;

public interface ICpuCoreMetric : IComponentMetric
{
    public DateTime Timestamp { get; set; }
    public int Load { get; set; }
}