using System.Text.Json.Serialization;
using Netmon.Data.DBO.Device;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceConnectionDTO
{
    public int Port { get; set; }
    public string Community { get; set; } = null!;
    public int Version { get; set; }
    public string? AuthPassword { get; set; }
    public string? PrivacyPassword { get; set; }
    public AuthProtocol? AuthProtocol { get; set; }
    public PrivacyProtocol? PrivacyProtocol { get; set; }
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