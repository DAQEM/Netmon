using System.Net.NetworkInformation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevicesLib.Json.Converter;

public class PhysicalAddressConverter : JsonConverter<PhysicalAddress>
{
    public override PhysicalAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && reader.TryGetBytesFromBase64(out byte[]? addressBytes))
        {
            return new PhysicalAddress(addressBytes);
        }

        throw new JsonException($"Unexpected token or invalid MAC address format: {reader.TokenType}");    }

    public override void Write(Utf8JsonWriter writer, PhysicalAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(BitConverter.ToString(value.GetAddressBytes()).Replace("-", ":"));
    }
}