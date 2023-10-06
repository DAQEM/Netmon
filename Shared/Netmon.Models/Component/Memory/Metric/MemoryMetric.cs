namespace Netmon.Models.Component.Memory.Metric;

public class MemoryMetric : IMemoryMetric
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int AllocationUnits { get; set; }
    public int TotalSpace { get; set; }
    public int UsedSpace { get; set; }
}