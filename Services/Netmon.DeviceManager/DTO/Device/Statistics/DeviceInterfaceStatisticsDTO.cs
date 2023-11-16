using System.Net.NetworkInformation;
using Netmon.DeviceManager.DTO.Device.Components;
using Netmon.Models.Component.Interface;

namespace Netmon.DeviceManager.DTO.Device.Statistics;

public class DeviceInterfaceStatisticsDTO
{
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    public InterfaceType Type { get; set; }
    public PhysicalAddress PhysAddress { get; set; } = null!;
    public List<InterfaceMetricsDTO> Metrics { get; set; } = null!;
    
    public static DeviceInterfaceStatisticsDTO FromDisk(IInterface @interface)
    {
        return new DeviceInterfaceStatisticsDTO
        {
            Index = @interface.Index,
            Name = @interface.Name,
            Type = @interface.Type,
            PhysAddress = @interface.PhysAddress,
            Metrics = @interface.Metrics.Select(InterfaceMetricsDTO.FromInterfaceMetric).ToList()
        };
    }
}