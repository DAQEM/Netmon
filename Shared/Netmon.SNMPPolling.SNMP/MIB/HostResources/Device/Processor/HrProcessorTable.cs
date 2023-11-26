using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Processor;

public class HrProcessorTable
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.3";

    public List<HrProcessorEntry> HrProcessorEntries { get; set; }  = null!;
    
    public static ISNMPDeserializer<HrProcessorTable> Deserializer { get; } = new HrProcessorTableDeserializer();
    
    private class HrProcessorTableDeserializer : ISNMPDeserializer<HrProcessorTable>
    {
        public HrProcessorTable Deserialize(ISNMPResult isnmpResult)
        {
            List<List<Variable>> table = isnmpResult.GetEntries(HrProcessorEntry.OID);
            
            return new HrProcessorTable
            {
                HrProcessorEntries = table.Select(list => HrProcessorEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}