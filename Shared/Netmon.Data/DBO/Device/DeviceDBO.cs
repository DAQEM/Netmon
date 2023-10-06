using System.ComponentModel.DataAnnotations;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Models.Device;

namespace Netmon.Data.DBO.Device;

public class DeviceDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string IpAddress { get; set; } = null!;
    
    public string? Location { get; set; }
    
    public string? Contact { get; set; }
    
    public DeviceConnectionDBO DeviceConnection { get; set; } = null!;
    
    public List<DiskDBO> Disks { get; set; } = null!;
    
    public List<CpuDBO> Cpus { get; set; } = null!;
    
    public List<MemoryDBO> Memory { get; set; } = null!;
    
    public List<InterfaceDBO> Interfaces { get; set; } = null!;
    
    public static DeviceDBO FromDevice(IDevice device)
    {
        return new DeviceDBO
        {
            Id = Guid.NewGuid(),
            Name = device.Name ?? string.Empty,
            IpAddress = device.IpAddress,
            Location = device.Location,
            Contact = device.Contact,
            DeviceConnection = DeviceConnectionDBO.FromDeviceConnection(device.DeviceConnection),
            Disks = device.Disks.Select(DiskDBO.FromDisk).ToList(),
            Cpus = device.Cpus.Select(CpuDBO.FromCpu).ToList(),
            Memory = device.Memory.Select(MemoryDBO.FromMemory).ToList(),
            Interfaces = device.Interfaces.Select(InterfaceDBO.FromInterface).ToList()
        };
    }
}