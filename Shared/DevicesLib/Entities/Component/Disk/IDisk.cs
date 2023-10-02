namespace DevicesLib.Entities.Component.Disk;

public interface IDisk : IComponent
{
    public string MountingPoint { get; }
    public int AllocationUnits { get; }
    public int TotalSpace { get; }
    public int UsedSpace { get; }
}