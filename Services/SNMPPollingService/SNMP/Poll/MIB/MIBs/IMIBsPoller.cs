using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.MIB.MIBs;

public interface IMIBsPoller
{
    public Task<List<IMIB>> PollAllMIBs(SNMPConnectionInfo snmpConnectionInfo);
    
    public Task<SystemMIB> PollSystemMIB(SNMPConnectionInfo snmpConnectionInfo);
}