using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Network;

public class HrNetworkTable
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.4";

    public List<HrNetworkEntry> HrNetworkEntries { get; set; } = new();
    
    public static ISNMPDeserializer<HrNetworkTable> Deserializer { get; } = new HrNetworkTableDeserializer();
    
    private class HrNetworkTableDeserializer : ISNMPDeserializer<HrNetworkTable>
    {
        public HrNetworkTable Deserialize(ISNMPResult isnmpResult)
        {
            List<List<Variable>> table = isnmpResult.GetEntries(HrNetworkEntry.OID);
            
            return new HrNetworkTable
            {
                HrNetworkEntries = table.Select(list => HrNetworkEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}