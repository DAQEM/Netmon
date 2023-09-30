using System.Text.Json.Serialization;

namespace SNMPPollingService.Entities;

[JsonConverter(typeof(EntityTypeConverter))]
public enum EntityType
{
    Device,
    Disk,
    Cpu,
    Memory,
    Interface
}