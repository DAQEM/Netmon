﻿namespace Netmon.Models.Component.Disk.Metric;

public interface IDiskMetric : IComponentMetric
{
    public DateTime Timestamp { get; set; }
    public int AllocationUnits { get; set; }
    public int TotalSpace { get; set; }
    public int UsedSpace { get; set; }
}