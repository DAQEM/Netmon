using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.Deserializer;

public interface ISNMPDeserializer<out T> where T : class
{
    public T Deserialize(ISNMPResult iSnmpResult);
}