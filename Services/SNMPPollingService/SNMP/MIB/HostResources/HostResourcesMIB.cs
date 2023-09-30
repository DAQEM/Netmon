using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.HostResources.Device;
using SNMPPollingService.SNMP.MIB.HostResources.Storage;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.HostResources;

public class HostResourcesMIB : IMIB<HostResourcesMIB>
{
    public static readonly string OID = "1.3.6.1.2.1.25";
    
    public HrStorage HrStorage { get; set; } = new();
    public HrDevice HrDevice { get; set; } = new();
    
    public async Task<HostResourcesMIB> Poll(ISNMPManager snmpManager, SNMPConnectionInfo connectionInfo)
    {
        ISNMPResult hrStorage = await snmpManager.BulkWalkAsync(connectionInfo, HrStorage.OID);
        ISNMPResult hrDevice = await snmpManager.BulkWalkAsync(connectionInfo, HrDevice.OID);
        
        return new HostResourcesMIB
        {
            HrStorage = HrStorage.Deserializer.Deserialize(hrStorage),
            HrDevice = HrDevice.Deserializer.Deserialize(hrDevice)
        };
    }
}