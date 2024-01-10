using System.Text.Json.Serialization;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? IpAddress { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }

    public static DeviceDTO FromDevice(Models.Device.Device device)
    {
        return new DeviceDTO
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Location = device.Location,
            Contact = device.Contact
        };
    }
}