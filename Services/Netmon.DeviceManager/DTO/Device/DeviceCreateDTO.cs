using System.Text.Json.Serialization;
using Netmon.Models.Device;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceCreateDTO
{
    [JsonPropertyName("connection")]
    public DeviceConnectionDTO Connection { get; set; } = null!;

    public IDevice ToDevice()
    {
        return new Models.Device.Device
        {
            IpAddress = Connection.IpAddress,
            DeviceConnection = new DeviceConnection
            {
                Port = Connection.Port,
                Community = Connection.Community,
                SNMPVersion = Connection.Version,
                AuthPassword = Connection.AuthPassword ?? string.Empty,
                PrivacyPassword = Connection.PrivacyPassword ?? string.Empty,
                AuthProtocol = Connection.AuthProtocol ?? AuthProtocol.SHA256,
                PrivacyProtocol = Connection.PrivacyProtocol ?? PrivacyProtocol.AES,
                ContextName = Connection.Context ?? string.Empty
            }
        };
    }
}