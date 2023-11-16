using Netmon.Models.Component.Interface;

namespace Netmon.DeviceManager.DTO.Device.Statistics;

public class DeviceInterfacesStatisticsDTO
{
    public List<DeviceInterfaceStatisticsDTO> Interfaces { get; set; }

    public static DeviceInterfacesStatisticsDTO FromInterfaces(List<IInterface> interfaces)
    {
        return new DeviceInterfacesStatisticsDTO
        {
            Interfaces = interfaces.Select(DeviceInterfaceStatisticsDTO.FromDisk).ToList()
        };
    }
}