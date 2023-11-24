using Lextm.SharpSnmpLib;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.SNMPPolling.DTO;

public class DeviceConnectionDTO
{
    public VersionCode SNMPVersion { get; set; }
    public int Port { get; set; }
    public string Community { get; set; } = null!;
    public string AuthPassword { get; set; } = null!;
    public string PrivacyPassword { get; set; } = null!;
    public AuthProtocol AuthProtocol { get; set; }
    public PrivacyProtocol PrivacyProtocol { get; set; }
    public string ContextName { get; set; } = null!;
}