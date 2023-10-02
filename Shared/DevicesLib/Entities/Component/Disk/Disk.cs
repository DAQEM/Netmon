namespace DevicesLib.Entities.Component.Disk;

public class Disk : IDisk
{

    public Disk(int index, string mountingPoint, int allocationUnits, int totalSpace, int usedSpace)
    {
        Index = index;
        MountingPoint = mountingPoint;
        AllocationUnits = allocationUnits;
        TotalSpace = totalSpace;
        UsedSpace = usedSpace;
    }

    public int Index { get; }
    public string MountingPoint { get; }
    public int AllocationUnits { get; }
    public int TotalSpace { get; }
    public int UsedSpace { get; }
}