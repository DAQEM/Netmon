using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

public class HrStorageEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.2.3.1";
    
    public Integer32 HrStorageIndex { get; set; }
    public StorageType HrStorageType { get; set; }
    public OctetString HrStorageDescr { get; set; }
    public Integer32 HrStorageAllocationUnits { get; set; }
    public Integer32 HrStorageSize { get; set; }
    public Integer32 HrStorageUsed { get; set; }
    public Integer32? HrStorageAllocationFailures { get; set; }
    
    public static ISNMPDeserializer<HrStorageEntry> Deserializer { get; } = new HrProcessorEntryDeserializer();
    
    private class HrProcessorEntryDeserializer : ISNMPDeserializer<HrStorageEntry>
    {
        public HrStorageEntry Deserialize(ISNMPResult isnmpResult)
        {
            Dictionary<string, Variable> variables = isnmpResult.Variables.ToDictionary(v => v.Id.ToString().Split(".").Reverse().Skip(1).First(), v => v);
            
            return new HrStorageEntry
            {
                HrStorageIndex = (Integer32) variables["1"].Data,
                HrStorageType = (StorageType) int.Parse(((ObjectIdentifier) variables["2"].Data).ToString().Split(".").Last()),
                HrStorageDescr = (OctetString) variables["3"].Data,
                HrStorageAllocationUnits = (Integer32) variables["4"].Data,
                HrStorageSize = (Integer32) variables["5"].Data,
                HrStorageUsed = (Integer32) variables["6"].Data,
                HrStorageAllocationFailures = variables.TryGetValue("7", out Variable? variable) ? (Integer32?) variable.Data : null
            };
        }
    }
    
    public enum StorageType
    {
        Other = 1,
        Ram = 2,
        VirtualMemory = 3,
        FixedDisk = 4,
        RemovableDisk = 5,
        FloppyDisk = 6,
        CompactDisc = 7,
        RamDisk = 8,
        FlashMemory = 9,
        NetworkDisk = 10
    }
}