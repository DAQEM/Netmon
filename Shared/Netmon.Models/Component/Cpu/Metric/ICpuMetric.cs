namespace Netmon.Models.Component.Cpu.Metric;

public interface ICpuMetric : IComponentMetric
{
    public DateTime Timestamp { get; set; }
    
    public int OneMinuteLoad { get; set; }
    
    public int FiveMinuteLoad { get; set; }
    
    public int FifteenMinuteLoad { get; set; }
}