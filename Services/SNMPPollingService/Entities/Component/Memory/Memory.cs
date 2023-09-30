namespace SNMPPollingService.Entities.Component.Memory;

public class Memory : IMemory
{
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
    
    public Memory(string name, long allocationUnits, long totalSpace, long usedSpace)
    {
        Name = name;
        AllocationUnits = allocationUnits;
        TotalSpace = totalSpace;
        UsedSpace = usedSpace;
    }
}