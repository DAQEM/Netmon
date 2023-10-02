using DevicesLib.Entities.Component.Memory;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.HostResources.Storage;

namespace SNMPPollingService.SNMP.Converter.Component;

public class MIBMemoryConverter : IMIBComponentConverter<IMemory>
{
    public List<IMemory> ConvertMIBsToComponent(List<IMIB> mibs)
    {
        HostResourcesMIB? hostResourcesMIB = mibs.OfType<HostResourcesMIB>().FirstOrDefault();
        
        if (hostResourcesMIB == null)
        {
            return new List<IMemory>();
        }
        
        return hostResourcesMIB.HrStorage.HrStorageTable.HrStorageEntries
            .Where(e => e.HrStorageType == HrStorageEntry.StorageType.Ram)
            .Select(e => new Memory(
                e.HrStorageIndex.ToInt32(),
                e.HrStorageDescr.ToString(),
                e.HrStorageAllocationUnits.ToInt32(),
                e.HrStorageSize.ToInt32(),
                e.HrStorageUsed.ToInt32()
            ))
            .Cast<IMemory>()
            .ToList();
    }
}