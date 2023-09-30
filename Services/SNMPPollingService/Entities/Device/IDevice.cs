using SNMPPollingService.Entities.Component;
using SNMPPollingService.Entities.Component.Cpu;
using SNMPPollingService.Entities.Component.Disk;
using SNMPPollingService.Entities.Component.Interface;
using SNMPPollingService.Entities.Component.Memory;

namespace SNMPPollingService.Entities.Device;

public interface IDevice : IEntity
{
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public string Community { get; set; }
    
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
    
    public List<IDisk> Disks { get; set; }
    public List<ICpu> Cpus { get; set; }
    public List<IMemory> Memory { get; set; }
    public List<IInterface> Interfaces { get; set; }
}