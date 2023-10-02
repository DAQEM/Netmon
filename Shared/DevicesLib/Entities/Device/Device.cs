using DevicesLib.Entities.Component.Cpu;
using DevicesLib.Entities.Component.Disk;
using DevicesLib.Entities.Component.Interface;
using DevicesLib.Entities.Component.Memory;

namespace DevicesLib.Entities.Device;

public class Device : IDevice
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