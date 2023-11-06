using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Netmon.Models.Component.Interface.Metric;
using Netmon.Models.Json.Converter;

namespace Netmon.Models.Component.Interface;

public class Interface : IInterface
{
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    public InterfaceType Type { get; set; }
    [JsonConverter(typeof(PhysicalAddressConverter))]
    public PhysicalAddress PhysAddress { get; set; } = null!;
    public List<IInterfaceMetric> Metrics { get; set; } = new();
}