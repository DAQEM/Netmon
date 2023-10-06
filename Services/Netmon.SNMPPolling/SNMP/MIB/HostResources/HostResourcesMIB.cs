using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources;

public class HostResourcesMIB : IMIB
{
    public static readonly string OID = "1.3.6.1.2.1.25";
    
    public HrStorage HrStorage { get; set; } = new();
    public HrDevice HrDevice { get; set; } = new();
}