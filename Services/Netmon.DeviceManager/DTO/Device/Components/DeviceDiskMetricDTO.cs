using Netmon.Models.Component.Disk.Metric;

namespace Netmon.DeviceManager.DTO.Device.Components;

public class DeviceDiskMetricDTO
{
    public DateTime Timestamp { get; set; }
    public int AllocationUnits { get; set; }
    public int TotalSpace { get; set; }
    public int UsedSpace { get; set; }
    
    public static DeviceDiskMetricDTO FromDiskMetric(IDiskMetric metric)
    {
        return new DeviceDiskMetricDTO
        {
            Timestamp = metric.Timestamp,
            AllocationUnits = metric.AllocationUnits,
            TotalSpace = metric.TotalSpace,
            UsedSpace = metric.UsedSpace
        };
    }
}