using Netmon.Models.Device;

namespace Netmon.SNMPPolling.DTO;

public class DeviceDetailsDTO
{
    public string IpAddress { get; set; } = null!;
    public int Port { get; set; }
    public string Community { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Contact { get; set; } = null!;

    public DeviceDetailsDTO FromDevice(IDevice device)
    {
        return new()
        {
            IpAddress = device.IpAddress ?? "",
            Port = device.DeviceConnection?.Port ?? 0,
            Community = device.DeviceConnection?.Community ?? "",
            Name = device.Name ?? "",
            Location = device.Location ?? "",
            Contact = device.Contact ?? ""
        };
    }
}