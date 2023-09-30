namespace SNMPPollingService.Entities.Component.Memory;

public interface IMemory : IComponent
{
    public string Name { get; }
    public long AllocationUnits { get; }
    public long TotalSpace { get; }
    public long UsedSpace { get; }
}