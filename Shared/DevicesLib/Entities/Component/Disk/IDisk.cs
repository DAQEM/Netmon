namespace DevicesLib.Entities.Component.Disk;

public interface IDisk : IComponent
{
    public string MountingPoint { get; }
    public long AllocationUnits { get; }
    public long TotalSpace { get; }
    public long UsedSpace { get; }
}