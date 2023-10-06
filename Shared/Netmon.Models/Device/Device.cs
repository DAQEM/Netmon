using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.Models.Device;

public class Device : IDevice
{
    public string IpAddress { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
    public IDeviceConnection? DeviceConnection { get; set; }
    public List<IDisk> Disks { get; set; }
    public List<ICpu> Cpus { get; set; }
    public List<IMemory> Memory { get; set; }
    public List<IInterface> Interfaces { get; set; }
}