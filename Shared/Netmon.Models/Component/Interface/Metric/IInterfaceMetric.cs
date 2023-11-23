namespace Netmon.Models.Component.Interface.Metric;

public interface IInterfaceMetric : IComponentMetric
{
    public DateTime Timestamp { get; set; }
    public InterfaceStatus AdminStatus { get; set; }
    public InterfaceStatus OperationStatus { get; set; }
    public uint Speed { get; set; }
    public long Mtu { get; set; }
    public ulong InOctets { get; set; }
    public ulong OutOctets { get; set; }
    public ulong InErrors { get; set; }
    public ulong OutErrors { get; set; }
    public ulong InDiscards { get; set; }
    public ulong OutDiscards { get; set; }
    public ulong InBroadcastPackets { get; set; }
    public ulong OutBroadcastPackets { get; set; }
    public ulong InMulticastPackets { get; set; }
    public ulong OutMulticastPackets { get; set; }
    public ulong InUnicastPackets { get; set; }
    public ulong OutUnicastPackets { get; set; }
}