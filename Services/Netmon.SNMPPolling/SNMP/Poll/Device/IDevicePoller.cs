using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.Models.Device;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.SNMP.Poll.Device;

public interface IDevicePoller
{
    public Task<IDevice?> PollFull(SNMPConnectionInfo connectionInfo);
    
    public Task<IDevice?> PollDetails(SNMPConnectionInfo connectionInfo);
    
    public Task<List<IDisk>?> PollDisks(SNMPConnectionInfo connectionInfo);
    
    public Task<List<IMemory>?> PollMemory(SNMPConnectionInfo connectionInfo);
    
    public Task<List<ICpu>?> PollCpus(SNMPConnectionInfo connectionInfo);
    
    public Task<List<IInterface>?> PollInterfaces(SNMPConnectionInfo connectionInfo);
}