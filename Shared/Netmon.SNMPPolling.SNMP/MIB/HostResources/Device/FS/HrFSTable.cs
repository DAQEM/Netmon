using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.FS;

public class HrFSTable
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.8";

    public List<HrFSEntry> HrFSEntries { get; set; } = new();
    
    public static ISNMPDeserializer<HrFSTable> Deserializer { get; } = new HrFSTableDeserializer();
    
    private class HrFSTableDeserializer : ISNMPDeserializer<HrFSTable>
    {
        public HrFSTable Deserialize(ISNMPResult isnmpResult)
        {
            List<List<Variable>> table = isnmpResult.GetEntries(HrFSEntry.OID);
            
            return new HrFSTable
            {
                HrFSEntries = table.Select(list => HrFSEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}