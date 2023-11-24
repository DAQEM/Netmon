using System.Text.Json.Serialization;
using Netmon.Data.EntityFramework.Attribute.Validator;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceCreateDTO
{
    [ValidateIpAddress]
    [JsonPropertyName("ip_address")]
    public string IpAddress { get; set; } = null!;
    [JsonPropertyName("connection")]
    public DeviceConnectionDTO Connection { get; set; } = null!;

    public DeviceDBO ToDeviceDBO()
    {
        return new DeviceDBO
        {
            Name = "Unknown",
            IpAddress = IpAddress,
            DeviceConnection = new DeviceConnectionDBO
            {
                Port = Connection.Port,
                Community = Connection.Community,
                SNMPVersion = Connection.Version,
                AuthPassword = Connection.AuthPassword ?? string.Empty,
                PrivacyPassword = Connection.PrivacyPassword ?? string.Empty,
                AuthProtocol = Connection.AuthProtocol ?? AuthProtocol.SHA256,
                PrivacyProtocol = Connection.PrivacyProtocol ?? PrivacyProtocol.AES,
                ContextName = Connection.ContextName ?? string.Empty
            }
        };
    }
    
    public Models.Device.Device ToDevice()
    {
        return new Models.Device.Device
        {
            Name = "Unknown",
            IpAddress = IpAddress,
            DeviceConnection = new DeviceConnection
            {
                Port = Connection.Port,
                Community = Connection.Community,
                SNMPVersion = Connection.Version,
                AuthPassword = Connection.AuthPassword ?? string.Empty,
                PrivacyPassword = Connection.PrivacyPassword ?? string.Empty,
                AuthProtocol = Connection.AuthProtocol ?? AuthProtocol.SHA256,
                PrivacyProtocol = Connection.PrivacyProtocol ?? PrivacyProtocol.AES,
                ContextName = Connection.ContextName ?? string.Empty
            }
        };
    }
}