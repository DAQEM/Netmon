using System.ComponentModel.DataAnnotations;
using DevicesLib.DBO.Component.Cpu;
using DevicesLib.DBO.Component.Disk;
using DevicesLib.DBO.Component.Interface;
using DevicesLib.DBO.Component.Memory;

namespace DevicesLib.DBO.Device;

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
}