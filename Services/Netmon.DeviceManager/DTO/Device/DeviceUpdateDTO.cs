using System.Text.Json.Serialization;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Attribute.Validator;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceUpdateDTO
{
    [ValidateIpAddress]
    [JsonPropertyName("ip_address")]
    public string IpAddress { get; set; } = null!;
    [JsonPropertyName("connection")]
    public DeviceConnectionDTO Connection { get; set; } = null!;

    public DeviceDBO ToDeviceDBO(DeviceDBO deviceDBO)
    {
        return new DeviceDBO
        {
            Id = deviceDBO.Id,
            Name = deviceDBO.Name,
            IpAddress = IpAddress,
            Location = deviceDBO.Location,
            Contact = deviceDBO.Contact,
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
}