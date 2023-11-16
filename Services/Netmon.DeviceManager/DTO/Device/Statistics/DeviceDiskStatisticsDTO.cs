using Netmon.DeviceManager.DTO.Device.Components;
using Netmon.Models.Component.Disk;

namespace Netmon.DeviceManager.DTO.Device.Statistics;

public class DeviceDiskStatisticsDTO
{
    public int Index { get; set; }
    public string MountingPoint { get; set; } = null!;
    public List<DeviceDiskMetricDTO> Metrics { get; set; } = null!;
    
    public static DeviceDiskStatisticsDTO FromDisk(IDisk disk)
    {
        return new DeviceDiskStatisticsDTO
        {
            Index = disk.Index,
            MountingPoint = disk.MountingPoint,
            Metrics = disk.Metrics.Select(DeviceDiskMetricDTO.FromDiskMetric).ToList()
        };
    }
}