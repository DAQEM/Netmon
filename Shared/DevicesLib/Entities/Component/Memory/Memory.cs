using DevicesLib.DBO.Component.Memory;

namespace DevicesLib.Entities.Component.Memory;

public class Memory : IMemory
{
    public int Index { get; }
    public string Name { get; }
    public int AllocationUnits { get; }
    public int TotalSpace { get; }
    public int UsedSpace { get; }

    public Memory(int index, string name, int allocationUnits, int totalSpace, int usedSpace)
    {
        Index = index;
        Name = name;
        AllocationUnits = allocationUnits;
        TotalSpace = totalSpace;
        UsedSpace = usedSpace;
    }

    public MemoryDBO ToDBO()
    {
        return new MemoryDBO
        {
            Index = Index,
            Name = Name,
            MemoryMetrics = new List<MemoryMetricsDBO>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    AllocationUnits = AllocationUnits,
                    TotalSpace = TotalSpace,
                    UsedSpace = UsedSpace
                }
            }
        };
    }
}