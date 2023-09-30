using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.If.Interface;
using SNMPPollingService.SNMP.MIB.If.InterfaceX;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.MIB.If;

public class IfMIB : IMIB<IfMIB>
{
    public IfTable IfTable { get; set; } = new();
    public IfXTable IfXTable { get; set; } = new();
    
    public async Task<IfMIB> Poll(ISNMPManager snmpManager, SNMPConnectionInfo connectionInfo)
    {
        return new IfMIB
        {
            IfTable = IfTable.Deserializer.Deserialize(await snmpManager.BulkWalkAsync(connectionInfo, IfTable.OID)),
            IfXTable = IfXTable.Deserializer.Deserialize(await snmpManager.BulkWalkAsync(connectionInfo, IfXTable.OID))
        };
    }
}