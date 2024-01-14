using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.SNMPPolling.SNMP.Exception.SNMP;

public class UnknownAuthProtocolException(string? authProtocol) : SNMPBaseException(
    $"Unknown auth protocol: {authProtocol}. Supported protocols: {Enum.GetNames(typeof(AuthProtocol)).Aggregate((a, b) => $"{a}, {b}")}");