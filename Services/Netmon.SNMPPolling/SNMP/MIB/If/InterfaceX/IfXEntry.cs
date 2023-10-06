using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;

public class IfXEntry
{
    public static readonly string OID = "1.3.6.1.2.1.31.1.1.1";

    public int IfIndex { get; set; }
    public Counter64 IfHCInOctets { get; set; }
    public Counter64 IfHCInUcastPkts { get; set; }
    public Counter64 IfHCInMulticastPkts { get; set; }
    public Counter64 IfHCInBroadcastPkts { get; set; }
    public Counter64 IfHCOutOctets { get; set; }
    public Counter64 IfHCOutUcastPkts { get; set; }
    public Counter64 IfHCOutMulticastPkts { get; set; }
    public Counter64 IfHCOutBroadcastPkts { get; set; }
    
    
    public static ISNMPDeserializer<IfXEntry> Deserializer { get; } = new IfXEntryDeserializer();
    
    private class IfXEntryDeserializer : ISNMPDeserializer<IfXEntry>
    {
        public IfXEntry Deserialize(ISNMPResult iSnmpResult)
        {
            return new IfXEntry
            {
                IfIndex = int.Parse(iSnmpResult.Variables.First().Id.ToString().Split('.').Last()),
                IfHCInOctets = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.6")).First().Data,
                IfHCInUcastPkts = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.7")).First().Data,
                IfHCInMulticastPkts = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.8")).First().Data,
                IfHCInBroadcastPkts = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.9")).First().Data,
                IfHCOutOctets = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.10")).First().Data,
                IfHCOutUcastPkts = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.11")).First().Data,
                IfHCOutMulticastPkts = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.12")).First().Data,
                IfHCOutBroadcastPkts = (Counter64) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.13")).First().Data
            };
        }
    }
}