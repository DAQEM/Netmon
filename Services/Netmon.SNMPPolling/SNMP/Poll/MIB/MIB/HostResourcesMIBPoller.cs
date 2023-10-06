using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Device;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;

public class HostResourcesMIBPoller : IMIBPoller<HostResourcesMIB>
{
    private readonly ISNMPManager _snmpManager;
    
    public HostResourcesMIBPoller(ISNMPManager snmpManager)
    {
        _snmpManager = snmpManager;
    }
    
    public async Task<HostResourcesMIB> PollMIB(SNMPConnectionInfo connectionInfo)
    {
        ISNMPResult hrStorage = await _snmpManager.BulkWalkAsync(connectionInfo, HrStorage.OID);
        ISNMPResult hrDevice = await _snmpManager.BulkWalkAsync(connectionInfo, HrDevice.OID);
        
        return new HostResourcesMIB
        {
            HrStorage = HrStorage.Deserializer.Deserialize(hrStorage),
            HrDevice = HrDevice.Deserializer.Deserialize(hrDevice)
        };
    }
}