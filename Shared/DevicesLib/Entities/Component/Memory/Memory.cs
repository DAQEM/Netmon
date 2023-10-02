namespace DevicesLib.Entities.Component.Memory;

public class Memory : IMemory
{
    public int Index { get; }
    public string Name { get; }
    public long AllocationUnits { get; }
    public long TotalSpace { get; }
    public long UsedSpace { get; }
    
    // public Memory(MemoryDTO memory)
    // {
    //     Name = memory.Name;
    //     AllocationUnits = memory.AllocationUnits;
    //     TotalSpace = memory.TotalSpace;
    //     UsedSpace = memory.UsedSpace;
    // }
    
    public Memory(int index, string name, long allocationUnits, long totalSpace, long usedSpace)
    {
        Index = index;
        Name = name;
        AllocationUnits = allocationUnits;
        TotalSpace = totalSpace;
        UsedSpace = usedSpace;
    }
}