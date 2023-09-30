using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.Deserializer;

public interface ISNMPDeserializer<out T> where T : class
{
    public T Deserialize(ISNMPResult iSnmpResult);
}