using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.MIB.UCDavis;

public class UCDavisMIB : IMIB
{
    public LaLoadTable LaLoadTable { get; set; } = new();
}