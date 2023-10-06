﻿namespace Netmon.Models.Component.Memory.Metric;

public interface IMemoryMetric
{
    public DateTime Timestamp { get; set; }
    public int AllocationUnits { get; set; }
    public int TotalSpace { get; set; }
    public int UsedSpace { get; set; }
}