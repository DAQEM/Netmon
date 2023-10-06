namespace Netmon.Models.Component.Disk.Metric;

public class DiskMetric : IDiskMetric
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int AllocationUnits { get; set; }
    public int TotalSpace { get; set; }
    public int UsedSpace { get; set; }
}