using System.Text.Json.Serialization;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }
    [JsonPropertyName("location")]
    public string? Location { get; set; }
    [JsonPropertyName("contact")]
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