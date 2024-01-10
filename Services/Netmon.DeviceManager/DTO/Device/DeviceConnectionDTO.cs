using System.Text.Json.Serialization;
using Netmon.Data.DBO.Device;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceConnectionDTO
{
    [JsonPropertyName("port")]
    public int Port { get; set; }
    [JsonPropertyName("community")]
    public string Community { get; set; } = null!;
    [JsonPropertyName("version")]
    public int Version { get; set; }
    [JsonPropertyName("auth_password")]
    public string? AuthPassword { get; set; }
    [JsonPropertyName("privacy_password")]
    public string? PrivacyPassword { get; set; }
    [JsonPropertyName("auth_protocol")]
    public AuthProtocol? AuthProtocol { get; set; }
    [JsonPropertyName("privacy_protocol")]
    public PrivacyProtocol? PrivacyProtocol { get; set; }
    [JsonPropertyName("context_name")]
    public string? ContextName { get; set; }

    public static DeviceConnectionDTO FromDeviceConnectionDBO(DeviceConnectionDBO deviceConnection)
    {
        return new DeviceConnectionDTO
        {
            Port = deviceConnection.Port,
            Community = deviceConnection.Community,
            Version = deviceConnection.SNMPVersion,
            AuthPassword = deviceConnection.AuthPassword,
            PrivacyPassword = deviceConnection.PrivacyPassword,
            AuthProtocol = deviceConnection.AuthProtocol,
            PrivacyProtocol = deviceConnection.PrivacyProtocol,
            ContextName = deviceConnection.ContextName
        };
    }
    
    public static DeviceConnectionDTO FromDeviceConnection(IDeviceConnection? deviceConnection)
    {
        if (deviceConnection == null) return new DeviceConnectionDTO();
        return new DeviceConnectionDTO
        {
            Port = deviceConnection.Port,
            Community = deviceConnection.Community,
            Version = deviceConnection.SNMPVersion,
            AuthPassword = deviceConnection.AuthPassword,
            PrivacyPassword = deviceConnection.PrivacyPassword,
            AuthProtocol = deviceConnection.AuthProtocol,
            PrivacyProtocol = deviceConnection.PrivacyProtocol,
            ContextName = deviceConnection.ContextName
        };
    }
}