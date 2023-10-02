using System.Net.NetworkInformation;

namespace DevicesLib.Entities.Component.Interface;

public class Interface : IInterface
{
    public int Index { get; set; }
    public string Name { get; set; }
    public InterfaceType Type { get; set; }
    public InterfaceStatus AdminStatus { get; set; }
    public InterfaceStatus OperationStatus { get; set; }
    public uint Speed { get; set; }
    public PhysicalAddress PhysAddress { get; set; }
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

    public Interface(int index, string name, InterfaceType type, InterfaceStatus adminStatus, InterfaceStatus operationStatus, uint speed, PhysicalAddress physAddress, long mtu, ulong inOctets, ulong outOctets, ulong inErrors, ulong outErrors, ulong inDiscards, ulong outDiscards, ulong inBroadcastPackets, ulong outBroadcastPackets, ulong inMulticastPackets, ulong outMulticastPackets, ulong inUnicastPackets, ulong outUnicastPackets)
    {
        Index = index;
        Name = name;
        Type = type;
        AdminStatus = adminStatus;
        OperationStatus = operationStatus;
        Speed = speed;
        PhysAddress = physAddress;
        Mtu = mtu;
        InOctets = inOctets;
        OutOctets = outOctets;
        InErrors = inErrors;
        OutErrors = outErrors;
        InDiscards = inDiscards;
        OutDiscards = outDiscards;
        InBroadcastPackets = inBroadcastPackets;
        OutBroadcastPackets = outBroadcastPackets;
        InMulticastPackets = inMulticastPackets;
        OutMulticastPackets = outMulticastPackets;
        InUnicastPackets = inUnicastPackets;
        OutUnicastPackets = outUnicastPackets;
    }
}