using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

public class HrStorage
{
    public static readonly string OID = "1.3.6.1.2.1.25.2";

    public Integer32 HrMemorySize { get; set; }  = null!;
    public HrStorageTable HrStorageTable { get; set; } = new();

    public static ISNMPDeserializer<HrStorage> Deserializer { get; } = new HrStorageDeserializer();

    private class HrStorageDeserializer : ISNMPDeserializer<HrStorage>
    {
        public HrStorage Deserialize(ISNMPResult isnmpResult)
        {
            List<Variable> variables = isnmpResult.GetTable(OID);

            return new HrStorage
            {
                HrMemorySize = variables.Count > 0 ? (Integer32) variables[0].Data : new Integer32(0),
                HrStorageTable = HrStorageTable.Deserializer.Deserialize(new SNMPResult(isnmpResult.GetTable(HrStorageTable.OID))),
            };
        }
    }
}