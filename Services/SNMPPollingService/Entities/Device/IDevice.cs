using SNMPPollingService.Entities.Component;

namespace SNMPPollingService.Entities.Device;

public interface IDevice : IEntity
{
    public string IpAddress { get; }
    public int Port { get; }
    public string Community { get; }
    
    public string? Name { get; }
    public string? Location { get; }
    public string? Contact { get; }
    
    public Dictionary<EntityType, List<IComponent>> Components { get; }
}