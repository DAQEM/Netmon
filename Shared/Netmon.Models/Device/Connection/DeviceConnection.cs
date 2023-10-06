using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.Models.Device.Connection;

public class DeviceConnection : IDeviceConnection
{
    public int Port { get; set; }
    
    public string Community { get; set; }
    
    public string AuthPassword { get; set; }
    
    public string PrivacyPassword { get; set; }
    
    public AuthProtocol AuthProtocol { get; set; }
    
    public PrivacyProtocol PrivacyProtocol { get; set; }
    
    public string ContextName { get; set; }
    
    public int SNMPVersion { get; set; }
}