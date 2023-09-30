using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.HostResources.Device.Network;

public class HrNetworkEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.4.1";

    public Integer32 HrNetworkIfIndex { get; set; }
    
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