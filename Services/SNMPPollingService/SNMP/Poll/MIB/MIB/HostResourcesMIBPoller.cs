using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.HostResources.Device;
using SNMPPollingService.SNMP.MIB.HostResources.Storage;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.Poll.MIB.MIB;

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