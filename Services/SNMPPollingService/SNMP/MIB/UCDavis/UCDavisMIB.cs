using SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;

namespace SNMPPollingService.SNMP.MIB.UCDavis;

public class UCDavisMIB : IMIB
{
    public LaLoadTable LaLoadTable { get; set; } = new();
}