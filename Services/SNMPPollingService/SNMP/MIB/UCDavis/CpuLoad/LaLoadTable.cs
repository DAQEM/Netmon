using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;

public class LaLoadTable
{
        public static readonly string OID = "1.3.6.1.4.1.2021.10.1";

    public List<LaLoadEntry> LaLoadEntries { get; set; } = new();
    
    public static ISNMPDeserializer<LaLoadTable> Deserializer { get; } = new LaLoadTableDeserializer();
    
    private class LaLoadTableDeserializer : ISNMPDeserializer<LaLoadTable>
    {
        public LaLoadTable Deserialize(ISNMPResult iSnmpResult)
        {
            List<List<Variable>> table = iSnmpResult.GetEntries(LaLoadEntry.OID);
            
            return new LaLoadTable
            {
                LaLoadEntries = table.Select(list => LaLoadEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}