using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.If;
using Netmon.SNMPPolling.SNMP.MIB.If.Interface;
using Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;
using Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB;

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