using DevicesLib.Entities;

namespace SNMPPollingService.Entities.Component.Disk;

public class Disk : IDisk
{
    public Disk(DiskDTO disk)
    {
        MountingPoint = disk.MountingPoint;
        AllocationUnits = disk.AllocationUnits;
        TotalSpace = disk.TotalSpace;
        UsedSpace = disk.UsedSpace;
    }
    
    public Disk(string mountingPoint, long allocationUnits, long totalSpace, long usedSpace)
    {
        MountingPoint = mountingPoint;
        AllocationUnits = allocationUnits;
        TotalSpace = totalSpace;
        UsedSpace = usedSpace;
    }

    public string MountingPoint { get; private set; }
    public long AllocationUnits { get; private set; }
    public long TotalSpace { get; private set; }
    public long UsedSpace { get; private set; }
}