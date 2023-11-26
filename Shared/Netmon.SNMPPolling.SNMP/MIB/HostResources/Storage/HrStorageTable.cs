using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

public class HrStorageTable
{
    public static readonly string OID = "1.3.6.1.2.1.25.2.3";
    
    public List<HrStorageEntry> HrStorageEntries { get; set; } = new();
    
    public static ISNMPDeserializer<HrStorageTable> Deserializer { get; } = new HrProcessorTableDeserializer();
    
    private class HrProcessorTableDeserializer : ISNMPDeserializer<HrStorageTable>
    {
        public HrStorageTable Deserialize(ISNMPResult isnmpResult)
        {
            List<List<Variable>> table = isnmpResult.GetEntries(HrStorageEntry.OID);
            
            return new HrStorageTable
            {
                HrStorageEntries = table.Select(list => HrStorageEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}