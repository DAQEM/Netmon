using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

public class HrStorage
{
    public static readonly string OID = "1.3.6.1.2.1.25.2";

    public Integer32 HrMemorySize { get; set; }
    public HrStorageTable HrStorageTable { get; set; } = new();

    public static ISNMPDeserializer<HrStorage> Deserializer { get; } = new HrDeviceDeserializer();

    private class HrDeviceDeserializer : ISNMPDeserializer<HrStorage>
    {
        public HrStorage Deserialize(ISNMPResult isnmpResult)
        {
            return new HrStorage
            {
                HrMemorySize = (Integer32) isnmpResult.GetTable(OID)[0].Data,
                HrStorageTable = HrStorageTable.Deserializer.Deserialize(new SNMPResult(isnmpResult.GetTable(HrStorageTable.OID))),
            };
        }
    }
}