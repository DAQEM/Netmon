using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.Models.Device.Connection;

public class DeviceConnection : IDeviceConnection
{
    public int Port { get; set; } = -1;
    
    public string Community { get; set; } = null!;
    
    public string AuthPassword { get; set; } = null!;
    
    public string PrivacyPassword { get; set; } = null!;
    
    public AuthProtocol AuthProtocol { get; set; }
    
    public PrivacyProtocol PrivacyProtocol { get; set; }
    
    public string ContextName { get; set; } = null!;
    
    public int SNMPVersion { get; set; } = -1;
}