using System.Text.Json;
using System.Text.Json.Serialization;
using DevicesLib.Entities.Component.Cpu;
using DevicesLib.Entities.Component.Disk;
using DevicesLib.Entities.Component.Interface;
using DevicesLib.Entities.Component.Memory;

namespace DevicesLib.Entities.Component;

public class ComponentConverter : JsonConverter<IComponent>
{
    public override IComponent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IComponent value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case IDisk disk:
                JsonSerializer.Serialize(writer, disk, options);
                break;
            case ICpu cpu:
                JsonSerializer.Serialize(writer, cpu, options);
                break;
            case IMemory memory:
                JsonSerializer.Serialize(writer, memory, options);
                break;
            case IInterface @interface:
                JsonSerializer.Serialize(writer, @interface, options);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}