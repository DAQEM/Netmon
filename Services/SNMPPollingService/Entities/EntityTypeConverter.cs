using System.Text.Json;
using System.Text.Json.Serialization;

namespace SNMPPollingService.Entities;

public class EntityTypeConverter : JsonConverter<EntityType>
{
    public override EntityType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? propertyName = reader.GetString();
        if (propertyName == null)
        {
            throw new JsonException("Expected a property name.");
        }

        if (Enum.TryParse(propertyName, out EntityType entityType))
        {
            return entityType;
        } 
        throw new JsonException($"'{propertyName}' is not a valid {nameof(EntityType)}. Available values are: {string.Join(", ", Enum.GetNames<EntityType>())}");
    }

    public override void Write(Utf8JsonWriter writer, EntityType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    public override EntityType ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Read(ref reader, typeToConvert, options);
    }
    
    public override void WriteAsPropertyName(Utf8JsonWriter writer, EntityType value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}