using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources;

public class HostResourcesMIB : IMIB
{
    public HrStorage HrStorage { get; set; } = new();
    public HrDevice HrDevice { get; set; } = new();
}