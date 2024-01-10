using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.If;
using Netmon.SNMPPolling.SNMP.MIB.If.Interface;
using Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

public class IfMIBPoller : IMIBPoller<IfMIB>
{
    private readonly ISNMPManager snmpManager;
    
    public IfMIBPoller(ISNMPManager snmpManager)
    {
        this.snmpManager = snmpManager;
    }
    
    public async Task<IfMIB?> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        Task<ISNMPResult> ifTableTask = snmpManager.BulkWalkAsync(connectionInfo, IfTable.OID, 10000);
        Task<ISNMPResult> ifXTableTask = snmpManager.BulkWalkAsync(connectionInfo, IfXTable.OID, 10000);

        await Task.WhenAll(ifTableTask, ifXTableTask);
        
        ISNMPResult ifTableResult = ifTableTask.Result;
        ISNMPResult ifXTableResult = ifXTableTask.Result;
        
        if (!ifTableResult.Variables.Any() && !ifXTableResult.Variables.Any()) return null;
        
        return new IfMIB
        {
            IfTable = IfTable.Deserializer.Deserialize(ifTableResult),
            IfXTable = IfXTable.Deserializer.Deserialize(ifXTableResult)
        };
    }
}