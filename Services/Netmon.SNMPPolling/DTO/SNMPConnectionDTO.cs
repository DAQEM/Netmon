using System.Text.Json.Serialization;
using Lextm.SharpSnmpLib;
using Netmon.Models.Device.Connection.Protocol;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.DTO;

public class SNMPConnectionDTO
{
    public string IpAddress { get; set; } = null!;
    [JsonPropertyName("version")]
    public VersionCode SNMPVersion { get; set; }
    public int Port { get; set; }
    public string Community { get; set; } = null!;
    public string? AuthPassword { get; set; } = null!;
    public string? PrivacyPassword { get; set; } = null!;
    public AuthProtocol? AuthProtocol { get; set; }
    public PrivacyProtocol? PrivacyProtocol { get; set; }
    public string? ContextName { get; set; } = null!;
    
    public SNMPConnectionInfo ToSNMPConnectionInfo()
    {
        return new SNMPConnectionInfo
        {
            IpAddress = IpAddress,
            Version = SNMPVersion,
            Port = Port,
            Community = Community,
            AuthPassword = AuthPassword,
            PrivacyPassword = PrivacyPassword,
            AuthProtocol = AuthProtocol,
            PrivacyProtocol = PrivacyProtocol,
            ContextName = ContextName
        };
    }
}