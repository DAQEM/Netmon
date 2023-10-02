using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.If;
using SNMPPollingService.SNMP.MIB.If.Interface;
using SNMPPollingService.SNMP.MIB.If.InterfaceX;
using SNMPPollingService.SNMP.Poll.MIB.MIB;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.MIB;

public class IfMIBPoller : IMIBPoller<IfMIB>
{
    private readonly ISNMPManager snmpManager;
    
    public IfMIBPoller(ISNMPManager snmpManager)
    {
        this.snmpManager = snmpManager;
    }
    
    public async Task<IfMIB> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        return new IfMIB
        {
            IfTable = IfTable.Deserializer.Deserialize(await snmpManager.BulkWalkAsync(connectionInfo, IfTable.OID)),
            IfXTable = IfXTable.Deserializer.Deserialize(await snmpManager.BulkWalkAsync(connectionInfo, IfXTable.OID))
        };
    }
}