using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis.CpuLoad;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

public class UCDavisMIBPoller : IMIBPoller<UCDavisMIB>
{
    private readonly ISNMPManager snmpManager;
    
    public UCDavisMIBPoller(ISNMPManager snmpManager)
    {
        this.snmpManager = snmpManager;
    }
    
    public async Task<UCDavisMIB> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        return new UCDavisMIB
        {
            LaLoadTable = LaLoadTable.Deserializer.Deserialize(await snmpManager.BulkWalkAsync(connectionInfo, LaLoadTable.OID))
        };
    }
}