using System.ComponentModel.DataAnnotations;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Models.Device;

namespace Netmon.Data.DBO.Device;

public class DeviceDBO : IDBO
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
            Id = device.Id,
            Name = device.Name ?? string.Empty,
            IpAddress = device.IpAddress,
            Location = device.Location,
            Contact = device.Contact,
            DeviceConnection = DeviceConnectionDBO.FromDeviceConnection(device.DeviceConnection),
            Disks = device.Disks?.Select(DiskDBO.FromDisk).ToList() ?? new List<DiskDBO>(),
            Cpus = device.Cpus?.Select(CpuDBO.FromCpu).ToList() ?? new List<CpuDBO>(),
            Memory = device.Memory?.Select(MemoryDBO.FromMemory).ToList() ?? new List<MemoryDBO>(),
            Interfaces = device.Interfaces?.Select(InterfaceDBO.FromInterface).ToList() ?? new List<InterfaceDBO>()
        };
    }

    public IDevice ToDevice()
    {
        return new Models.Device.Device
        {
            Id = Id,
            Name = Name,
            IpAddress = IpAddress,
            Location = Location,
            Contact = Contact,
            DeviceConnection = DeviceConnection?.ToDeviceConnection(),
            Disks = Disks?.Select(disk => disk.ToDisk()).ToList(),
            Cpus = Cpus?.Select(cpu => cpu.ToCpu()).ToList(),
            Memory = Memory?.Select(memory => memory.ToMemory()).ToList(),
            Interfaces = Interfaces?.Select(@interface => @interface.ToInterface()).ToList()
        };
    }
}