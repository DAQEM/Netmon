using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.If.InterfaceX;

public class IfXTable
{
    public static readonly string OID = "1.3.6.1.2.1.31.1.1";
    
    public List<IfXEntry> IfXEntries { get; set; } = new();
    
    public static ISNMPDeserializer<IfXTable> Deserializer { get; } = new IfXTableDeserializer();
    
    private class IfXTableDeserializer : ISNMPDeserializer<IfXTable>
    {
        public IfXTable Deserialize(ISNMPResult iSnmpResult)
        {
            List<List<Variable>> table = iSnmpResult.GetEntries(IfXEntry.OID);
            
            return new IfXTable
            {
                IfXEntries = table.Select(list => IfXEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}