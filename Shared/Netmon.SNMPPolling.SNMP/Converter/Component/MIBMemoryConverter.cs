using Netmon.Models.Component.Memory;
using Netmon.Models.Component.Memory.Metric;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

namespace Netmon.SNMPPolling.SNMP.Converter.Component;

public class MIBMemoryConverter : IMIBComponentConverter<IMemory>
{
    public List<IMemory> ConvertMIBsToComponent(List<IMIB> mibs)
    {
        HostResourcesMIB? hostResourcesMIB = mibs.OfType<HostResourcesMIB>().FirstOrDefault();
        
        if (hostResourcesMIB is null)
        {
            return new List<IMemory>();
        }
        
        return hostResourcesMIB.HrStorage.HrStorageTable.HrStorageEntries
            .Where(e => e.HrStorageType == HrStorageEntry.StorageType.Ram)
            .Select(e => new Memory{
                Index = e.HrStorageIndex.ToInt32(),
                Name = e.HrStorageDescr.ToString(),
                Metrics = new List<IMemoryMetric>
                {
                    new MemoryMetric
                    {
                        AllocationUnits = e.HrStorageAllocationUnits.ToInt32(),
                        TotalSpace = e.HrStorageSize.ToInt32(),
                        UsedSpace = e.HrStorageUsed.ToInt32()
                    }
                }
                
            })
            .Cast<IMemory>()
            .ToList();
    }
}