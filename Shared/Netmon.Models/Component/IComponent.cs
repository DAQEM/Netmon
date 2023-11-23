using System.Text.Json.Serialization;
using Netmon.Models.Json.Converter;

namespace Netmon.Models.Component;

[JsonConverter(typeof(ComponentConverter))]
public interface IComponent : IModel
{
    
}