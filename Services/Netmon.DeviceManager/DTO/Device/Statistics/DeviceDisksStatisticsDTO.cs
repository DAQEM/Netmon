using Netmon.Models.Component.Disk;

namespace Netmon.DeviceManager.DTO.Device.Statistics;

public class DeviceDisksStatisticsDTO
{
    public List<DeviceDiskStatisticsDTO> Disks { get; set; }

    public static DeviceDisksStatisticsDTO FromDisks(List<IDisk> disks)
    {
        return new DeviceDisksStatisticsDTO
        {
            Disks = disks.Select(DeviceDiskStatisticsDTO.FromDisk).ToList()
        };
    }
}