using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.SNMPPolling.DTO;

public class DeviceConnectionDTO
{
    public int Port { get; set; }
    public string Community { get; set; } = null!;
    public string? AuthPassword { get; set; } = null!;
    public string? PrivacyPassword { get; set; } = null!;
    public AuthProtocol? AuthProtocol { get; set; }
    public PrivacyProtocol? PrivacyProtocol { get; set; }
    public string? ContextName { get; set; } = null!;
    public int SNMPVersion { get; set; }

    public static DeviceConnectionDTO FromDeviceConnection(IDeviceConnection? deviceDeviceConnection)
    {
        if (deviceDeviceConnection is null) return new DeviceConnectionDTO();
        return new DeviceConnectionDTO
        {
            SNMPVersion = deviceDeviceConnection.SNMPVersion,
            Port = deviceDeviceConnection.Port,
            Community = deviceDeviceConnection.Community,
            AuthPassword = deviceDeviceConnection.AuthPassword,
            PrivacyPassword = deviceDeviceConnection.PrivacyPassword,
            AuthProtocol = deviceDeviceConnection.AuthProtocol,
            PrivacyProtocol = deviceDeviceConnection.PrivacyProtocol,
            ContextName = deviceDeviceConnection.ContextName
        };
    }
}