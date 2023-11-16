using Netmon.Models.Component.Interface.Metric;

namespace Netmon.DeviceManager.DTO.Device.Components;

public class InterfaceMetricsDTO
{
    public DateTime Timestamp { get; set; }
    public ulong InOctets { get; set; }
    public ulong OutOctets { get; set; }

    public static InterfaceMetricsDTO FromInterfaceMetric(IInterfaceMetric metric)
    {
        return new InterfaceMetricsDTO
        {
            Timestamp = metric.Timestamp,
            InOctets = metric.InOctets,
            OutOctets = metric.OutOctets
        };
    }
}