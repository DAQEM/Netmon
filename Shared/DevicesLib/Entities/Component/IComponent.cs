using System.Text.Json.Serialization;

namespace DevicesLib.Entities.Component;

[JsonConverter(typeof(ComponentConverter))]
public interface IComponent : IEntity
{
    
}