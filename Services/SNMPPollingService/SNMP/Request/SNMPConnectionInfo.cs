using System.ComponentModel;
using DevicesLib.Attribute.Validator;
using DevicesLib.Protocol;
using Lextm.SharpSnmpLib;

namespace SNMPPollingService.SNMP.Request;

public class SNMPConnectionInfo
{
    [DefaultValue("V3")]
    public VersionCode Version { get; set; } = VersionCode.V3;
    
    [DefaultValue("127.0.0.1")]
    [ValidateIpAddress] 
    public string IpAddress { get; set; } = "127.0.0.1";
    
    [DefaultValue(161)]
    public int Port { get; set; } = 161;
    
    [DefaultValue("public")]
    public string Community { get; set; } = "public";
    
    public string? AuthPassword { get; set; }
    public string? PrivacyPassword { get; set; }
    
    [DefaultValue("SHA512")]
    public AuthProtocol? AuthProtocol { get; set; }
    
    [DefaultValue("AES")]
    public PrivacyProtocol? PrivacyProtocol { get; set; }
    
    [DefaultValue("")]
    public string? ContextName { get; set; }
}