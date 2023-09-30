using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.HostResources.Device.Device;

public class HrDeviceTable
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.2";

    public List<HrDeviceEntry> HrDeviceEntries { get; set; } = new();
    
    public static ISNMPDeserializer<HrDeviceTable> Deserializer { get; } = new HrDeviceTableDeserializer();
    
    public class HrDeviceTableDeserializer : ISNMPDeserializer<HrDeviceTable>
    {
        public HrDeviceTable Deserialize(ISNMPResult isnmpResult)
        {
            List<List<Variable>> table = isnmpResult.GetEntries(HrDeviceEntry.OID);
            
            return new HrDeviceTable
            {
                HrDeviceEntries = table.Select(list => HrDeviceEntry.Deserializer.Deserialize(new SNMPResult(list))).ToList()
            };
        }
    }
}