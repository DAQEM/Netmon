using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.Models.Device.Connection;

namespace Netmon.Models.Device;

public class Device : IDevice
{
    public string IpAddress { get; set; } = null!;
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
    public IDeviceConnection? DeviceConnection { get; set; }
    public List<IDisk> Disks { get; set; } = new();
    public List<ICpu> Cpus { get; set; } = new();
    public List<IMemory> Memory { get; set; } = new();
    public List<IInterface> Interfaces { get; set; } = new();
}