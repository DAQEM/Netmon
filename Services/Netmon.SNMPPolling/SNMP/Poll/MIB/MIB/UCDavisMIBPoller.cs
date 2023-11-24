using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis.CpuLoad;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

public class UCDavisMIBPoller : IMIBPoller<UCDavisMIB>
{
    private readonly ISNMPManager snmpManager;
    
    public UCDavisMIBPoller(ISNMPManager snmpManager)
    {
        this.snmpManager = snmpManager;
    }
    
    public async Task<UCDavisMIB?> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        ISNMPResult snmpResult = await snmpManager.BulkWalkAsync(connectionInfo, LaLoadTable.OID, 3000);

        if (!snmpResult.Variables.Any()) return null;
        
        return new UCDavisMIB
        {
            LaLoadTable = LaLoadTable.Deserializer.Deserialize(snmpResult)
        };
    }
}