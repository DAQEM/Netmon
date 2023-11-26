using Netmon.SNMPPolling.SNMP.MIB.UCDavis.CpuLoad;

namespace Netmon.SNMPPolling.SNMP.MIB.UCDavis;

public class UCDavisMIB : IMIB
{
    public LaLoadTable LaLoadTable { get; set; } = new();
}