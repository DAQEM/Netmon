using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Disk.Metric;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.HostResources.Storage;

namespace Netmon.SNMPPolling.SNMP.Converter.Component;

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
            .Select(e => new Disk{
                Index = e.HrStorageIndex.ToInt32(),
                MountingPoint = e.HrStorageDescr.ToString(),
                Metrics = new List<IDiskMetric>
                {
                    new DiskMetric
                    {
                        AllocationUnits = e.HrStorageAllocationUnits.ToInt32(),
                        TotalSpace = e.HrStorageSize.ToInt32(),
                        UsedSpace = e.HrStorageUsed.ToInt32()
                    }
                }
                
            })
            .Cast<IDisk>()
            .ToList();
    }
}