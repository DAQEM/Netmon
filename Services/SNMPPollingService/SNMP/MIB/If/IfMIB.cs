using SNMPPollingService.SNMP.MIB.If.Interface;
using SNMPPollingService.SNMP.MIB.If.InterfaceX;

namespace SNMPPollingService.SNMP.MIB.If;

public class IfMIB : IMIB
{
    public IfTable IfTable { get; set; } = new();
    public IfXTable IfXTable { get; set; } = new();
}