using SNMPPollingService.SNMP.Protocol;

namespace SNMPPollingService.Exception.SNMP;

public class UnknownAuthProtocolException : SNMPBaseException
{
    public UnknownAuthProtocolException(string? authProtocol) 
        : base($"Unknown auth protocol: {authProtocol}. Supported protocols: {Enum.GetNames(typeof(AuthProtocol)).Aggregate((a, b) => $"{a}, {b}")}")
    {
    }
}