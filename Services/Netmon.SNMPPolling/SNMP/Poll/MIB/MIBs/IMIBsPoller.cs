using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIBs;

public interface IMIBsPoller
{
    public Task<List<IMIB>> PollAllMIBs(SNMPConnectionInfo snmpConnectionInfo);
    
    public Task<SystemMIB> PollSystemMIB(SNMPConnectionInfo snmpConnectionInfo);
}