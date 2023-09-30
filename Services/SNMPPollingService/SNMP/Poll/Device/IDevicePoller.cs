using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.Device;

public interface IDevicePoller
{
    public Task<IDevice> PollFull(SNMPConnectionInfo connectionInfo);
    
    public Task<IDevice> PollDetails(SNMPConnectionInfo connectionInfo);
}