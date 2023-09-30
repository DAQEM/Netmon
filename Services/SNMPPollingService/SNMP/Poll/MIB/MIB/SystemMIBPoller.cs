using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.Poll.MIB.MIB;

public class SystemMIBPoller : IMIBPoller<SystemMIB>
{
    private readonly ISNMPManager _snmpManager;
    
    public SystemMIBPoller(ISNMPManager snmpManager)
    {
        _snmpManager = snmpManager;
    }
    
    public async Task<SystemMIB> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        ISNMPResult sysSystemResult = await _snmpManager.BulkWalkAsync(connectionInfo, SystemMIB.OID);
        return SystemMIB.Deserializer.Deserialize(sysSystemResult);
    }
}