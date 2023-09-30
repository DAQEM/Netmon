using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.If.Interface;

public class IfTable
{
    public static readonly string OID = "1.3.6.1.2.1.2.2";
    
    public List<IfEntry> IfEntries { get; set; } = new();
    
    public static ISNMPDeserializer<IfTable> Deserializer { get; } = new IfTableDeserializer();
    
    private class IfTableDeserializer : ISNMPDeserializer<IfTable>
    {
        public IfTable Deserialize(ISNMPResult iSnmpResult)
        {
            List<List<Variable>> table = iSnmpResult.GetEntries(IfEntry.OID);
            
            return new IfTable
            {
                IfEntries = table.Select(list => IfEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}