using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.UCDavis;
using SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.MIB.MIB;

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