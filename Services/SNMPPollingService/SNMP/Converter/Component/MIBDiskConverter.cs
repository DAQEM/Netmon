using SNMPPollingService.Entities.Component.Disk;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.HostResources.Storage;

namespace SNMPPollingService.SNMP.Converter.Component;

public class MIBDiskConverter : IMIBComponentConverter<IDisk>
{
    public List<IDisk> ConvertMIBsToComponent(List<IMIB> mibs)
    {
        HostResourcesMIB? hostResourcesMIB = mibs.OfType<HostResourcesMIB>().FirstOrDefault();
        if (hostResourcesMIB == null)
        {
            return new List<IDisk>();
        }
        
        return hostResourcesMIB.HrStorage.HrStorageTable.HrStorageEntries
            .Where(e => e.HrStorageType == HrStorageEntry.StorageType.FixedDisk)
            .Select(e => new Disk(
                e.HrStorageDescr.ToString(),
                e.HrStorageAllocationUnits.ToInt32(),
                e.HrStorageSize.ToInt32(),
                e.HrStorageUsed.ToInt32()
            ))
            .Cast<IDisk>()
            .ToList();
    }
}