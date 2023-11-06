using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Network;

public class HrNetworkEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.4.1";

    public Integer32 HrNetworkIfIndex { get; set; } = null!;
    
    public static ISNMPDeserializer<HrNetworkEntry> Deserializer { get; } = new HrNetworkEntryDeserializer();
    
    private class HrNetworkEntryDeserializer : ISNMPDeserializer<HrNetworkEntry>
    {
        public HrNetworkEntry Deserialize(ISNMPResult isnmpResult)
        {
            Dictionary<string, Variable> variables = isnmpResult.Variables.ToDictionary(v => v.Id.ToString().Split(".").Reverse().Skip(1).First(), v => v);
            
            return new HrNetworkEntry
            {
                HrNetworkIfIndex = (Integer32) variables["1"].Data
            };
        }
    }
}