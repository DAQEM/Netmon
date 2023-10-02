using DevicesLib.DBO.Component.Disk;

namespace DevicesLib.Entities.Component.Disk;

public interface IDisk : IComponent
{
    public int Index { get; }
    public string MountingPoint { get; }
    public int AllocationUnits { get; }
    public int TotalSpace { get; }
    public int UsedSpace { get; }
    
    public DiskDBO ToDBO();
}