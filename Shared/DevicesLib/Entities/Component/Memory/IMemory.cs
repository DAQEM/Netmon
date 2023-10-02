using DevicesLib.DBO.Component.Memory;

namespace DevicesLib.Entities.Component.Memory;

public interface IMemory : IComponent
{
    public int Index { get; }
    public string Name { get; }
    public int AllocationUnits { get; }
    public int TotalSpace { get; }
    public int UsedSpace { get; }
    
    public MemoryDBO ToDBO();
}