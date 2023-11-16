using System.Net.NetworkInformation;
using Netmon.Models.Component.Interface;
using Netmon.Models.Json.Converter;
using Newtonsoft.Json;

namespace Netmon.DeviceManager.DTO.Device.Components;

public class InterfaceDTO
{
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    public InterfaceType Type { get; set; }
    [JsonConverter(typeof(PhysicalAddressConverter))]
    public PhysicalAddress PhysAddress { get; set; } = null!;
    public List<InterfaceMetricsDTO> Metrics { get; set; } = new();
}