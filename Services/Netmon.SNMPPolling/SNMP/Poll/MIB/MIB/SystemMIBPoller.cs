using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

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