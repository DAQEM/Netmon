using Lextm.SharpSnmpLib;
using Netmon.Models.Component.Interface;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.If.Interface;

public class IfEntry
{
    public static readonly string OID = "1.3.6.1.2.1.2.2.1";
    
    public Integer32 IfIndex { get; set; } = null!;
    public OctetString IfDescr { get; set; } = null!;
    public InterfaceType IfType { get; set; }
    public InterfaceStatus IfAdminStatus { get; set; }
    public InterfaceStatus IfOperationalStatus { get; set; }
    public Integer32 IfMtu { get; set; } = null!;
    public Gauge32 IfSpeed { get; set; } = null!;
    public OctetString IfPhysAddress { get; set; } = null!;
    public Counter32 IfInErrors { get; set; } = null!;
    public Counter32 IfOutErrors { get; set; } = null!;
    public Counter32 IfInDiscards { get; set; } = null!;
    public Counter32 IfOutDiscards { get; set; } = null!;
    
    public static ISNMPDeserializer<IfEntry> Deserializer { get; } = new IfEntryDeserializer();
    
    private class IfEntryDeserializer : ISNMPDeserializer<IfEntry>
    {
        public IfEntry Deserialize(ISNMPResult iSnmpResult)
        {
            return new IfEntry
            {
                IfIndex = (Integer32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.1")).First().Data,
                IfDescr = (OctetString) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.2")).First().Data,
                IfType = (InterfaceType) ((Integer32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.3")).First().Data).ToInt32(),
                IfMtu = (Integer32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.4")).First().Data,
                IfSpeed = (Gauge32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.5")).First().Data,
                IfPhysAddress = (OctetString) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.6")).First().Data,
                IfAdminStatus = (InterfaceStatus) ((Integer32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.7")).First().Data).ToInt32(),
                IfOperationalStatus = (InterfaceStatus) ((Integer32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.8")).First().Data).ToInt32(),
                IfInDiscards = (Counter32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.13")).First().Data,
                IfInErrors = (Counter32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.14")).First().Data,
                IfOutDiscards = (Counter32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.19")).First().Data,
                IfOutErrors = (Counter32) iSnmpResult.Variables.Where(v => v.Id.ToString().Contains($"{OID}.20")).First().Data
            };
        }
    }
}