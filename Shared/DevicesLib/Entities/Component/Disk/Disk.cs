namespace DevicesLib.Entities.Component.Disk;

public class Disk : IDisk
{
    
    public Disk(string mountingPoint, int allocationUnits, int totalSpace, int usedSpace)
    {
        MountingPoint = mountingPoint;
        AllocationUnits = allocationUnits;
        TotalSpace = totalSpace;
        UsedSpace = usedSpace;
    }

    public string MountingPoint { get; }
    public int AllocationUnits { get; }
    public int TotalSpace { get; }
    public int UsedSpace { get; }
}