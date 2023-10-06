using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Device;

public class HrDeviceEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.2.1";
    
    public Integer32 HrDeviceIndex { get; set; }
    public ObjectIdentifier HrDeviceType { get; set; }
    public OctetString HrDeviceDescr { get; set; }
    public ObjectIdentifier HrDeviceID { get; set; }
    public Integer32? HrDeviceStatus { get; set; }
    public Counter32? HrDeviceErrors { get; set; }

    public static ISNMPDeserializer<HrDeviceEntry> Deserializer { get; } = new HrDeviceEntryDeserializer();
    
    public class HrDeviceEntryDeserializer : ISNMPDeserializer<HrDeviceEntry>
    {
        public HrDeviceEntry Deserialize(ISNMPResult isnmpResult)
        {
            Dictionary<string, Variable> variables = isnmpResult.Variables.ToDictionary(v => v.Id.ToString().Split(".").Reverse().Skip(1).First(), v => v);
            
            return new HrDeviceEntry
            {
                HrDeviceIndex = (Integer32) variables["1"].Data,
                HrDeviceType = (ObjectIdentifier) variables["2"].Data,
                HrDeviceDescr = (OctetString) variables["3"].Data,
                HrDeviceID = (ObjectIdentifier) variables["4"].Data,
                HrDeviceStatus = variables.ContainsKey("5") ? (Integer32?) variables["5"].Data : null,
                HrDeviceErrors = variables.ContainsKey("6") ? (Counter32?) variables["6"].Data : null
            };
        }
    }
}