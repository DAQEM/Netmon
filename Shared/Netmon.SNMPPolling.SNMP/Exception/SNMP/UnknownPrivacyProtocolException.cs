
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.SNMPPolling.SNMP.Exception.SNMP;

public class UnknownPrivacyProtocolException(string? privacyProtocol) : SNMPBaseException(
    $"Unknown privacy protocol: {privacyProtocol}. Supported protocols: {Enum.GetNames(typeof(PrivacyProtocol)).Aggregate((a, b) => $"{a}, {b}")}");