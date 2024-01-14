using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

public class SystemMIBPoller(ISNMPManager snmpManager) : IMIBPoller<SystemMIB>
{
    public async Task<SystemMIB?> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        ISNMPResult sysSystemResult = await snmpManager.BulkWalkAsync(connectionInfo, SystemMIB.OID, 3000);
        
        if (!sysSystemResult.Variables.Any()) return null;
        
        return SystemMIB.Deserializer.Deserialize(sysSystemResult);
    }
}