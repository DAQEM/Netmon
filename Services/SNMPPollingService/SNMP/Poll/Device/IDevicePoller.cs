using DevicesLib.Entities.Component.Cpu;
using DevicesLib.Entities.Component.Disk;
using DevicesLib.Entities.Component.Interface;
using DevicesLib.Entities.Component.Memory;
using DevicesLib.Entities.Device;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.SNMP.Poll.Device;

public interface IDevicePoller
{
    public Task<IDevice> PollFull(SNMPConnectionInfo connectionInfo);
    
    public Task<IDevice> PollDetails(SNMPConnectionInfo connectionInfo);
    
    public Task<List<IDisk>> PollDisks(SNMPConnectionInfo connectionInfo);
    
    public Task<List<IMemory>> PollMemory(SNMPConnectionInfo connectionInfo);
    
    public Task<List<ICpu>> PollCpus(SNMPConnectionInfo connectionInfo);
    
    public Task<List<IInterface>> PollInterfaces(SNMPConnectionInfo connectionInfo);
}