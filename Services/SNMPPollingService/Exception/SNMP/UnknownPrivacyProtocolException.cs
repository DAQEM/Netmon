using DevicesLib.Protocol;

namespace SNMPPollingService.Exception.SNMP;

public class UnknownPrivacyProtocolException : SNMPBaseException
{
    public UnknownPrivacyProtocolException(string? privacyProtocol) 
        : base($"Unknown privacy protocol: {privacyProtocol}. Supported protocols: {Enum.GetNames(typeof(PrivacyProtocol)).Aggregate((a, b) => $"{a}, {b}")}")
    {
    }
}