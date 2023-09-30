using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.UCDavis;

public class UCDavisMIB : IMIB<UCDavisMIB>
{
    public LaLoadTable LaLoadTable { get; set; } = new();
    
    public async Task<UCDavisMIB> Poll(ISNMPManager snmpManager, SNMPConnectionInfo connectionInfo)
    {
        return new UCDavisMIB
        {
            LaLoadTable = LaLoadTable.Deserializer.Deserialize(await snmpManager.BulkWalkAsync(connectionInfo, LaLoadTable.OID))
        };
    }
}