using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.UCDavis.CpuLoad;

public class LaLoadEntry
{
    public static readonly string OID = "1.3.6.1.4.1.2021.10.1";

    public Integer32 LaLoadInt { get; set; }
    
    public static ISNMPDeserializer<LaLoadEntry> Deserializer { get; } = new LaLoadEntryDeserializer();
    
    private class LaLoadEntryDeserializer : ISNMPDeserializer<LaLoadEntry>
    {
        public LaLoadEntry Deserialize(ISNMPResult isnmpResult)
        {
            return new LaLoadEntry
            {
                LaLoadInt = (Integer32) isnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.5")).First().Data
            };
        }
    }
}