using System.Text.Json.Serialization;

namespace SNMPPollingService.Entities.Component;

[JsonConverter(typeof(ComponentConverter))]
public interface IComponent : IEntity
{
    
}