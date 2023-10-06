using Netmon.SNMPPolling.SNMP.MIB.If.Interface;
using Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;

namespace Netmon.SNMPPolling.SNMP.MIB.If;

public class IfMIB : IMIB
{
    public IfTable IfTable { get; set; } = new();
    public IfXTable IfXTable { get; set; } = new();
}