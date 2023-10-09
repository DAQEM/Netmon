using System.Text.Json.Serialization;
using Netmon.Data.EntityFramework.Attribute.Validator;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceConnectionDTO
{
    [ValidateIpAddress]
    [JsonPropertyName("ip_address")]
    public string IpAddress { get; set; } = null!;
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
    [JsonPropertyName("context")]
    public string? Context { get; set; }
}