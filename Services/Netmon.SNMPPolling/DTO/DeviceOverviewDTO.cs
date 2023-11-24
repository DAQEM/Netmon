using Netmon.Models.Device;

namespace Netmon.SNMPPolling.DTO;

public class DeviceOverviewDTO
{
    public string? IpAddress { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public string? Location { get; set; } = null!;
    public string? Contact { get; set; } = null!;
    public DeviceConnectionDTO? DeviceConnection { get; set; } = null!;
    public List<DiskDTO>? Disks { get; set; } = null!;
    public List<CpuDTO>? Cpus { get; set; } = null!;
    public List<MemoryDTO>? Memory { get; set; } = null!;
    public List<InterfaceDTO>? Interfaces { get; set; } = null!;

    public static DeviceOverviewDTO FromDevice(IDevice device)
    {
        return new DeviceOverviewDTO
        {
            IpAddress = device.IpAddress,
            Name = device.Name,
            Location = device.Location,
            Contact = device.Contact,
            DeviceConnection = DeviceConnectionDTO.FromDeviceConnection(device.DeviceConnection),
            Disks = device.Disks?.Select(DiskDTO.FromDisk).ToList(),
            Cpus = device.Cpus?.Select(CpuDTO.FromCpu).ToList(),
            Memory = device.Memory?.Select(MemoryDTO.FromMemory).ToList(),
            Interfaces = device.Interfaces?.Select(InterfaceDTO.FromInterface).ToList()
        };
    }
}