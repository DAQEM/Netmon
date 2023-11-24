namespace Netmon.SNMPPolling.DTO;

public class DeviceOverviewDTO
{
    public string IpAddress { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Contact { get; set; } = null!;
    public DeviceConnectionDTO DeviceConnection { get; set; } = null!;
    public List<DiskDTO> Disks { get; set; } = null!;
    public List<CpuDTO> Cpus { get; set; } = null!;
    public List<MemoryDTO> Memory { get; set; } = null!;
    public List<InterfaceDTO> Interfaces { get; set; } = null!;
}